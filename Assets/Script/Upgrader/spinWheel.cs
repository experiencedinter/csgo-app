using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spinWheel : MonoBehaviour
{
    public CheckValue checkValue;
    public ChangePointsCoins changePointsCoins;

    private float RotatePower;
    private float StopPower;
    private Rigidbody2D rbody;
    int inRotate;
    public Button button2x;
    public Button button4x;
    public Button button10x;
    public Sprite[] wheels;
    public TMP_InputField inputField;
    private int selectedButtonIndex = -1;
    public TMP_Text Gewinn;
    public TMP_Text Warnung;
    int a;
    int b;
    int c;
    int d;
    int v;
    string input;
    private float[] rewardValues;
    private bool rewardButtonPressed = false;

    private void Start()
    {
        Debug.Log("Start");
        rbody = GetComponent<Rigidbody2D>();
        button2x.onClick.AddListener(() => OnButtonClick(0));
        button4x.onClick.AddListener(() => OnButtonClick(1));
        button10x.onClick.AddListener(() => OnButtonClick(2));
    }
    Infos info = new Infos();
    public WriteMostGames mostGames;

    private void Awake()
    {
        info.Gamename = "Upgrader";
        info.Played = 1;
        mostGames.LoadLastItems(info);
    }
    private void giveinfo(int winlose)
    {
        info.WinLose = winlose;
        mostGames.LoadLastItems(info);

    }
    float t;
    private void Update()
    {
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= StopPower * Time.deltaTime;

            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        if (rbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward(selectedButtonIndex);

                inRotate = 0;
                t = 0;
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

    private void OnButtonClick(int buttonIndex)
    {
        if (inputField.text != "")
        {
            if (inRotate == 0 && int.Parse(inputField.text) != 0)
            {
                Warnung.text = "";
                selectedButtonIndex = buttonIndex;
                ChangeWheel(buttonIndex);
                rewardButtonPressed = true;
            }
        }
        
    }

    public void ChangeWheel(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < wheels.Length && inRotate == 0)
        {
            Image wheelImage = GetComponent<Image>();
            wheelImage.sprite = wheels[buttonIndex];
        }
    }
    public void Rotate(int selectedButtonIndex)
    {
        if (rewardButtonPressed)
        {
            a = ReadStringInput(inputField.text);
            b = a * 2;
            c = a * 4;
            d = a * 10;
            v = a - 2 * a;
            rewardValues = new float[] { b, c, d };
            RotatePower = UnityEngine.Random.Range(2000, 5000);
            StopPower = UnityEngine.Random.Range(175, 450);
            Debug.Log("Button Clicked!");
            Debug.Log(RotatePower);
            Debug.Log(StopPower);
            if (inRotate == 0 && checkValue.EnoughCoins(int.Parse(inputField.text)))
            {
                checkValue.ChangeCoins(int.Parse(inputField.text));
                rbody.AddTorque(RotatePower);
                inRotate = 1;
            }
        }
        else
        {
            Warnung.text = "Wähle erst einen \n Multiplikator aus!";
        }
    }

    public void GetReward(int buttonIndex)
    {
        float rot = transform.eulerAngles.z;
        if (buttonIndex == 0)
        {
            if (rot > 0 && rot <= 180)
            {
                Win("You Win");
                Gewinn.text = b.ToString();
                changePointsCoins.ChangeCoins(int.Parse(Gewinn.text));
                giveinfo(int.Parse(Gewinn.text));
            }
            else if (rot > 180 && rot <= 360)
            {
                Win("You Lose");
                Gewinn.text = v.ToString();
                giveinfo(-int.Parse(Gewinn.text));
            }
        }
        else if (buttonIndex == 1)
        {
            if (rot > 0 && rot <= 90)
            {
                
                Win("You Win");
                Gewinn.text = c.ToString();
                changePointsCoins.ChangeCoins(int.Parse(Gewinn.text));
                giveinfo(int.Parse(Gewinn.text));
            }
            else if (rot > 90 && rot <= 360)
            {

                Win("You Lose");
                Gewinn.text = v.ToString();
                giveinfo(-int.Parse(Gewinn.text));
            }
        }
        else if (buttonIndex == 2)
        {
            if (rot > 0 && rot <= 36)
            {
                
                Win("You Win");
                Gewinn.text = d.ToString();
                changePointsCoins.ChangeCoins(int.Parse(Gewinn.text));
                giveinfo(int.Parse(Gewinn.text));
            }
            else if (rot > 36 && rot <= 360)
            {
                Win("You Lose");
                Gewinn.text = v.ToString();
                giveinfo(-int.Parse(Gewinn.text));
            }
        }
    }

    public void Win(string Message)
    {
        print(Message);
    }
}
