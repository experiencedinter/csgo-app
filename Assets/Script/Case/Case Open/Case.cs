using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case : MonoBehaviour
{
    public int ID;
    public string Name;
    public float Price;
    public int Size;
    public Sprite Picture;
    public bool Gloves;
    public bool Knife;
    
    public Case(int ID, string Name, float Price, int Size,bool Gloves,bool Knife)
    {
        this.ID = ID;
        this.Name = Name;
        this.Price = Price;    
        this.Size = Size;
        this.Picture = Resources.Load<Sprite>("Case/" + Name);
        this.Knife = Knife;
        this.Gloves = Gloves;
    }

    public Case(Case _case)
    {
        this.ID = _case.ID;
        this.Name = _case.Name;
        this.Price = _case.Price;
        this.Size = _case.Size;
        this.Picture = Resources.Load<Sprite>("Case/" + Name);
        this.Knife = _case.Knife;
        this.Gloves = _case.Gloves;
    }


}
