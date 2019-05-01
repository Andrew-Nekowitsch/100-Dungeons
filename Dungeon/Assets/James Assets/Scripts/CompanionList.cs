using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CompanionList : MonoBehaviour
{
    public Companion hero;
    public Text heroName;
    public Text HP;
    public Text Herolvl;
    // Start is called before the first frame update
    void Start()
    {
        heroName.text = "";
        HP.text = "";
        Herolvl.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        heroName.text = "" + hero.entityName;
        HP.text = "HP " + hero.health + "/" + hero.maxHealth;
        Herolvl.text = "Level: " + hero.level;
    }
}