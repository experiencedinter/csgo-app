using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberOnly : MonoBehaviour
{
    private TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValidateInput += OnValidateInput;
    }

    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (char.IsDigit(addedChar) || addedChar == '-')
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }
}
