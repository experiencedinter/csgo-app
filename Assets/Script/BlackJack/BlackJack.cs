using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackJack : MonoBehaviour
{
    public Button Standbutton;
    public Button Hitbutton;
    public Button Doublebutton;

    public ChangePointsCoins changePointsCoins;
    public CheckValue checkValue;

    public GameObject PlayerZoneContainer;
    public GameObject DealerZoneContainer;
    public GameObject playercard;
    public GameObject dealercard;
    public GameObject dealercardhide;
    public List<GameObject> Hidecards;

    public TMP_Text Gewinn;
    public TMP_InputField Einsatz;

    public Sprite[] cards = new Sprite[52];
    public Sprite[] shuffelcards = new Sprite[52];

    public List<Sprite> Dealercards = new List<Sprite>();
    public List<Sprite> Playerscards = new List<Sprite>();

    int Cardsout = 0;
    bool gameover;
    int TempEinsatz;
    public int playercardscount , delaerscardscount;
    int playercards = 0, dealerscards = 0;
    
    Infos info = new Infos();
    public WriteMostGames mostGames;

    private void Awake()
    {
        info.Gamename = "BlackJack";
        info.Played = 1;
        mostGames.LoadLastItems(info);
    }
    private void giveinfo(int winlose)
    {
        info.WinLose = winlose;
        mostGames.LoadLastItems(info);

    }
    void loadCards()
    {
        cards = Resources.LoadAll<Sprite>("Images/Cards/");
        Standbutton.interactable = false;
        Hitbutton.interactable = false;
        Doublebutton.interactable = false;
        ShuffelCards();
    }
    void ShuffelCards()
    {
        for (int i = 0; i < cards.Length;)
        {
            int randomnumber = Random.Range(0, cards.Length);
            if (shuffelcards[i] == null)
            {
                shuffelcards[i] = cards[randomnumber];
                i++;
            }
                
        }
    }
    void RestartGame()
    {
        Hidecards.Clear();

        for (int i = 0; i < playercards; i++)
        {
            Destroy(PlayerZoneContainer.transform.GetChild(i).gameObject);
            
        }
        for (int i = 0; i < dealerscards; i++)
        {
            Destroy(DealerZoneContainer.transform.GetChild(i).gameObject);
        }
        gameover = false;
        Gewinn.text = "0";
        Cardsout = 0;
        playercards = 0;
        dealerscards = 0;
        delaerscardscount = 0;
        playercardscount = 0;
        Playerscards.Clear();
        Dealercards.Clear();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = null;
            shuffelcards[i] = null;
        }
    }
    public void StartGame()
    {
        if (Einsatz.text != "" && checkValue.EnoughCoins(int.Parse(Einsatz.text)))
        {
            checkValue.ChangeCoins(int.Parse(Einsatz.text));
            RestartGame();
            loadCards();
            Standbutton.interactable = true;
            Hitbutton.interactable = true;
            Doublebutton.interactable = true;
            StandartPull();
        }
        
    }
    public void Stand()
    {
        Standbutton.interactable = false;
        Hitbutton.interactable = false;
        Doublebutton.interactable = false;
        DealerPullCard();
    }
    public void Hit()
    {
        PlayerPullCard();
    }
    public void Double()
    {
        Standbutton.interactable = false;
        Hitbutton.interactable = false;
        Doublebutton.interactable = false;
        PlayerPullCard();
        TempEinsatz = (int.Parse(Einsatz.text) * 2);
        if(!gameover)
            Stand();

    }
    void PlayerPullCard()
    {
        Playerscards.Add(shuffelcards[Cardsout]);
        PlayerCreateCard(Playerscards[playercards].name);
        PlayercountCards();
        countCards();
        PlayerGameOverCheck();
    }
    void DealerPullCard()
    {
        Dealercards.Add(shuffelcards[Cardsout]);
        DealerCreateCard(Dealercards[dealerscards].name);
        DealercountCards();
        countCards();
        UnderSixteenCheck();
        if (delaerscardscount <= 16)
            DealerPullCard();
        else
            WhoWins();
    }
    void UnderSixteenCheck()
    {
        delaerscardscount = 0;
        for (int i = 0; i < Dealercards.Count; i++)
        {
            delaerscardscount += SplitCardSpriteName(Dealercards[i].name);
            if (delaerscardscount == 21 && Dealercards.Count == 2)
            {
                Gameover();
            }
            else if (delaerscardscount >= 22)
                Gameover();
        }
    }
    void WhoWins()
    {
        if (delaerscardscount > playercardscount && delaerscardscount < 22)
            Gameover();
        else if (delaerscardscount > playercardscount && delaerscardscount > 21)
            Win();
        else if (delaerscardscount == playercardscount)
            GiveMoneyBack();
        else
            Win();
    }
    void GiveMoneyBack()
    {
        Gewinn.text = Einsatz.text;
    }
    void StandartPull()
    {
        Playerscards.Add(shuffelcards[Cardsout]);
        PlayerCreateCard(Playerscards[0].name);
        PlayercountCards();
        countCards();
        Dealercards.Add(shuffelcards[Cardsout]);
        DealerCreateCard(Dealercards[0].name);
        DealercountCards();
        countCards();
        Playerscards.Add(shuffelcards[Cardsout]);
        PlayerCreateCard(Playerscards[1].name);
        PlayercountCards();
        countCards();
        PlayerGameOverCheck();
        UnderSixteenCheck();
    }
    void countCards()
    {
        Cardsout++;
    }
    void PlayercountCards()
    {
        playercards++;
    }
    void DealercountCards()
    {
        dealerscards++;
    }
    void PlayerGameOverCheck()
    {
        playercardscount = 0;
        for (int i = 0; i < Playerscards.Count; i++)
        {
            playercardscount += SplitCardSpriteName(Playerscards[i].name);
            if (playercardscount == 21 && Playerscards.Count == 2)
                BlackjackWin();
            else if (playercardscount >= 22)
            {
                gameover = true;
                Gameover();
            }
        }
    }
    void BlackjackWin()
    {
        Standbutton.interactable = false;
        Hitbutton.interactable = false;
        Doublebutton.interactable = false;
        Gewinn.text = ((int.Parse(Einsatz.text)* 2) * 1.25f).ToString();
        info.WinLose = int.Parse(Gewinn.text);
        changePointsCoins.ChangeCoins(int.Parse(Gewinn.text));
        giveinfo(int.Parse(Gewinn.text));
    }
    void Win()
    {
        Standbutton.interactable = false;
        Hitbutton.interactable = false;
        Doublebutton.interactable = false;
        Gewinn.text = (int.Parse(Einsatz.text) * 2).ToString();
        info.WinLose = int.Parse(Gewinn.text);
        changePointsCoins.ChangeCoins(int.Parse(Gewinn.text));
        giveinfo(int.Parse(Gewinn.text));
    }
    void Gameover()
    {
        info.WinLose = -int.Parse(Gewinn.text);
        Standbutton.interactable = false;
        Hitbutton.interactable = false;
        Doublebutton.interactable = false;
        giveinfo(int.Parse(Gewinn.text) * -1);
    }
    int SplitCardSpriteName(string spriteName)
    {
        string numberPart = spriteName.Replace("card", "");

        char lastChar = numberPart[numberPart.Length - 1];
        if (char.IsLetter(lastChar))
        {
            return GetCardLetterValue(lastChar);
        }
        else
        {
            int numericValue = int.Parse(lastChar.ToString());
            if (numericValue == 0)
                return 10;
            return numericValue;
        }
    }
    int GetCardLetterValue(char letter)
    {
        switch (letter)
        {

            case 'A': {
                    if (dealerscards > 2 || playercards > 2)
                        return 1;
                    else
                        return 11;
                        }
            case 'K': return 10;
            case 'Q': return 10;
            case 'J': return 10;
            default: return 0;
        }
    }
    void PlayerCreateCard(string Cardname)
    {
        playercard = new GameObject(Cardname);
        playercard.transform.parent = PlayerZoneContainer.transform;
        playercard.AddComponent<Image>().sprite = Resources.Load<Sprite>("Images/Cards/"+Cardname);
        playercard.transform.localScale = Vector3.one;
    }
    void DealerCreateCard(string Cardname)
    {
        dealercard = new GameObject(Cardname);
        dealercard.transform.parent = DealerZoneContainer.transform;
        dealercard.AddComponent<Image>().sprite = Resources.Load<Sprite>("Images/Cards/" + Cardname);
        dealercard.transform.localScale = Vector3.one;
    }
}

