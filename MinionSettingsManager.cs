using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MinionSettingsManager : MonoBehaviour
{
    private static MinionSettingsManager _instance;
    public static MinionSettingsManager Instance
    {
        get { if (_instance == null) _instance = FindAnyObjectByType<MinionSettingsManager>(FindObjectsInactive.Include); return _instance; }
    }

    [Header("Pannello Principale")]
    public CanvasGroup panelGroup;

    [Header("Riquadro Superiore")]
    public Image artworkImage;
    public Button artworkButton;
    public TextMeshProUGUI nomeMinionText;

    [Header("Statistiche")]
    public TMP_InputField inputAttacco;
    public TMP_InputField inputSalute;

    [Header("Tick Verdi (Effetti)")]
    public GameObject tickGolden;
    public GameObject tickDivineShield;
    public GameObject tickTaunt;
    public GameObject tickVenomous;
    public GameObject tickPoisonous;
    public GameObject tickReborn;
    public GameObject tickWindfury;
    public GameObject tickStealth;

    private BoardSlot slotCorrente;
    private bool staAggiornandoInput = false;

    void Awake()
    {
        _instance = this;
        if (panelGroup != null) panelGroup.alpha = 1;
        if (artworkButton != null) { artworkButton.onClick.RemoveAllListeners(); artworkButton.onClick.AddListener(CliccaSostituisciMinion); }
        if (inputAttacco != null) inputAttacco.onValueChanged.AddListener(delegate { ApplicaStatistiche(); });
        if (inputSalute != null) inputSalute.onValueChanged.AddListener(delegate { ApplicaStatistiche(); });
    }

    public void ApriImpostazioni(BoardSlot slot)
    {
        gameObject.SetActive(true);
        slotCorrente = slot;

        if (slot.minionTokenArt != null && slot.minionTokenArt.sprite != null) artworkImage.sprite = slot.minionTokenArt.sprite;
        nomeMinionText.text = slot.datiAttualiCarta != null ? slot.datiAttualiCarta.name : "Token Evocato";

        AggiornaInputDaSlot();

        if (tickGolden) tickGolden.SetActive(slot.isGolden);
        if (tickDivineShield) tickDivineShield.SetActive(slot.hasDivineShield);
        if (tickTaunt) tickTaunt.SetActive(slot.hasTaunt);
        if (tickVenomous) tickVenomous.SetActive(slot.hasVenomous);
        if (tickPoisonous) tickPoisonous.SetActive(slot.hasPoisonous);
        if (tickReborn) tickReborn.SetActive(slot.hasReborn);
        if (tickWindfury) tickWindfury.SetActive(slot.hasWindfury);
        if (tickStealth) tickStealth.SetActive(slot.hasStealth);

        panelGroup.alpha = 0;
        panelGroup.DOFade(1f, 0.2f);
    }

    public void ChiudiImpostazioni() { panelGroup.DOFade(0f, 0.15f).OnComplete(() => { gameObject.SetActive(false); }); }

    public void CliccaSostituisciMinion() { ChiudiImpostazioni(); slotCorrente.ClearSlot(); MenuManager.Instance.ApriMenu(slotCorrente); }

    public void ToggleGolden() { slotCorrente.SetGolden(!slotCorrente.isGolden); if (tickGolden != null) tickGolden.SetActive(slotCorrente.isGolden); AggiornaInputDaSlot(); }
    public void ToggleDivineShield() { slotCorrente.hasDivineShield = !slotCorrente.hasDivineShield; tickDivineShield.SetActive(slotCorrente.hasDivineShield); slotCorrente.RinfrescaMeccanicheVisive(); }
    public void ToggleTaunt() { slotCorrente.hasTaunt = !slotCorrente.hasTaunt; tickTaunt.SetActive(slotCorrente.hasTaunt); slotCorrente.RinfrescaMeccanicheVisive(); }
    public void ToggleVenomous() { slotCorrente.hasVenomous = !slotCorrente.hasVenomous; tickVenomous.SetActive(slotCorrente.hasVenomous); slotCorrente.RinfrescaMeccanicheVisive(); }
    public void TogglePoisonous() { slotCorrente.hasPoisonous = !slotCorrente.hasPoisonous; tickPoisonous.SetActive(slotCorrente.hasPoisonous); slotCorrente.RinfrescaMeccanicheVisive(); }
    public void ToggleReborn() { slotCorrente.hasReborn = !slotCorrente.hasReborn; tickReborn.SetActive(slotCorrente.hasReborn); slotCorrente.RinfrescaMeccanicheVisive(); }
    public void ToggleWindfury() { slotCorrente.hasWindfury = !slotCorrente.hasWindfury; tickWindfury.SetActive(slotCorrente.hasWindfury); slotCorrente.RinfrescaMeccanicheVisive(); }
    public void ToggleStealth() { slotCorrente.hasStealth = !slotCorrente.hasStealth; tickStealth.SetActive(slotCorrente.hasStealth); slotCorrente.RinfrescaMeccanicheVisive(); }

    private void AggiornaInputDaSlot()
    {
        staAggiornandoInput = true;
        if (inputAttacco != null) inputAttacco.text = slotCorrente.currentAttack.ToString();
        if (inputSalute != null) inputSalute.text = slotCorrente.currentHealth.ToString();
        nomeMinionText.text = slotCorrente.datiAttualiCarta != null ? slotCorrente.datiAttualiCarta.name : "Token Evocato";
        staAggiornandoInput = false;
    }

    public void ApplicaStatistiche()
    {
        if (slotCorrente == null || staAggiornandoInput) return;

        // FIX 3: Salviamo nella memoria base custom prima di ricalcolare, così lo scaling non si somma sporco!
        if (int.TryParse(inputAttacco.text, out int atk)) { slotCorrente.currentAttack = atk; slotCorrente.baseCustomAttack = atk; }
        if (int.TryParse(inputSalute.text, out int hp)) { slotCorrente.currentHealth = hp; slotCorrente.baseCustomHealth = hp; }

        slotCorrente.RicalcolaStatistiche(false);
    }
}