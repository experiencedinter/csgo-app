using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class flipCoin : MonoBehaviour
{
    public CheckValue checkValue;
    public ChangePointsCoins changePointsCoins;
    SpriteRenderer spriteRenderer;
    public TMP_InputField Playerinput;
    public TMP_InputField Botinput;
    public TMP_Text Gewinn;
    public Sprite[] sides;
    int flipCount = 0;
    int a;
    int b;
    int x;
    int y;
    string input;
    bool isFlipping = false;

    public WriteMostGames mostGames;
    Infos info = new Infos();
    private void Awake()
    {
        info.Gamename = "Coinflip";
        info.Played = 1;
        mostGames.LoadLastItems(info);
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnMouseDown()
    {
        if (int.Parse(Playerinput.text) != 0 && int.Parse(Botinput.text) != 0)
        {
            if(checkValue.EnoughCoins(int.Parse(Playerinput.text)))
            {
                checkValue.ChangeCoins(int.Parse(Playerinput.text));
                if (!isFlipping)
                {
                    a = ReadStringInput(Playerinput.text);
                    b = ReadStringInput(Botinput.text);
                    x = a + b;
                    int randomResult = UnityEngine.Random.Range(0, x);
                    int randomFlips = UnityEngine.Random.Range(5, 10);
                    int flipsToWin = randomFlips;
                    int startingSide = flipCount % 2;
                    if (randomResult < a) // 0 gewinnt
                    {
                        if (startingSide == 0 && randomFlips % 2 == 0)
                        {
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("CT gewinnt");
                        }
                        else if (startingSide == 1 && randomFlips % 2 == 1)
                        {
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("CT gewinnt");
                        }
                        else if (startingSide == 0 && randomFlips % 2 == 1)
                        {
                            flipsToWin++;
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("CT gewinnt");
                        }
                        else if (startingSide == 1 && randomFlips % 2 == 0)
                        {
                            flipsToWin++;
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("CT gewinnt");
                        }
                    }
                    if (randomResult >= a) // 1 gewinnt
                    {
                        if (startingSide == 1 && randomFlips % 2 == 0)
                        {
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("T gewinnt");
                        }
                        else if (startingSide == 0 && randomFlips % 2 == 1)
                        {
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("T gewinnt");
                        }
                        else if (startingSide == 1 && randomFlips % 2 == 1)
                        {
                            flipsToWin++;
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("T gewinnt");
                        }
                        else if (startingSide == 0 && randomFlips % 2 == 0)
                        {
                            flipsToWin++;
                            StartCoroutine(FlipCoinAnimation(0.1f, 1.0f, flipsToWin, randomResult));
                            Debug.Log("T gewinnt");
                        }
                    }
                    Debug.Log("startingSide =" + startingSide);
                    Debug.Log("flipCount =" + flipCount);
                    Debug.Log("flipsToWin =" + flipsToWin);
                    Debug.Log("randomFlips=" + randomFlips);
                    Debug.Log("randomResult =" + randomResult);
                }
            }
        }
    }


    public int ReadStringInput(string stringValue)
    {
        try
        {
            input = stringValue;
        }
        catch
        {
            Debug.Log("Not a Number");
        }
        return int.Parse(input);
    }

    IEnumerator FlipCoinAnimation(float duration, float size, int numFlips, int randomResult)
    {
        isFlipping = true;
        Gewinn.text = "";
        int startingSide = flipCount % 2;
        for (int flip = 0; flip < numFlips; flip++)
        {
            int targetSide = (startingSide + flip) % 2;

            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                size = Mathf.Lerp(83.43758f, 0f, t);
                transform.localScale = new Vector3(83.43758f, size, 83.43758f);
                yield return null;
            }

            spriteRenderer.sprite = sides[targetSide];

            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                size = Mathf.Lerp(0f, 83.43758f, t);
                transform.localScale = new Vector3(83.43758f, size, 83.43758f);
                yield return null;
            }

            transform.localScale = new Vector3(83.43758f, 83.43758f, 83.43758f);

            flipCount++;
        }
        if (randomResult < a)
        {
            
            Gewinn.text = x.ToString();
            changePointsCoins.ChangeCoins(int.Parse(Gewinn.text));
            giveinfo(int.Parse(Gewinn.text));
        }

        else if (randomResult >= a)
        {
            changePointsCoins.ChangeCoins(-int.Parse(Playerinput.text));
            int y = a - (2 * a);
            Gewinn.text = y.ToString();
            giveinfo(-int.Parse(Playerinput.text));
        }
        isFlipping = false;
    }
    private void giveinfo(int winlose)
    {
        info.WinLose = winlose;
        mostGames.LoadLastItems(info);

    }
}
