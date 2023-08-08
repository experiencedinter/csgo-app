using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public int ID;
    public string Name;
    public float Price;
    public int Condition;
    public int Color;
    public Sprite Picture;
    public string CaseName;
    public string InspectInGame;
    public bool StatTrak;

    public Weapon(int ID, string Name, float Price, int Condition, int Color, string CaseName, string InspectInGame, bool StatTrak)
    {
        this.ID = ID;
        this.Name = Name;
        this.Price = Price;
        this.Condition = Condition;
        this.Color = Color;
        this.Picture = Resources.Load<Sprite>("Weapon/" + Name);
        this.CaseName = CaseName;
        this.InspectInGame = InspectInGame;
        this.StatTrak = StatTrak;
    }

    public Weapon(Weapon _weapon)
    {
        this.ID = ID;
        this.Name = Name;
        this.Price = Price;
        this.Condition = Condition;
        this.Color = Color;
        this.Picture = Resources.Load<Sprite>("Weapon/" + Name);
        this.CaseName = CaseName;
        this.InspectInGame = InspectInGame;
        this.StatTrak = StatTrak;
    }
}
