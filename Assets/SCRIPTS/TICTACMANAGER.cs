using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TICTACMANAGER : MonoBehaviour
{
    public static TICTACMANAGER inst;
    public Sprite X, O;
    public List<Boxes> Boxes = new List<Boxes>();
    public Image Me, Opp;
    public bool placed = false;
    public bool Completed = false,Playerwon,Opponentwon;
    public GameObject Hud, LcPanel;
    public Text Titletext;
    public int FilledBoxes = 0;
    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        Createtable();
        X = Resources.Load<Sprite>("x");
        O = Resources.Load<Sprite>("o");
        SetPlayersprite();
    }

    public void SetPlayersprite()
    {
        int i = Random.Range(0, 2);
        Me.sprite = (i ==0) ? Me.sprite = X: Me.sprite = O;
        Opp.sprite = (i == 0) ? Opp.sprite = O : Opp.sprite = X;
    }


    void Createtable()
    {
        GameObject newcanvas = new GameObject("MYCANVAS");
        Canvas c = newcanvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        newcanvas.AddComponent<CanvasScaler>();
        newcanvas.AddComponent<GraphicRaycaster>();
        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        panel.transform.SetParent(newcanvas.transform, false);
         Image i= panel.AddComponent<Image>();
        i.color = Color.white;
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        GameObject Background = new GameObject("background");
        Background.AddComponent<CanvasRenderer>();
        Background.transform.SetParent(panel.transform, false);
        Image k = Background.AddComponent<Image>();
        k.color = Color.white;
        k.raycastTarget = false;
        Background.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width);

        for (int j = 0; j < 2; j++)
        {
            GameObject vertLines = new GameObject("Lines");
            vertLines.AddComponent<CanvasRenderer>();
            vertLines.transform.SetParent(Background.transform, false);
            Image l = vertLines.AddComponent<Image>();
            l.color = Color.black;
            vertLines.GetComponent<RectTransform>().sizeDelta = new Vector2(15, 900);
            vertLines.GetComponent<RectTransform>().localPosition = new Vector2(300 * j - 150, 0);
            GameObject horizontallines = new GameObject("Lines");
            horizontallines.AddComponent<CanvasRenderer>();
            horizontallines.transform.SetParent(Background.transform, false);
            Image hor = horizontallines.AddComponent<Image>();
            hor.color = Color.black;
            horizontallines.GetComponent<RectTransform>().sizeDelta = new Vector2(900,15);
            horizontallines.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,300 * j - 150);

        }
        for (int box = 0; box < 9; box++)
        {

            GameObject BOX = new GameObject("BOX");
            BOX.AddComponent<Boxes>();
            BOX.AddComponent<Button>();
            Boxes.Add(BOX.GetComponent<Boxes>());
            BOX.GetComponent<Boxes>().BoxNo=box+1;
            GameObject BOXchild = new GameObject("BOX");
            BOX.AddComponent<CanvasRenderer>();
            BOXchild.AddComponent<CanvasRenderer>();
            BOX.transform.SetParent(Background.transform, false);
            BOXchild.transform.SetParent(BOX.transform, false);
            Image l = BOX.AddComponent<Image>();
            l.color = Color.white;
            Image childcol = BOXchild.AddComponent<Image>();
            childcol.color = Color.white;
            BOX.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
            BOXchild.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            if (row < 3)
            {
                BOX.GetComponent<RectTransform>().anchoredPosition = new Vector2( (300 * row) -300,300-(column*300));
                row++;
                if (row == 3)
                {

                    row = 0;
                    column++;
                }
            }
            
        }
        CreateHud(newcanvas);
        createLEVELCOMPLETE(newcanvas);
    }

    void CreateHud( GameObject canvas)
    {
        GameObject panel = new GameObject("HUD");
        panel.AddComponent<CanvasRenderer>();
        panel.transform.SetParent(canvas.transform, false);
        Image i = panel.AddComponent<Image>();
        Color cc = i.color;
        cc.a = 0f;
        i.color = cc;
        i.raycastTarget = false;
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        GameObject you = new GameObject("You");
        you.AddComponent<CanvasRenderer>();
        you.transform.SetParent(panel.transform, false);
        Image k = you.AddComponent<Image>();
        you.GetComponent<RectTransform>().anchorMax=new Vector2(0, 1);
        you.GetComponent<RectTransform>().anchorMin=new Vector2(0, 1);
        you.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        you.GetComponent<RectTransform>().anchoredPosition = new Vector2(300, -120);
        Me = you.GetComponent<Image>();

        GameObject OPP = new GameObject("You");
        OPP.AddComponent<CanvasRenderer>();
        OPP.transform.SetParent(panel.transform, false);
        Image kopp = OPP.AddComponent<Image>();
        OPP.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        OPP.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
        OPP.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        OPP.GetComponent<RectTransform>().anchoredPosition = new Vector2(-90, -120);
        Opp = OPP.GetComponent<Image>();

        //TEXTS
        GameObject metxt = new GameObject("Youtxt");
        metxt.AddComponent<CanvasRenderer>();
        metxt.transform.SetParent(panel.transform, false);
        Text txt = metxt.AddComponent<Text>();
        txt.font = Resources.Load<Font>("Alien");
        txt.text = "YOU";
        txt.alignment = TextAnchor.MiddleCenter;
        txt.color = Color.black;
        txt.fontSize = 80;
        metxt.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        metxt.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        metxt.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150);
        metxt.GetComponent<RectTransform>().anchoredPosition = new Vector2(133, -120);

        GameObject Opptxt = new GameObject("Opptxt");
        Opptxt.AddComponent<CanvasRenderer>();
        Opptxt.transform.SetParent(panel.transform, false);
        Text Opptt = Opptxt.AddComponent<Text>();
        Opptt.font = txt.font;
        Opptt.text = "OPP";
        Opptt.alignment =TextAnchor.MiddleCenter;
        Opptt.color = Color.black;
        Opptt.fontSize = 80;
        Opptxt.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        Opptxt.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
        Opptxt.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150);
        Opptxt.GetComponent<RectTransform>().anchoredPosition = new Vector2(-250, -120);
    }

    void createLEVELCOMPLETE(GameObject canvas)
    {
        LcPanel = new GameObject("LC");
        LcPanel.AddComponent<CanvasRenderer>();
        LcPanel.transform.SetParent(canvas.transform, false);
        Image i = LcPanel.AddComponent<Image>();
        Color cc = i.color;
        cc.a = 0.3f;
        i.color = cc;
        i.raycastTarget = false;
        LcPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        //Texts

        GameObject metxt = new GameObject("Youtxt");
        metxt.AddComponent<CanvasRenderer>();
        metxt.transform.SetParent(LcPanel.transform, false);
        Text txt = metxt.AddComponent<Text>();
        txt.font = Resources.Load<Font>("Alien");
        txt.text = "YOU WON";
        txt.alignment = TextAnchor.MiddleCenter;
        txt.color = Color.black;
        txt.fontSize = 80;
        Titletext = txt;
       
        metxt.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 140);
        


        GameObject Button = new GameObject("Button");
        Button.AddComponent<CanvasRenderer>();
      Button Bt=  Button.AddComponent<Button>();
        Bt.onClick.AddListener(() => { Restart(); });
            Button.transform.SetParent(LcPanel.transform, false);
        Image k = Button.AddComponent<Image>();
        k.color = Color.black;
        Button.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
        Button.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
        Button.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 150);
        Button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 300);

        LcPanel.SetActive(false);

        GameObject Restrt = new GameObject("restart");
        Restrt.AddComponent<CanvasRenderer>();
        Restrt.transform.SetParent(Button.transform, false);
        Text restxt = Restrt.AddComponent<Text>();
        restxt.font = txt.font;
        restxt.text = "RESTART";
        restxt.alignment = TextAnchor.MiddleCenter;
        restxt.color = Color.white;
        restxt.fontSize = 45;
        Restrt.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 140);
        LcPanel.SetActive(false);
    }
    int column = 0,row=0;
   public bool checkwin()
    {
        return
        Checkrownandcolumns(1,2,3)|| Checkrownandcolumns(4, 5, 6) || Checkrownandcolumns(7, 8, 9) || Checkrownandcolumns(1, 4, 7) || Checkrownandcolumns(2, 5, 8) 
        || Checkrownandcolumns(3, 6, 9) || Checkrownandcolumns(1, 5, 9) || Checkrownandcolumns(3, 5, 7);

    }
    public bool checkOppwin()
    {
        return
        CheckOppownandcolumns(1, 2, 3) || CheckOppownandcolumns(4, 5, 6) || CheckOppownandcolumns(7, 8, 9) || CheckOppownandcolumns(1, 4, 7) || CheckOppownandcolumns(2, 5, 8)
        || CheckOppownandcolumns(3, 6, 9) || CheckOppownandcolumns(1, 5, 9) || CheckOppownandcolumns(3, 5, 7);

    }
    public bool Checkrownandcolumns(int x, int y, int z)
    {
         Sprite   MySprite = Me.sprite;
        bool match = (Boxes[x-1].Main.sprite == MySprite && Boxes[y-1].Main.sprite == MySprite && Boxes[z-1].Main.sprite == MySprite);
        return match;
    }
    public bool CheckOppownandcolumns(int x, int y, int z)
    {
        Sprite Opponent = Opp.sprite;
        bool match = (Boxes[x - 1].Main.sprite == Opponent && Boxes[y - 1].Main.sprite == Opponent && Boxes[z - 1].Main.sprite == Opponent);
        return match;
    }

  
    public void Matchcomplete()
    {
        LcPanel.SetActive(true);
        if (Playerwon)
            Titletext.text = "You Won";
        if(Opponentwon)
            Titletext.text = "You Loss";
        if(!Playerwon && !Opponentwon)
            Titletext.text = "MatchDraw";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
