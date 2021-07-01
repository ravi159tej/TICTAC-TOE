using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
/*https://drive.google.com/uc?export=download&id=11lYHZ83Ghlnokb6h6nEOd7iJaINNm6sk*/
public class Loadasset : MonoBehaviour
{
    public string assetName;
    void Start()
    {
        //StartCoroutine(DownloadAsset());
        StartCoroutine(DownloadingAssets());
        createUI();
    }

    IEnumerator DownloadAsset()
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("https://drive.google.com/uc?export=download&id=11lYHZ83Ghlnokb6h6nEOd7iJaINNm6sk"))

        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                var prefab = bundle.LoadAsset(assetName);
                Instantiate(prefab);
                bundle.Unload(false);
            }
        }
    }

    IEnumerator DownloadingAssets()
    {
        using(WWW www=new WWW("https://drive.google.com/uc?export=download&id=11lYHZ83Ghlnokb6h6nEOd7iJaINNm6sk"))
        {
            yield return www;
            if (www.error != null)
            {
                throw new Exception("Download Had an Error" + www.error);

            }
            else
            {
                AssetBundle bundle = www.assetBundle;
                GameObject obj = (GameObject)bundle.LoadAsset(assetName);
                Instantiate(obj);
                bundle.Unload(false);
            }
        }
    }

    public void Spawn()
    {
        StartCoroutine(DownloadingAssets());
    }
    public void createUI()
    {
        GameObject newcanvas = new GameObject("MYCANVAS");
        Canvas c = newcanvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        newcanvas.AddComponent<CanvasScaler>();
        newcanvas.AddComponent<GraphicRaycaster>();
        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        panel.transform.SetParent(newcanvas.transform, false);
        Image i = panel.AddComponent<Image>();
        i.raycastTarget = false;
        i.color = Color.white;
        Color cc = i.color;
        cc.a = 0f;
        i.color = cc;
        
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);


        GameObject Button = new GameObject("Button");
        Button.AddComponent<CanvasRenderer>();
        Button Bt = Button.AddComponent<Button>();
        Bt.onClick.AddListener(() => { Spawn(); });
        Button.transform.SetParent(panel.transform, false);
        Image k = Button.AddComponent<Image>();
        k.color = Color.black;
        Button.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
        Button.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
        Button.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 150);
        Button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 300);
        GameObject Restrt = new GameObject("Spawn");
        Restrt.AddComponent<CanvasRenderer>();
        Restrt.transform.SetParent(Button.transform, false);
        Text restxt = Restrt.AddComponent<Text>();
        restxt.font = Resources.Load<Font>("Alien"); 
        restxt.text = "Spawn";
        restxt.alignment = TextAnchor.MiddleCenter;
        restxt.color = Color.white;
        restxt.fontSize = 45;
        Restrt.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 140);
    }
}
