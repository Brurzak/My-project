using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class BoardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static List<BoardSlot> tuttiGliSlot = new List<BoardSlot>();
    public bool isPlayerBoard = true;

    [Header("1. Sfondo e Bordi")]
    public Image placeholderImage;
    public Image bordoNormaleImage;
    public Image bordoPremiumImage;

    [Header("2. Token in Board (Testi Normali)")]
    public GameObject tokenContainer;
    public Image minionTokenArt;
    public TextMeshProUGUI normalAttackText;
    public TextMeshProUGUI normalHealthText;

    [Header("2.1 Token in Board (Testi Golden)")]
    public TextMeshProUGUI goldenAttackText;
    public TextMeshProUGUI goldenHealthText;

    public TextMeshProUGUI attackText => isGolden && goldenAttackText != null ? goldenAttackText : normalAttackText;
    public TextMeshProUGUI healthText => isGolden && goldenHealthText != null ? goldenHealthText : normalHealthText;

    [Header("3. Hover Popup")]
    public GameObject hoverPopupContainer; public Image hoverFullCardImage;

    [Header("4. Icone Meccaniche")]
    public GameObject tauntIcon; public GameObject divineShieldIcon; public GameObject deathrattleIcon; public GameObject rebornIcon; public GameObject triggerIcon; public GameObject poisonousIcon; public GameObject venomousIcon; public GameObject legendaryIcon; public GameObject rallyIcon;

    [Header("5. Icone Golden/Premium")]
    public GameObject tauntPremiumIcon;
    public GameObject legendaryPremiumIcon;

    [Header("Configurazione")]
    [Range(0.1f, 3.0f)] public float scalaBaseToken = 1.0f;
    [Range(0.1f, 3.0f)] public float scalaHoverPopup = 1.0f;

    [Header("Dati Sincronizzazione Realtime")]
    public string currentCardId;
    public CardData datiAttualiCarta;
    public int idUnico;
    public int currentAttack;
    public int currentHealth;

    public int baseCustomAttack;
    public int baseCustomHealth;

    public int originalSiblingIndex;
    public int originalTokenAttack = -1;
    public int originalTokenHealth = -1;

    [Header("Meccaniche Attive (Sovrascrivibili)")]
    public bool isGolden = false;
    public bool hasTaunt;
    public bool hasDivineShield;
    public bool hasVenomous;
    public bool hasPoisonous;
    public bool hasReborn;
    public bool hasWindfury;
    public bool hasStealth;

    private bool isArtReady = false;
    private List<GameObject> iconeAttiveSuQuestaCarta = new List<GameObject>();
    private static BoardSlot slotTrascinato;
    private static BoardSlot slotDropCorrente;

    private bool staTrascinando = false;
    private bool bloccaClickDopoDrag = false;
    private Transform parentTokenPrimaDelDrag;
    private int siblingTokenPrimaDelDrag;
    private Canvas canvasRadice;
    private RectTransform canvasRadiceRect;
    private CanvasGroup canvasGroupDrag;

    void Awake()
    {
        if (!tuttiGliSlot.Contains(this)) tuttiGliSlot.Add(this);
        originalSiblingIndex = transform.GetSiblingIndex();

        GetComponent<Button>().onClick.AddListener(() => {
            bool isSim = BattleManager.Instance != null && BattleManager.Instance.isSimulating;
            if (!isSim && !staTrascinando && !bloccaClickDopoDrag)
            {
                if (string.IsNullOrEmpty(currentCardId))
                {
                    if (MenuManager.Instance != null) MenuManager.Instance.ApriMenu(this);
                }
                else
                {
                    if (MinionSettingsManager.Instance != null) MinionSettingsManager.Instance.ApriImpostazioni(this);
                }
            }
        });

        if (tokenContainer != null) tokenContainer.SetActive(false);
        if (placeholderImage != null) placeholderImage.enabled = true;
        DisattivaEResettaTutteLeIcone();
        if (hoverPopupContainer != null) { hoverPopupContainer.SetActive(false); hoverPopupContainer.transform.localScale = Vector3.one * scalaHoverPopup; }

        SpegniTestiGolden();
    }

    void OnDestroy() => tuttiGliSlot.Remove(this);

    public void SetCard(string idCarta)
    {
        currentCardId = idCarta; isArtReady = false; DisattivaEResettaTutteLeIcone();
        isGolden = false;

        if (bordoNormaleImage != null) bordoNormaleImage.gameObject.SetActive(true);
        if (bordoPremiumImage != null) bordoPremiumImage.gameObject.SetActive(false);
        SpegniTestiGolden();

        if (placeholderImage != null) placeholderImage.enabled = false;
        if (tokenContainer != null) { tokenContainer.SetActive(true); tokenContainer.transform.localScale = Vector3.one * scalaBaseToken; }
        if (minionTokenArt != null) minionTokenArt.color = Color.clear;

        CaricaDatiDaDatabase(true, true);
        CaricaArtwork();

        if (tokenContainer != null)
        {
            tokenContainer.transform.DOKill();
            Vector3 scalaFinale = Vector3.one * scalaBaseToken;
            tokenContainer.transform.localScale = scalaFinale * 1.8f;
            Sequence playAnim = DOTween.Sequence();
            playAnim.Append(tokenContainer.transform.DOScale(scalaFinale * 0.9f, 0.15f).SetEase(Ease.InCubic));
            playAnim.Append(tokenContainer.transform.DOScale(scalaFinale, 0.25f).SetEase(Ease.OutBack));
        }
    }

    public void SetGolden(bool golden)
    {
        isGolden = golden;

        if (bordoNormaleImage != null) bordoNormaleImage.gameObject.SetActive(!isGolden);
        if (bordoPremiumImage != null) bordoPremiumImage.gameObject.SetActive(isGolden);

        if (normalAttackText != null) normalAttackText.gameObject.SetActive(!isGolden);
        if (normalHealthText != null) normalHealthText.gameObject.SetActive(!isGolden);
        if (goldenAttackText != null) goldenAttackText.gameObject.SetActive(isGolden);
        if (goldenHealthText != null) goldenHealthText.gameObject.SetActive(isGolden);

        CaricaDatiDaDatabase(false, true);
        CaricaArtwork();
    }

    private void CaricaDatiDaDatabase(bool estraiMeccanicheIniziali, bool resetStats = false)
    {
        if (BattleManager.Instance == null || BattleManager.Instance.databaseTotale == null) return;

        CardData cartaBase = null;
        BattleManager.Instance.databaseTotale.TryGetValue(currentCardId, out cartaBase);
        datiAttualiCarta = cartaBase;

        if (isGolden && cartaBase != null)
        {
            CardData cartaGolden = null;
            if (cartaBase.battlegroundsPremiumDbfId > 0)
            {
                cartaGolden = BattleManager.Instance.databaseTotale.Values.FirstOrDefault(c => c.dbfId == cartaBase.battlegroundsPremiumDbfId);
            }
            if (cartaGolden == null)
            {
                BattleManager.Instance.databaseTotale.TryGetValue(currentCardId + "_G", out cartaGolden);
            }

            if (cartaGolden != null)
            {
                datiAttualiCarta = cartaGolden;
            }
        }

        if (datiAttualiCarta != null)
        {
            if (estraiMeccanicheIniziali) InizializzaMeccanicheDaBase();
            RicalcolaStatistiche(estraiMeccanicheIniziali || resetStats);
            RinfrescaMeccanicheVisive();
        }
    }

    private void InizializzaMeccanicheDaBase()
    {
        List<string> mec = datiAttualiCarta.mechanics != null ? datiAttualiCarta.mechanics.Select(m => m.ToUpper()).ToList() : new List<string>();

        hasTaunt = mec.Contains("TAUNT");
        hasDivineShield = mec.Contains("DIVINE_SHIELD");
        hasReborn = mec.Contains("REBORN");
        hasPoisonous = mec.Contains("POISONOUS");
        hasVenomous = mec.Contains("VENOMOUS");
        hasWindfury = mec.Contains("WINDFURY");
        hasStealth = mec.Contains("STEALTH");
    }

    private void CaricaArtwork()
    {
        string targetId = datiAttualiCarta != null ? datiAttualiCarta.id : currentCardId;

        ImageCache.Instance.RichiediImmagine(currentCardId, targetId, isGolden, (art, full) => {
            if (minionTokenArt != null && art != null) { minionTokenArt.sprite = art; minionTokenArt.color = Color.white; }
            if (hoverFullCardImage != null && full != null) { hoverFullCardImage.sprite = full; isArtReady = true; }
        });
    }

    public void RinfrescaMeccanicheVisive()
    {
        DisattivaEResettaTutteLeIcone();
        if (datiAttualiCarta == null && string.IsNullOrEmpty(currentCardId)) return;

        bool isLeg = (datiAttualiCarta != null && datiAttualiCarta.elite) || (datiAttualiCarta != null && !string.IsNullOrEmpty(datiAttualiCarta.rarity) && datiAttualiCarta.rarity.ToUpper() == "LEGENDARY");
        if (isLeg) PreparaIcona(isGolden ? legendaryPremiumIcon : legendaryIcon);

        if (hasTaunt) PreparaIcona(isGolden ? tauntPremiumIcon : tauntIcon);
        if (hasDivineShield) PreparaIcona(divineShieldIcon);
        if (hasReborn) PreparaIcona(rebornIcon);
        if (hasPoisonous) PreparaIcona(poisonousIcon);
        if (hasVenomous) PreparaIcona(venomousIcon);

        List<string> mec = datiAttualiCarta != null && datiAttualiCarta.mechanics != null ? datiAttualiCarta.mechanics.Select(m => m.ToUpper()).ToList() : new List<string>();
        bool hasRally = mec.Contains("RALLY") || mec.Contains("BACON_RALLY");
        if (mec.Contains("DEATHRATTLE")) PreparaIcona(deathrattleIcon);

        if (datiAttualiCarta != null && !string.IsNullOrEmpty(datiAttualiCarta.text))
        {
            string t = datiAttualiCarta.text.ToUpper();
            if (t.Contains("START OF COMBAT") || t.Contains("END OF") || t.Contains("AVENGE") || t.Contains("SPELLCRAFT")) PreparaIcona(triggerIcon);
            if (t.Contains("RALLY:") || t.Contains("RALLY (")) hasRally = true;
        }
        if (hasRally && rallyIcon != null) PreparaIcona(rallyIcon);
    }

    public void AnimaIconaTrigger(string tipoTrigger, float speed)
    {
        GameObject iconToAnim = null;
        if (tipoTrigger == "Rantolo") iconToAnim = deathrattleIcon;
        else if (tipoTrigger == "Rally") iconToAnim = rallyIcon;
        else if (tipoTrigger == "Grido") iconToAnim = triggerIcon;

        if (iconToAnim != null && iconToAnim.activeSelf)
        {
            iconToAnim.transform.DOKill();
            iconToAnim.transform.SetAsLastSibling();
            iconToAnim.transform.localScale = Vector3.one;

            Sequence seq = DOTween.Sequence();
            seq.Append(iconToAnim.transform.DOScale(Vector3.one * 1.8f, 0.15f * speed).SetEase(Ease.OutBack));
            seq.AppendInterval(0.1f * speed);
            seq.Append(iconToAnim.transform.DOScale(Vector3.one, 0.2f * speed).SetEase(Ease.InQuad));

            Image img = iconToAnim.GetComponent<Image>();
            if (img != null)
            {
                img.DOKill();
                img.color = Color.white;
                img.DOColor(new Color(1f, 0.9f, 0.2f), 0.15f * speed).SetLoops(2, LoopType.Yoyo);
            }
        }
    }

    public void RicalcolaStatistiche(bool resetToBase = false)
    {
        if (datiAttualiCarta == null) return;

        if (resetToBase)
        {
            bool usaFallback = isGolden && datiAttualiCarta.id == currentCardId;
            currentAttack = usaFallback ? datiAttualiCarta.attack * 2 : datiAttualiCarta.attack;
            currentHealth = usaFallback ? datiAttualiCarta.health * 2 : datiAttualiCarta.health;

            // FIX: Assicura che i Cavalieri nascano nativamente 4/2 (e 8/4) e non 4/1
            if (datiAttualiCarta != null && (datiAttualiCarta.id == "BG25_008" || datiAttualiCarta.id == "BG25_008_G"))
            {
                if (currentAttack == (isGolden ? 8 : 4) && currentHealth == (isGolden ? 2 : 1))
                {
                    currentHealth = isGolden ? 4 : 2;
                }
            }

            baseCustomAttack = currentAttack;
            baseCustomHealth = currentHealth;
        }
        else
        {
            currentAttack = baseCustomAttack;
            currentHealth = baseCustomHealth;
        }

        if (GameSettings.Instance != null && !string.IsNullOrEmpty(datiAttualiCarta.name))
        {
            string n = datiAttualiCarta.name.ToUpper().Trim();
            if (n.Contains("ETERNAL KNIGHT")) { int deaths = isPlayerBoard ? GameSettings.Instance.playerEternalKnightDeaths : GameSettings.Instance.opponentEternalKnightDeaths; currentAttack += ((isGolden ? 8 : 4) * deaths); currentHealth += ((isGolden ? 4 : 2) * deaths); }
            else if (n.Contains("AUTOMATON")) { int summons = isPlayerBoard ? GameSettings.Instance.playerAutomatonSummons : GameSettings.Instance.opponentAutomatonSummons; currentAttack += (3 * summons); currentHealth += (2 * summons); }
        }
        AggiornaTestiStats();
    }

    public void AggiornaTestiStats()
    {
        Color verdeBuff = new Color(0.15f, 0.85f, 0.15f); Color rossoDanno = new Color(0.9f, 0.1f, 0.1f); Color biancoBase = Color.white;
        int displayHp = Mathf.Max(0, currentHealth);

        bool usaFallback = isGolden && datiAttualiCarta != null && datiAttualiCarta.id == currentCardId;
        int trueJsonBaseAtk = datiAttualiCarta != null ? (usaFallback ? datiAttualiCarta.attack * 2 : datiAttualiCarta.attack) : (originalTokenAttack >= 0 ? originalTokenAttack : 0);
        int trueJsonBaseHp = datiAttualiCarta != null ? (usaFallback ? datiAttualiCarta.health * 2 : datiAttualiCarta.health) : (originalTokenHealth >= 0 ? originalTokenHealth : 0);

        // FIX: Evitiamo che i testi diventino rossi/verdi se passiamo da 1 a 2 HP nativi
        if (datiAttualiCarta != null && (datiAttualiCarta.id == "BG25_008" || datiAttualiCarta.id == "BG25_008_G"))
        {
            if (trueJsonBaseAtk == (isGolden ? 8 : 4) && trueJsonBaseHp == (isGolden ? 2 : 1)) trueJsonBaseHp = isGolden ? 4 : 2;
        }

        Color coloreAtk = (currentAttack > trueJsonBaseAtk) ? verdeBuff : biancoBase;
        Color coloreHp = biancoBase;
        if (displayHp > trueJsonBaseHp) coloreHp = verdeBuff;
        else if (displayHp < trueJsonBaseHp) coloreHp = rossoDanno;

        if (attackText != null) { attackText.text = currentAttack.ToString(); attackText.color = coloreAtk; }
        if (healthText != null) { healthText.text = displayHp.ToString(); healthText.color = coloreHp; }
    }

    private void SpegniTestiGolden()
    {
        if (normalAttackText != null) normalAttackText.gameObject.SetActive(true);
        if (normalHealthText != null) normalHealthText.gameObject.SetActive(true);
        if (goldenAttackText != null) goldenAttackText.gameObject.SetActive(false);
        if (goldenHealthText != null) goldenHealthText.gameObject.SetActive(false);
    }

    public void PreparaIcona(GameObject icona) { if (icona != null) { icona.SetActive(true); icona.transform.localScale = Vector3.zero; icona.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack); if (!iconeAttiveSuQuestaCarta.Contains(icona)) iconeAttiveSuQuestaCarta.Add(icona); } }
    public void NascondiIconePerMenuAperto() { foreach (var i in iconeAttiveSuQuestaCarta) if (i != null) i.transform.localScale = Vector3.zero; }
    public void AnimaIconeEntrataMenuChiuso() { foreach (var i in iconeAttiveSuQuestaCarta) if (i != null) { i.transform.DOKill(); i.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack); } }
    public void RipristinaIconeIstante() { foreach (var i in iconeAttiveSuQuestaCarta) if (i != null) { i.transform.DOKill(); i.transform.localScale = Vector3.one; } }
    public void SpegniIconaSpecifica(GameObject icona) { if (icona != null) { icona.SetActive(false); if (iconeAttiveSuQuestaCarta.Contains(icona)) iconeAttiveSuQuestaCarta.Remove(icona); } }

    private void DisattivaEResettaTutteLeIcone()
    {
        iconeAttiveSuQuestaCarta.Clear();
        GameObject[] icone = { tauntIcon, divineShieldIcon, deathrattleIcon, rebornIcon, triggerIcon, poisonousIcon, venomousIcon, legendaryIcon, rallyIcon, tauntPremiumIcon, legendaryPremiumIcon };
        foreach (var i in icone) if (i != null) { i.transform.DOKill(); i.transform.localScale = Vector3.one; i.SetActive(false); }
    }

    public void RipristinaFisicaBase()
    {
        gameObject.SetActive(true);
        if (tokenContainer != null) { tokenContainer.transform.DOKill(); tokenContainer.transform.localPosition = Vector3.zero; tokenContainer.transform.localScale = Vector3.one * scalaBaseToken; }
        if (minionTokenArt != null) minionTokenArt.color = Color.white;
        if (divineShieldIcon != null) { divineShieldIcon.transform.DOKill(); divineShieldIcon.GetComponent<Image>().DOFade(1, 0); }
    }

    public void ClearSlot()
    {
        currentCardId = ""; datiAttualiCarta = null; isArtReady = false; idUnico = 0;
        originalTokenAttack = -1; originalTokenHealth = -1;
        baseCustomAttack = 0; baseCustomHealth = 0;
        isGolden = false; hasTaunt = false; hasDivineShield = false; hasVenomous = false;
        hasPoisonous = false; hasReborn = false; hasWindfury = false; hasStealth = false;

        if (bordoNormaleImage != null) bordoNormaleImage.gameObject.SetActive(true);
        if (bordoPremiumImage != null) bordoPremiumImage.gameObject.SetActive(false);
        SpegniTestiGolden();

        if (minionTokenArt != null) { minionTokenArt.sprite = null; minionTokenArt.color = Color.clear; }
        if (hoverFullCardImage != null) hoverFullCardImage.sprite = null;

        RipristinaFisicaBase(); DisattivaEResettaTutteLeIcone();
        if (tokenContainer != null) tokenContainer.SetActive(false);
        if (hoverPopupContainer != null) hoverPopupContainer.SetActive(false);
        if (placeholderImage != null) placeholderImage.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotTrascinato != null) { if (slotTrascinato != this && slotTrascinato.isPlayerBoard == isPlayerBoard && transform.parent == slotTrascinato.transform.parent) { slotDropCorrente = this; } return; }
        bool isSim = BattleManager.Instance != null && BattleManager.Instance.isSimulating;
        if (!string.IsNullOrEmpty(currentCardId) && isArtReady && hoverPopupContainer != null && !isSim)
        {
            hoverPopupContainer.SetActive(true); hoverPopupContainer.transform.DOKill(); hoverPopupContainer.transform.localScale = Vector3.one * (scalaHoverPopup * 0.7f); hoverPopupContainer.transform.DOScale(Vector3.one * scalaHoverPopup, 0.2f).SetEase(Ease.OutBack);
        }
    }

    public void OnPointerExit(PointerEventData eventData) { if (slotDropCorrente == this) slotDropCorrente = null; if (hoverPopupContainer != null) hoverPopupContainer.SetActive(false); }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!PuoTrascinare()) return;
        staTrascinando = true; bloccaClickDopoDrag = true; slotTrascinato = this; slotDropCorrente = null;

        if (hoverPopupContainer != null) hoverPopupContainer.SetActive(false);
        parentTokenPrimaDelDrag = tokenContainer.transform.parent; siblingTokenPrimaDelDrag = tokenContainer.transform.GetSiblingIndex();
        canvasRadice = GetComponentInParent<Canvas>(); canvasRadiceRect = canvasRadice != null ? canvasRadice.transform as RectTransform : null;
        canvasGroupDrag = tokenContainer.GetComponent<CanvasGroup>(); if (canvasGroupDrag == null) canvasGroupDrag = tokenContainer.AddComponent<CanvasGroup>();
        canvasGroupDrag.blocksRaycasts = false; canvasGroupDrag.alpha = 0.9f;

        if (canvasRadice != null) { tokenContainer.transform.SetParent(canvasRadice.transform, true); tokenContainer.transform.SetAsLastSibling(); }
        tokenContainer.transform.DOKill(); tokenContainer.transform.DOScale(Vector3.one * (scalaBaseToken * 1.12f), 0.12f).SetEase(Ease.OutQuad);
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!staTrascinando || slotTrascinato != this || tokenContainer == null) return;
        if (canvasRadiceRect != null) { Camera cam = canvasRadice.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvasRadice.worldCamera; if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRadiceRect, eventData.position, cam, out Vector3 worldPoint)) { tokenContainer.transform.position = worldPoint; } }
        else { tokenContainer.transform.position = eventData.position; }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotTrascinato != this) return;
        BoardSlot destinazione = TrovaSlotSottoMouse(eventData);
        RipristinaTokenDopoDrag();

        if (destinazione != null && destinazione != this && destinazione.isPlayerBoard == isPlayerBoard && destinazione.transform.parent == transform.parent)
        {
            transform.SetSiblingIndex(destinazione.transform.GetSiblingIndex());
            CustomBoardLayout layout = GetComponentInParent<CustomBoardLayout>();
            if (layout != null) layout.AllineaBoardCentrata();
            Canvas.ForceUpdateCanvases();
        }

        staTrascinando = false; slotTrascinato = null; slotDropCorrente = null;
        StartCoroutine(ResetBloccoClickDrag());
    }

    private bool PuoTrascinare() { bool isSim = BattleManager.Instance != null && BattleManager.Instance.isSimulating; return !isSim && tokenContainer != null && tokenContainer.activeSelf && !string.IsNullOrEmpty(currentCardId); }
    private BoardSlot TrovaSlotSottoMouse(PointerEventData eventData) { if (eventData.pointerCurrentRaycast.gameObject != null) { BoardSlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<BoardSlot>(); if (slot != null) return slot; } return slotDropCorrente; }
    private void RipristinaTokenDopoDrag() { if (tokenContainer == null) return; tokenContainer.transform.DOKill(); if (parentTokenPrimaDelDrag != null) { tokenContainer.transform.SetParent(parentTokenPrimaDelDrag, true); tokenContainer.transform.SetSiblingIndex(siblingTokenPrimaDelDrag); } tokenContainer.transform.localPosition = Vector3.zero; tokenContainer.transform.localScale = Vector3.one * scalaBaseToken; if (canvasGroupDrag != null) { canvasGroupDrag.blocksRaycasts = true; canvasGroupDrag.alpha = 1f; } }
    private IEnumerator ResetBloccoClickDrag() { yield return null; bloccaClickDopoDrag = false; }
}