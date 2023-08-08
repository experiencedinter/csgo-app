using TMPro;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    public CheckValue checkValue;
    public WriteInventar writeInventar;
    public Weapon weaponinfo;

    void whatareWeapon()
    {
        weaponinfo.Name = gameObject.GetComponent<CreateShopGrid>().gameObject.Name;
        weaponinfo.Price = gameObject.GetComponent<CreateShopGrid>().gameObject.Price;
        weaponinfo.InspectInGame = gameObject.GetComponent<CreateShopGrid>().gameObject.InspectInGame;
        weaponinfo.Condition = gameObject.GetComponent<CreateShopGrid>().gameObject.Condition;
        weaponinfo.Color = gameObject.GetComponent<CreateShopGrid>().gameObject.Color;
        weaponinfo.StatTrak = gameObject.GetComponent<CreateShopGrid>().gameObject.StatTrak;
        weaponinfo.CaseName = gameObject.GetComponent<CreateShopGrid>().gameObject.CaseName;
        string name = weaponinfo.Name;
        string formatiertername = name.Replace(" | ", "_");
        weaponinfo.Picture = Resources.Load<Sprite>("Weapon/" + weaponinfo.CaseName + "/" + formatiertername);
    }

    public void BuyingwithCoins(TMP_Text Coins) {

        if (checkValue.EnoughCoins(int.Parse(Coins.text)))
        {
            checkValue.ChangeCoins(int.Parse(Coins.text));
            whatareWeapon();
            writeInventar.WriteInventarData(weaponinfo);
        }

    }

    public void BuyingwithPoints(TMP_Text Points)
    {

        if (checkValue.EnoughPoints(int.Parse(Points.text)))
        {
            checkValue.ChangePoints(int.Parse(Points.text));
            whatareWeapon();
            writeInventar.WriteInventarData(weaponinfo);
        }

    }
}
