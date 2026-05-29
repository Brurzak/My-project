using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ImageCache : MonoBehaviour
{
    public static Dictionary<string, Sprite> cacheCarteFull = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> cacheCarteArt = new Dictionary<string, Sprite>();

    public static ImageCache Instance;

    private int maxDownloadSimultanei = 15;
    private int downloadAttivi = 0;
    private Queue<IEnumerator> codaDownload = new Queue<IEnumerator>();

    void Awake() { if (Instance == null) Instance = this; }

    public void PrescaricaImmagine(string cardId) { RichiediImmagine(cardId, cardId, false, null); }

    public void RichiediImmagine(string cardId, System.Action<Sprite, Sprite> onComplete)
    {
        StartCoroutine(GestisciRichiesta(cardId, cardId, false, onComplete));
    }

    public void RichiediImmagine(string baseId, string targetId, bool isGolden, System.Action<Sprite, Sprite> onComplete)
    {
        StartCoroutine(GestisciRichiesta(baseId, targetId, isGolden, onComplete));
    }

    private IEnumerator GestisciRichiesta(string baseId, string targetId, bool isGolden, System.Action<Sprite, Sprite> onComplete)
    {
        string cacheId = isGolden ? baseId + "_G" : baseId;

        if (!cacheCarteFull.ContainsKey(cacheId))
        {
            cacheCarteFull.Add(cacheId, null);

            // IL LINK CORRETTO: Cerca l'ID della vera carta (targetId) su Firestone. 
            // Se fallisce, il metodo DownloadFull interrogherà HearthstoneJSON per l'Hover render dorato
            string urlPrimarioFull = $"https://static.firestoneapp.com/cards/bgs/enUS/512/{targetId}.png";
            string urlFallbackFull = $"https://art.hearthstonejson.com/v1/render/latest/enUS/512x/{targetId}.png";

            AccodaDownload(DownloadFull(urlPrimarioFull, urlFallbackFull, cacheId, baseId));
        }

        if (!cacheCarteArt.ContainsKey(cacheId))
        {
            cacheCarteArt.Add(cacheId, null);
            AccodaDownload(DownloadArtPura(targetId, cacheId));
        }

        while (cacheCarteFull[cacheId] == null || cacheCarteArt[cacheId] == null) yield return null;

        onComplete?.Invoke(cacheCarteArt[cacheId], cacheCarteFull[cacheId]);
    }

    private void AccodaDownload(IEnumerator routine) { codaDownload.Enqueue(routine); ProcessaCoda(); }
    private void ProcessaCoda() { while (downloadAttivi < maxDownloadSimultanei && codaDownload.Count > 0) { downloadAttivi++; StartCoroutine(EseguiDownload(codaDownload.Dequeue())); } }
    private IEnumerator EseguiDownload(IEnumerator routine) { yield return StartCoroutine(routine); downloadAttivi--; ProcessaCoda(); }

    IEnumerator DownloadFull(string urlPrimario, string urlFallback, string cacheId, string baseIdFallback)
    {
        UnityWebRequest request = UnityWebRequest.Get(urlPrimario);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            cacheCarteFull[cacheId] = CreaSprite(request.downloadHandler.data);
        }
        else
        {
            UnityWebRequest reqFallback = UnityWebRequest.Get(urlFallback);
            yield return reqFallback.SendWebRequest();

            if (reqFallback.result == UnityWebRequest.Result.Success)
            {
                cacheCarteFull[cacheId] = CreaSprite(reqFallback.downloadHandler.data);
            }
            else
            {
                // Ultimo estremo tentativo: ricarica la base (no premium)
                string urlBase = $"https://static.firestoneapp.com/cards/bgs/enUS/512/{baseIdFallback}.png";
                UnityWebRequest reqBase = UnityWebRequest.Get(urlBase);
                yield return reqBase.SendWebRequest();

                cacheCarteFull[cacheId] = reqBase.result == UnityWebRequest.Result.Success ? CreaSprite(reqBase.downloadHandler.data) : CreaSpriteVuoto();
                reqBase.Dispose();
            }
            reqFallback.Dispose();
        }

        request.Dispose();
    }

    IEnumerator DownloadArtPura(string cardId, string cacheId)
    {
        string urlPrimario = $"https://art.hearthstonejson.com/v1/orig/{cardId}.png";
        UnityWebRequest req = UnityWebRequest.Get(urlPrimario);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            cacheCarteArt[cacheId] = CreaSprite(req.downloadHandler.data);
            req.Dispose();
            yield break;
        }
        req.Dispose();

        string urlFallback = $"https://art.hearthstonejson.com/v1/256x/{cardId}.jpg";
        UnityWebRequest reqFallback = UnityWebRequest.Get(urlFallback);
        yield return reqFallback.SendWebRequest();

        cacheCarteArt[cacheId] = reqFallback.result == UnityWebRequest.Result.Success ? CreaSprite(reqFallback.downloadHandler.data) : CreaSpriteVuoto();
        reqFallback.Dispose();
    }

    private Sprite CreaSprite(byte[] data)
    {
        Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false); tex.LoadImage(data);
        tex.filterMode = FilterMode.Bilinear; tex.wrapMode = TextureWrapMode.Clamp; tex.Apply(false, false);
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect);
    }

    private Sprite CreaSpriteVuoto()
    {
        Texture2D tex = new Texture2D(2, 2);
        return Sprite.Create(tex, new Rect(0, 0, 2, 2), Vector2.zero);
    }
}