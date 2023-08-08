using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class MinenfeldGridCreate : MonoBehaviour
{
    public Button Startbutton;
    public Button Neustart;
    public Button Auszahlen;

    public ChangePointsCoins pointsCoins;
    public CheckValue checkValue;

    public TMP_InputField bombcount;
    public TMP_InputField Einsatz;
    public TMP_Text Infotext;
    public TMP_Text Infotext2;
    public TMP_Text Infotext3;
    public TMP_Text Gewinn;

    GameObject Minenfield;
    GameObject Feld;
    GameObject buttonText;

    public float[] Klicked = new float[35];
    public bool[] bombgrid = new bool[35];

    int OpenFields = 35;
    int klickedFields = 0;
    int bomb; 
    public int count;
    public int test;
    int Einsatzcheck, Bombencheck;
    float temp = 0;
    bool klicked;
    private void Start()
    {
        Auszahlen.interactable = false;
        Neustart.gameObject.SetActive(false);
    }
    Infos info = new Infos();
    public WriteMostGames mostGames;

    private void Awake()
    {
        info.Gamename = "Minenfeld";
        info.Played = 1;
        mostGames.LoadLastItems(info);
    }
    private void giveinfo(int winlose)
    {
        info.WinLose = winlose;
        mostGames.LoadLastItems(info);

    }
    void randomBomb()
    {
        System.Random random = new System.Random();
        while (count < bomb) { 
            int number = random.Next(0, 35);
            if (!bombgrid[number]) {
                bombgrid[number] = true;
                count++;
            }
        } 
    }
    void bombset()
    {
        bomb = int.Parse(bombcount.text);
    }
    private void SetReset()
    {
        count = 0;
        bomb = 0;
        temp = 0;
        OpenFields = 35;
        klickedFields = 0;
        for (int i = 0; i < bombgrid.Length; i++)
        {
            bombgrid[i] = false;
        }
        Minenfield = GameObject.Find("Minenfeld");
        for (int i = 0; i < bombgrid.Length; i++)
        {
            Feld = Minenfield.transform.GetChild(i).gameObject;
            Feld.GetComponent<Button>().interactable = false;
            Feld.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Field");
            buttonText = Feld.transform.GetChild(0).gameObject;
            buttonText.GetComponent<TMP_Text>().text = "";
        }
        Auszahlen.interactable = true;
    }
    
    private void Update()
    {
        try
        {
            Einsatzcheck = int.Parse(Einsatz.text);
            Bombencheck = int.Parse(bombcount.text);
            if (Einsatzcheck > 0 && Bombencheck > 0)
            {
                Startbutton.interactable = true;
            }
            else
            {
                Startbutton.interactable = false;
            }
        }
        catch (System.Exception)
        {}
        
    }
    public void place()
    {
        try
        {

            if (int.Parse(bombcount.text) >= 35)
            {
                Infotext.gameObject.SetActive(true);
            }

            else
            {
                if (Infotext.gameObject.active)
                    Infotext.gameObject.SetActive(false);
                else
                {
                    neustart();
                }
                if (checkValue.EnoughCoins(int.Parse(Einsatz.text)))
                {
                    checkValue.ChangeCoins(int.Parse(Einsatz.text));
                    SetReset();
                    bombset();
                    randomBomb();
                    openFields();
                }
                
                
            }
            if(Infotext2.enabled)
            {
                Infotext2.gameObject.SetActive(false);
            }
            
        }
        catch (System.Exception)
        {
            Infotext2.gameObject.SetActive(true);
        }  
    }
    
    void openFields()
    {
        for (int i = 0; i < bombgrid.Length; i++)
        {
            Feld = Minenfield.transform.GetChild(i).gameObject;
            Feld.GetComponent<Button>().interactable = true;
        }
    }

    public void openField(int name)
    {
        Startbutton.interactable = false;
        Minenfield = GameObject.Find("Minenfeld");
        Feld = Minenfield.transform.GetChild(name - 1).gameObject;
        OpenFields--;
        klickedFields++;
        if (bombgrid[name-1])
        {
            

            if (bombgrid[name - 1])
            {
                Feld.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Bombe");
                Feld.GetComponent<Button>().interactable = false;
                gameover();
                Debug.Log("Game Over");
            }
                
            Debug.Log(Minenfield.transform.GetChild(name-1).gameObject.name);
        }
        else
        {
            Feld.GetComponent<Button>().interactable = false;
            buttonText = Feld.transform.GetChild(0).gameObject;
            buttonText.GetComponent<TMP_Text>().text = ((bombgrid.Length - OpenFields * 0.2f) * (count/100f)).ToString() + "x";
            CheckWinOver();


        }
    }

    void Gewinnrechner()
    {
        if (klickedFields == 0)
        {
            klicked = false;
        }
        else
        {
            klicked = true;
        }

        if (klicked)
        {
            temp = (bombgrid.Length - OpenFields * 0.2f) * (count / 100f);

            Gewinn.text = Mathf.CeilToInt((int.Parse(Einsatz.text) + (temp * int.Parse(Einsatz.text)))).ToString();
            int coins = Mathf.CeilToInt(int.Parse(Einsatz.text) + (temp * int.Parse(Einsatz.text)));
            giveinfo(int.Parse(Gewinn.text));
            pointsCoins.ChangeCoins(coins);
        }
        else
        {
            pointsCoins.ChangeCoins(int.Parse(Einsatz.text));
        }
    }


    void gameover()
    {
        Minenfield = GameObject.Find("Minenfeld");
        giveinfo(-int.Parse(Einsatz.text));
        for (int i = 0; i < bombgrid.Length; i++)
        {
            Feld = Minenfield.transform.GetChild(i).gameObject;
            Feld.GetComponent<Button>().interactable = false;
        }
        Startbutton.gameObject.SetActive(false);
        Auszahlen.interactable = false;
        Neustart.gameObject.SetActive(true);
    }

    void CheckWinOver()
    {
        test = bombgrid.Length - klickedFields;
        if (bombgrid.Length - klickedFields == count)
        {
            Gewinnrechner();
            gameover();
        }
            
    }

    public void auszahlen()
    {
        Gewinnrechner();
        gameover();
        Startbutton.interactable = true;
        SetReset();
        Auszahlen.interactable = false;
    }

    public void neustart()
    {
        SetReset();
        Startbutton.gameObject.SetActive(true);
        Neustart.gameObject.SetActive(false);
        Auszahlen.interactable = false;
        Startbutton.interactable = true;
    }
}
