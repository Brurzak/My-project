using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    [Header("Livelli Taverna")]
    public int playerTavernTier = 1;
    public int opponentTavernTier = 1;

    [Header("Gemme del Sangue (Blood Gems) Player")]
    public int playerBloodGemAttack = 1;
    public int playerBloodGemHealth = 1;

    [Header("Gemme del Sangue (Blood Gems) Opponent")]
    public int opponentBloodGemAttack = 1;
    public int opponentBloodGemHealth = 1;

    [Header("Contatori Cavalieri Eterni")]
    public int playerEternalKnightDeaths = 0;
    public int opponentEternalKnightDeaths = 0;

    [Header("Contatori Automaton")]
    public int playerAutomatonSummons = 0;
    public int opponentAutomatonSummons = 0;

    [Header("Velocità Simulazione")]
    public bool isSpeedX2 = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Avvisa tutte le carte in gioco che le impostazioni sono cambiate
    private void AggiornaCarteInBoard()
    {
        foreach (var slot in BoardSlot.tuttiGliSlot)
        {
            if (slot != null && !string.IsNullOrEmpty(slot.currentCardId))
            {
                slot.RicalcolaStatistiche();
            }
        }
    }

    // --- SISTEMA DI PARSING ANTI-BUG PER TEXTMESHPRO ---
    private bool EstraiNumero(string val, out int risultato)
    {
        risultato = 0;
        if (string.IsNullOrEmpty(val)) return false;

        // Distrugge il carattere invisibile \u200B generato da TMP e gli spazi extra
        string testoPulito = val.Replace("\u200B", "").Trim();
        return int.TryParse(testoPulito, out risultato);
    }

    // --- SETTER LIVELLI TAVERNA ---
    public void SetPlayerTier(string val) { if (EstraiNumero(val, out int res)) playerTavernTier = Mathf.Clamp(res, 1, 7); }
    public void SetOpponentTier(string val) { if (EstraiNumero(val, out int res)) opponentTavernTier = Mathf.Clamp(res, 1, 7); }

    // --- SETTER GEMME DEL SANGUE ---
    public void SetPlayerGemAtk(string val) { if (EstraiNumero(val, out int res)) playerBloodGemAttack = res; }
    public void SetPlayerGemHP(string val) { if (EstraiNumero(val, out int res)) playerBloodGemHealth = res; }
    public void SetOpponentGemAtk(string val) { if (EstraiNumero(val, out int res)) opponentBloodGemAttack = res; }
    public void SetOpponentGemHP(string val) { if (EstraiNumero(val, out int res)) opponentBloodGemHealth = res; }

    // --- SETTER CAVALIERI ETERNI (Con aggiornamento istantaneo) ---
    public void SetPlayerKnightDeaths(string val)
    {
        if (EstraiNumero(val, out int res)) { playerEternalKnightDeaths = res; AggiornaCarteInBoard(); }
    }
    public void SetOpponentKnightDeaths(string val)
    {
        if (EstraiNumero(val, out int res)) { opponentEternalKnightDeaths = res; AggiornaCarteInBoard(); }
    }

    // --- SETTER AUTOMATON (Con aggiornamento istantaneo) ---
    public void SetPlayerAutomatonSummons(string val)
    {
        if (EstraiNumero(val, out int res)) { playerAutomatonSummons = res; AggiornaCarteInBoard(); }
    }
    public void SetOpponentAutomatonSummons(string val)
    {
        if (EstraiNumero(val, out int res)) { opponentAutomatonSummons = res; AggiornaCarteInBoard(); }
    }

    public void ToggleSpeedX2()
    {
        isSpeedX2 = !isSpeedX2;
        Time.timeScale = isSpeedX2 ? 2.0f : 1.0f;
    }
}