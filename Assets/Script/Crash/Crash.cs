using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Crash : MonoBehaviour
{
    [HideInInspector]
    public float multiplier = 1f;
    [HideInInspector]
    public float playerMultiplier;
    [HideInInspector]
    public int playerBet = 0;

    int index = 0;
    public float time;

    public CheckValue checkValue;
    public ChangePointsCoins changePointsCoins;

    public Animator animator;
    public TMP_InputField balanceInput;
    public TMP_InputField multiplierInput;
    public TMP_Text gewinn;
    public TMP_Text multiplikator;
    public TMP_Text showEinsatz;
    public TMP_Text winLose;
    public Button confirmInput;
    public Button start;
    public Button cashOut;

    void Start()
    {
        start.interactable = false;
        cashOut.interactable = false;
        confirmInput.interactable = true;
    }

    void reset()
    {
        multiplier = 1f;
        index = 0;
        playerBet = 0;
        start.interactable = false;
        confirmInput.interactable = true;
    }

    Infos info = new Infos();
    public WriteMostGames mostGames;

    private void Awake()
    {
        info.Gamename = "Crash";
        info.Played = 1;
        mostGames.LoadLastItems(info);
    }
    private void giveinfo(int winlose)
    {
        info.WinLose = winlose;
        mostGames.LoadLastItems(info);

    }

    public void setBet()
    {
        if (checkValue.EnoughCoins(int.Parse(balanceInput.text)) && int.Parse(balanceInput.text) > 0)
        {
            checkValue.ChangeCoins(int.Parse(balanceInput.text));
            playerBet = int.Parse(balanceInput.text);
            playerMultiplier = float.Parse(multiplierInput.text);
            Debug.Log("Einsatz: " + playerBet + " bei " + playerMultiplier + "X");
            showEinsatz.text = "Einsatz: " + playerBet + " auf " + playerMultiplier + "X";
            start.interactable = true;
            winLose.text = "";
            multiplikator.text = "";
            animator.SetBool("isStarted", false);
            animator.SetBool("lose", false);
        }
    }

    public void Button()
    {
        InvokeRepeating("startCrash", 0.0f, 1.0f / 7.0f);
    }

    void startCrash()
    {

        int isCrash = Random.Range(1, 100);

        cashOut.interactable = true;
        start.interactable = false;
        confirmInput.interactable = false;
        GameObject.Find("background").GetComponent<backgroundLoop>().StartLoop();
        animator.SetBool("isStarted", true);
        if (index == 0 && isCrash < 4)
        {
            Debug.Log("Instant Crash");
            loseGame();

        }

        else if (isCrash == 1)
        {

            loseGame();

        }

        else
        {
            multiplier += multiplier * 0.01f;
            index++;
            time = index / 7f;
            multiplikator.text = multiplier.ToString() + "X";
            Debug.Log("Multiplier: " + multiplier + " bei: " + time);

            if (multiplier > playerMultiplier)
            {
                winGame();

            }
        }

    }


    void loseGame()
    {
        cashOut.interactable = false;
        Debug.Log("You lose! Multiplier: " + multiplier);
        winLose.text = "Du hast verloren!";
        CancelInvoke();
        GameObject.Find("background").GetComponent<backgroundLoop>().StopLoop();
        animator.SetBool("isStarted", false);
        animator.SetBool("lose", true);
        giveinfo(-int.Parse(balanceInput.text));
        reset();
    }

    public void winGame()
    {
        cashOut.interactable = false;
        Debug.Log("You win! Multiplier: " + multiplier);
        float gewinnBetrag = playerBet * multiplier;
        Debug.Log("Gewinn: " + Mathf.RoundToInt(gewinnBetrag));
        gewinn.text = Mathf.RoundToInt(gewinnBetrag).ToString();
        changePointsCoins.ChangeCoins(Mathf.RoundToInt(gewinnBetrag));
        winLose.text = "Du hast Gewonnen!\nDein Gewinn beträgt: " + Mathf.RoundToInt(gewinnBetrag);
        giveinfo(int.Parse(balanceInput.text));
        CancelInvoke();
        GameObject.Find("background").GetComponent<backgroundLoop>().StopLoop();
        reset();

    }
}
