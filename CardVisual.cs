using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CardVisual : MonoBehaviour
{
    private Image imageComponent;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null) return;
        //preserveAspect è fondamentale per non schiacciare la carta
        imageComponent.preserveAspect = true;
    }

    public void CaricaImmagine(string cardId)
    {
        // URL ESATTO del database Firestone per Battlegrounds (512x in PNG)
        string url = $"https://static.firestoneapp.com/cards/bgs/enUS/512/{cardId}.png";
        StartCoroutine(ScaricaImmagineDalWeb(url));
    }

    IEnumerator ScaricaImmagineDalWeb(string url)
    {
        // Utilizziamo un semplice UnityWebRequest e non UnityWebRequestTexture
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Otteniamo i dati grezzi scaricati
                byte[] textureData = request.downloadHandler.data;

                // --- FIX QUALITÀ ESTREMA (ADDIO MIPMAPS & SFOCATURA) ---
                // Creiamo una texture NUOVA. '2, 2' sono dimensioni temporanee.
                // TextureFormat.ARGB32 = Colori pieni, niente compressione.
                // 'false' nel quarto parametro = DISATTIVA LE MIPMAPS COMPLETAMENTE.
                Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);

                // Carichiamo i dati grezzi PNG nella texture
                texture.LoadImage(textureData); // Metodo in ImageConversion

                ApplicaTexturePro(texture);
            }
            else
            {
                Debug.LogError($"❌ Artwork originale non trovato: {url}");
            }
        }
    }

    void ApplicaTexturePro(Texture2D texture)
    {
        // Trilinear è il miglior filtro disponibile in Unity per UI scalate in giù
        texture.filterMode = FilterMode.Trilinear;
        texture.anisoLevel = 16; // Migliora la nitidezza ad angolazioni diverse
        texture.wrapMode = TextureWrapMode.Clamp; // Evita sbavature sui bordi
        texture.mipMapBias = -0.5f; // "Sveglia" Unity per usare dettagli più nitidi
        texture.Apply(true, false); // Applichiamo i cambiamenti alla texture

        // Creiamo lo sprite
        //SpriteMeshType.FullRect è fondamentale per le immagini UI intere
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect);

        // Applichiamo la grafica nitida
        imageComponent.sprite = sprite;
        imageComponent.color = Color.white;
    }
}