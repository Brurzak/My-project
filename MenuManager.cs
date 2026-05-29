using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Riferimenti Menu Carte")]
    public CanvasGroup canvasGroupMenu;
    public RectTransform gridAnchor;
    public GameObject cardPrefab;

    [Header("Configurazione Tendina e Ricerca")]
    public GameObject tendinaChiusaObj;
    public GameObject tendinaApertaObj;
    public GameObject contenitoreRazze;
    public GameObject raceButtonPrefab;
    public TMP_InputField barraDiRicerca;

    [Header("Icone Tendina")]
    public Image iconaRazzaCorrente;
    public Sprite spriteAllAmalgam;
    public Sprite[] spriteIconeRazze;

    [Header("Configurazione Tier (1 a 7)")]
    public Image[] bottoniTier;
    private bool[] tierAttivi = new bool[8];

    [Header("Configurazione Testi Razze")]
    public float spazioTraRigheTendina = 45f;
    public Color coloreTesto = new Color(0.2f, 0.1f, 0.05f, 1f);
    public float grandezzaTesto = 36f;
    public float spostamentoOrizzontale = 0f;
    public float spostamentoVerticaleGlobale = 0f;

    [Header("Configurazione Layout Carte")]
    public int colonne = 4;
    public float spazioX = 350f;
    public float spazioY = 500f;
    public Vector2 cardSize = new Vector2(300, 420);
    public float paddingSuperiore = 50f;
    public float paddingInferiore = 300f;
    public float altezzaVisibileScroll = 600f;
    public float velocitaScroll = 150f;

    public BoardSlot slotSelezionato;

    private List<GameObject> carteAttive = new List<GameObject>();
    private List<GameObject> testiRazzeAttivi = new List<GameObject>();

    private float targetScrollY = 0f;
    private float maxScrollY = 0f;
    private bool isTendinaAperta = false;
    private string filtroRazzaCorrente = "ALL";

    private readonly string[] listaRazze = {
        "Beast", "Demon", "Dragon", "Elemental", "Mech",
        "Murloc", "Naga", "Pirate", "Quilboar", "Undead",
        "Neutral", "Amalgam"
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        DOTween.SetTweensCapacity(1500, 100);
        if (canvasGroupMenu != null)
        {
            canvasGroupMenu.alpha = 0;
            canvasGroupMenu.gameObject.SetActive(false);
        }

        // SISTEMA DI MEMORIA: Inizializziamo i filtri base SOLO all'avvio del gioco
        for (int i = 1; i <= 7; i++) tierAttivi[i] = true;
        filtroRazzaCorrente = "ALL";
    }

    public void ApriMenu(BoardSlot chiamante)
    {
        slotSelezionato = chiamante;
        canvasGroupMenu.gameObject.SetActive(true);
        canvasGroupMenu.DOFade(1f, 0.3f);

        // Nascondiamo istantaneamente tutte le icone in board
        foreach (var slot in BoardSlot.tuttiGliSlot)
        {
            if (slot != null) slot.NascondiIconePerMenuAperto();
        }

        if (BattleManager.Instance != null && BattleManager.Instance.contenitoreBottoni != null)
        {
            BattleManager.Instance.contenitoreBottoni.SetActive(false);
        }

        targetScrollY = 0f;
        gridAnchor.anchoredPosition = Vector2.zero;

        // SISTEMA DI MEMORIA: Aggiorniamo la grafica in base a ciò che è rimasto salvato!
        AggiornaGraficaTier();
        AggiornaIconaHeader(filtroRazzaCorrente);

        // Azzeriamo solo la barra di ricerca, altrimenti è fastidioso cancellare a mano ogni volta
        if (barraDiRicerca != null) barraDiRicerca.text = "";

        ForzaChiusuraTendina();
        ApplicaFiltri();
        GeneraBottoniRazze();
    }

    public void ChiudiMenu()
    {
        foreach (var slot in BoardSlot.tuttiGliSlot)
        {
            if (slot != null)
            {
                if (slot == slotSelezionato)
                {
                    slot.AnimaIconeEntrataMenuChiuso();
                }
                else
                {
                    slot.RipristinaIconeIstante();
                }
            }
        }

        if (BattleManager.Instance != null && BattleManager.Instance.contenitoreBottoni != null)
        {
            BattleManager.Instance.contenitoreBottoni.SetActive(true);
        }

        canvasGroupMenu.DOFade(0f, 0.2f).OnComplete(() => {
            canvasGroupMenu.gameObject.SetActive(false);
            PulisciGriglia();
        });
    }

    void Update()
    {
        if (canvasGroupMenu.gameObject.activeSelf && carteAttive.Count > 0 && !isTendinaAperta)
        {
            if (Mouse.current != null)
            {
                float rawScroll = Mouse.current.scroll.ReadValue().y;
                float inputRotella = Mathf.Clamp(rawScroll, -1f, 1f);

                if (Mathf.Abs(inputRotella) > 0f)
                {
                    targetScrollY -= inputRotella * velocitaScroll;
                    targetScrollY = Mathf.Clamp(targetScrollY, 0f, maxScrollY);
                    gridAnchor.DOAnchorPosY(targetScrollY, 0.2f).SetEase(Ease.OutCubic);
                }
            }
        }
    }

    public void CliccaTendina()
    {
        isTendinaAperta = !isTendinaAperta;

        if (tendinaApertaObj != null)
        {
            tendinaApertaObj.transform.DOKill();

            if (isTendinaAperta)
            {
                tendinaApertaObj.SetActive(true);
                tendinaApertaObj.transform.localScale = new Vector3(0.85f, 0.7f, 1f);
                tendinaApertaObj.transform.DOScale(1f, 0.35f).SetEase(Ease.OutBack);

                for (int i = 0; i < testiRazzeAttivi.Count; i++)
                {
                    Transform testoTr = testiRazzeAttivi[i].transform;
                    testoTr.DOKill();
                    testoTr.localScale = Vector3.zero;
                    testoTr.DOScale(1f, 0.35f).SetEase(Ease.OutBack).SetDelay(0.1f + (i * 0.025f));
                }
            }
            else
            {
                tendinaApertaObj.transform.DOScale(new Vector3(0.85f, 0.7f, 1f), 0.15f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => tendinaApertaObj.SetActive(false));
            }
        }
    }

    public void ForzaChiusuraTendina()
    {
        isTendinaAperta = false;
        if (tendinaChiusaObj != null) tendinaChiusaObj.SetActive(true);
        if (tendinaApertaObj != null)
        {
            tendinaApertaObj.transform.DOKill();
            tendinaApertaObj.transform.localScale = Vector3.one;
            tendinaApertaObj.SetActive(false);
        }
    }

    public void ResetFiltroX()
    {
        ForzaChiusuraTendina();
        filtroRazzaCorrente = "ALL";
        AggiornaIconaHeader(filtroRazzaCorrente);

        if (barraDiRicerca != null) barraDiRicerca.text = "";
        for (int i = 1; i <= 7; i++) tierAttivi[i] = true;
        AggiornaGraficaTier();
        ApplicaFiltri();
    }

    void AggiornaIconaHeader(string filtro)
    {
        if (iconaRazzaCorrente == null) return;

        if (filtro == "ALL" || filtro == "Amalgam")
        {
            iconaRazzaCorrente.sprite = spriteAllAmalgam;
        }
        else
        {
            int index = System.Array.IndexOf(listaRazze, filtro);
            if (index >= 0 && index < spriteIconeRazze.Length)
            {
                iconaRazzaCorrente.sprite = spriteIconeRazze[index];
            }
        }
    }

    public void ToggleTier(int tier)
    {
        if (tier < 1 || tier > 7) return;
        tierAttivi[tier] = !tierAttivi[tier];
        AggiornaGraficaTier();
        ApplicaFiltri();
    }

    void AggiornaGraficaTier()
    {
        if (bottoniTier == null || bottoniTier.Length == 0) return;

        for (int i = 0; i < bottoniTier.Length; i++)
        {
            int tierReal = i + 1;
            if (tierReal < tierAttivi.Length && bottoniTier[i] != null)
            {
                bottoniTier[i].color = tierAttivi[tierReal] ? Color.white : new Color(0.3f, 0.3f, 0.3f, 0.5f);
            }
        }
    }

    public void OnTestoRicercaCambiato(string nuovoTesto)
    {
        ApplicaFiltri();
    }

    void GeneraBottoniRazze()
    {
        foreach (var t in testiRazzeAttivi) Destroy(t);
        testiRazzeAttivi.Clear();

        for (int i = 0; i < listaRazze.Length; i++)
        {
            GameObject btnObj = Instantiate(raceButtonPrefab, contenitoreRazze.transform);
            testiRazzeAttivi.Add(btnObj);

            RectTransform rect = btnObj.GetComponent<RectTransform>();
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 1f);

            float posizionamentoY = -(i * spazioTraRigheTendina) + spostamentoVerticaleGlobale;
            rect.anchoredPosition = new Vector2(spostamentoOrizzontale, posizionamentoY);

            string razzaSelezionata = listaRazze[i];

            TextMeshProUGUI testoTMP = btnObj.GetComponentInChildren<TextMeshProUGUI>();
            if (testoTMP != null)
            {
                testoTMP.text = razzaSelezionata;
                testoTMP.color = coloreTesto;
                testoTMP.fontSize = grandezzaTesto;
            }

            btnObj.GetComponent<Button>().onClick.AddListener(() => {
                ForzaChiusuraTendina();
                filtroRazzaCorrente = razzaSelezionata;
                AggiornaIconaHeader(filtroRazzaCorrente);
                ApplicaFiltri();
            });
        }
    }

    void ApplicaFiltri()
    {
        PulisciGriglia();
        targetScrollY = 0f;
        gridAnchor.anchoredPosition = Vector2.zero;

        var database = BattleManager.Instance.databaseCarte;
        if (database == null || database.Count == 0) return;

        List<CardData> carteFiltrate = database.Where(c => c.techLevel >= 1 && c.techLevel <= 7 && tierAttivi[c.techLevel]).ToList();

        if (filtroRazzaCorrente != "ALL")
        {
            string fUpper = filtroRazzaCorrente.ToUpper();

            if (fUpper == "NEUTRAL")
            {
                carteFiltrate = carteFiltrate.Where(c =>
                    (string.IsNullOrEmpty(c.race)) && (c.races == null || c.races.Count == 0)
                ).ToList();
            }
            else if (fUpper == "AMALGAM")
            {
                carteFiltrate = carteFiltrate.Where(c =>
                    (c.race != null && c.race.ToUpper() == "ALL") ||
                    (c.races != null && c.races.Any(r => r.ToUpper() == "ALL"))
                ).ToList();
            }
            else
            {
                carteFiltrate = carteFiltrate.Where(c =>
                    (c.race != null && (c.race.ToUpper() == fUpper || c.race.ToUpper().Contains(fUpper) || (fUpper == "MECH" && c.race.ToUpper().Contains("MECHANICAL")))) ||
                    (c.races != null && c.races.Any(r => r.ToUpper() == fUpper || r.ToUpper().Contains(fUpper) || (fUpper == "MECH" && r.ToUpper().Contains("MECHANICAL"))))
                ).ToList();
            }
        }

        string testoRicerca = barraDiRicerca != null ? barraDiRicerca.text.Trim().ToLower() : "";
        if (!string.IsNullOrEmpty(testoRicerca))
        {
            carteFiltrate = carteFiltrate.Where(c =>
                (c.name != null && c.name.ToLower().Contains(testoRicerca)) ||
                (c.text != null && c.text.ToLower().Contains(testoRicerca))
            ).ToList();
        }

        carteFiltrate = carteFiltrate.OrderBy(c => c.techLevel).ThenBy(c => c.name).ToList();

        if (carteFiltrate.Count == 0) return;

        RenderizzaGriglia(carteFiltrate);
    }

    void RenderizzaGriglia(List<CardData> lista)
    {
        int righeTotali = Mathf.CeilToInt((float)lista.Count / colonne);
        float altezzaTotaleGriglia = (righeTotali * spazioY) + paddingSuperiore + paddingInferiore;
        maxScrollY = Mathf.Max(0f, altezzaTotaleGriglia - altezzaVisibileScroll);

        float offsetInizialeX = -((colonne - 1) * spazioX) / 2f;

        for (int i = 0; i < lista.Count; i++)
        {
            int riga = i / colonne;
            int colonna = i % colonne;

            GameObject cardObj = Instantiate(cardPrefab, gridAnchor);
            carteAttive.Add(cardObj);

            RectTransform rect = cardObj.GetComponent<RectTransform>();
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 1f);
            rect.sizeDelta = cardSize;
            rect.localScale = Vector3.zero;

            float posX = offsetInizialeX + (colonna * spazioX);
            float posY = -(riga * spazioY) - paddingSuperiore;
            rect.anchoredPosition = new Vector2(posX, posY);

            string cardId = lista[i].id;
            Image img = cardObj.GetComponent<Image>();

            StartCoroutine(CaricaImmagineAttiva(img, cardId));

            Button btn = cardObj.GetComponentInChildren<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => {
                    if (Instance != null && Instance.slotSelezionato != null)
                    {
                        Instance.slotSelezionato.SetCard(cardId);
                        Instance.ChiudiMenu();
                    }
                });
            }
            else
            {
                Debug.LogWarning($"[MenuManager] La carta {cardObj.name} NON ha un bottone! Aggiungilo nel prefab.");
            }

            rect.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(Mathf.Min(i * 0.005f, 0.2f));
        }
    }

    void PulisciGriglia()
    {
        foreach (var c in carteAttive) Destroy(c);
        carteAttive.Clear();
        gridAnchor.DOKill();
    }

    IEnumerator CaricaImmagineAttiva(Image target, string id)
    {
        bool imgPronta = false;

        ImageCache.Instance.RichiediImmagine(id, (art, full) => {
            if (target != null && full != null)
            {
                target.sprite = full;
            }
            imgPronta = true;
        });

        while (!imgPronta)
        {
            yield return null;
        }
    }
}