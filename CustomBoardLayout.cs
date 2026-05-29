using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class CustomBoardLayout : MonoBehaviour
{
    [Header("Impostazioni Slot")]
    public GameObject slotPrefab;
    public int numeroSlot = 7;

    [Header("Dimensioni e Spaziature")]
    public float larghezzaSlot = 200f;
    public float altezzaSlot = 280f;
    public float spazioTraLeCarte = 210f;
    public float spostamentoVerticale = 0f;
    public float spostamentoOrizzontale = 0f;

    [Header("Animazioni")]
    public float tempoAnimazione = 0.5f;
    public float ritardoTraCarte = 0.05f; // Effetto cascata

    [Header("Fazione")]
    public bool isPlayerBoard = true;

    void Start()
    {
        GeneraEAllineaIniziale();
    }

    public void GeneraEAllineaIniziale()
    {
        if (slotPrefab == null)
        {
            Debug.LogError($"[CustomBoardLayout] Manca lo slotPrefab assegnato su {gameObject.name}!");
            return;
        }

        // Pulizia iniziale di sicurezza in caso di riavvio
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Calcolo matematico per la centratura
        float startX = -((numeroSlot - 1) * spazioTraLeCarte) / 2f;
        startX += spostamentoOrizzontale;

        for (int i = 0; i < numeroSlot; i++)
        {
            GameObject nuovoSlot = Instantiate(slotPrefab, transform);
            nuovoSlot.name = (isPlayerBoard ? "PlayerSlot_" : "OpponentSlot_") + (i + 1);

            // Assegnazione fazione automatica
            BoardSlot bSlot = nuovoSlot.GetComponent<BoardSlot>();
            if (bSlot != null)
            {
                bSlot.isPlayerBoard = this.isPlayerBoard;
            }

            RectTransform rect = nuovoSlot.GetComponent<RectTransform>();

            // Ancoraggi al centro
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);

            // RIPRISTINATE LE DIMENSIONI ESATTE CHE AVEVI
            rect.sizeDelta = new Vector2(larghezzaSlot, altezzaSlot);

            // Partono da zero e fermi al centro per l'animazione
            rect.localScale = Vector3.zero;
            rect.anchoredPosition = new Vector2(spostamentoOrizzontale, spostamentoVerticale);

            float posizioneFinaleX = startX + (i * spazioTraLeCarte);

            // RIPRISTINATA L'ANIMAZIONE DI SPAWN ESATTA
            rect.DOAnchorPos(new Vector2(posizioneFinaleX, spostamentoVerticale), tempoAnimazione)
                .SetEase(Ease.OutBack)
                .SetDelay(i * ritardoTraCarte);

            rect.DOScale(Vector3.one, tempoAnimazione)
                .SetEase(Ease.OutBack)
                .SetDelay(i * ritardoTraCarte);
        }
    }

    // Questa funzione viene chiamata per "stringere" la board durante il Fight e "riallargarla" al Reset
    public void AllineaBoardCentrata(bool istantaneo = false)
    {
        List<BoardSlot> slotLocali = GetComponentsInChildren<BoardSlot>(true).ToList();

        List<BoardSlot> slotPieni = slotLocali.Where(s => s != null && !string.IsNullOrEmpty(s.currentCardId)).ToList();
        List<BoardSlot> slotVuoti = slotLocali.Where(s => s != null && string.IsNullOrEmpty(s.currentCardId)).ToList();

        bool isSim = BattleManager.Instance != null && BattleManager.Instance.isSimulating;

        foreach (var vuoto in slotVuoti)
        {
            if (vuoto != null) vuoto.gameObject.SetActive(!isSim);
        }

        List<BoardSlot> slotDaAnimare = isSim ? slotPieni : slotLocali;
        int count = slotDaAnimare.Count;
        if (count == 0) return;

        float startX = -((count - 1) * spazioTraLeCarte) / 2f;
        startX += spostamentoOrizzontale;

        for (int i = 0; i < count; i++)
        {
            if (slotDaAnimare[i] == null) continue;

            RectTransform rect = slotDaAnimare[i].GetComponent<RectTransform>();
            float posizioneFinaleX = startX + (i * spazioTraLeCarte);

            rect.DOKill();
            Vector2 posizioneFinale = new Vector2(posizioneFinaleX, spostamentoVerticale);

            if (istantaneo)
            {
                rect.anchoredPosition = posizioneFinale;
            }
            else
            {
                rect.DOAnchorPos(posizioneFinale, tempoAnimazione)
                    .SetEase(Ease.OutBack);
            }
        }
    }
}
