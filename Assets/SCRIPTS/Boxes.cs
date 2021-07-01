using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boxes : MonoBehaviour
{
    public int BoxNo;
    public bool IsFilled = false;
   public Image Main;
    TICTACMANAGER Tic_Tac;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        Main = transform.GetChild(0).GetComponent<Image>();
        Tic_Tac = TICTACMANAGER.inst;
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { OnButtonClicked(); });
    }
    public void OnButtonClicked()
    {
        if (IsFilled || Tic_Tac.Completed)
            return;
        Tic_Tac.FilledBoxes++;
        if (Tic_Tac.placed)
        {
            Main.sprite = TICTACMANAGER.inst.Me.sprite;
            Tic_Tac.placed = false;
            bool Won = Tic_Tac.checkwin();
            if (Won)
            {
                Tic_Tac.Completed = true;
                Tic_Tac.Playerwon = true;
                Tic_Tac.Matchcomplete();
            } else if (Tic_Tac.FilledBoxes == 9)
            {
                Tic_Tac.Completed = true;
                Tic_Tac.Matchcomplete();
            }
        }
        else
        {
            Main.sprite = TICTACMANAGER.inst.Opp.sprite;
            Tic_Tac.placed = true;
            bool Won = Tic_Tac.checkOppwin();
            if (Won)
            {
                Tic_Tac.Completed = true;
                Tic_Tac.Opponentwon = true;
                Tic_Tac.Matchcomplete();
            }
            else if (Tic_Tac.FilledBoxes == 9)
            {
                Tic_Tac.Completed = true;
                Tic_Tac.Matchcomplete();
            }
        }
        IsFilled = true;
       
    }
   
}
