using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseDatabase : MonoBehaviour
{
    public List<Case> _case = new List<Case>();
    public void Awake()
    {
        BuildDatabase();
    }
    public Case GetStats(string Objectname)
    {
        return _case.Find(_case =>  _case.Name == Objectname);
    }

    public Case GetStats(int ID)
    {
        return _case.Find(_case => _case.ID == ID);
    }

    void BuildDatabase()
    {
        _case = new List<Case>() {

            new Case(0,"CSGO_Bravo_Case",61.75f,18,true,false),
            new Case(1,"CSGO_Weapon_Case_1",109.49f,18,true,false),
            new Case(2,"CSGO_Weapon_Case_2",17.46f,18,true,false),
            new Case(3,"CSGO_Weapon_Case_3",12.17f,18,true,false),
            new Case(4,"CSGO_Dangerzone_Case",3.29f,18,true,false),
            new Case(5,"CSGO_Dreams_and_Nightmares_Case",3.91f,18,true,false),
            new Case(6,"CSGO_Fracture_Case",3.19f,18,true,false),
            new Case(7,"CSGO_Horizont_Case",3.47f,18,true,false),
            new Case(8,"CSGO_Operation_Broken_Fang_Case",7.02f,18,true,false),
            new Case(9,"CSGO_Operation_Hydra_Case",29.92f,18,true,false),
            new Case(10,"CSGO_Operation_Phoenix_Case",6.19f,18,true,false),
            new Case(11,"CSGO_Operation_Riptide_Case",8.10f,18,true,false),
            new Case(12,"CSGO_Prisma_2_Case",3.30f,18,true,false),
            new Case(13,"CSGO_Recoil_Case",3.10f,18,true,false),
            new Case(14,"CSGO_Revolution_Case",3.89f,18,true,false),
            new Case(15,"CSGO_Snakebite_Case",2.88f,18,true,false),
            new Case(16,"CSGO_Winter_Offensive_Case",10.17f,18,true,false),


        };
    }

}
