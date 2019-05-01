using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public Text CharacterText;
    public Player Hero;

    // Start is called before the first frame update
    void Start()
    {
        CharacterText.text = "Hero name: " + Hero.entityName + "\n"
            + "Level: " + Hero.level + "\n"
            + "Health: " + Hero.maxHealth + "\n"
            + "Strenght: " + Hero.strength + "\n"
            + "Speed: " + Hero.speed + "\n"
            + "Armor: " + Hero.armor + "\n"
            + "Attack Speed: " + Hero.attackSpeed + "\n"
            + "Health Regenaration: " + Hero.healthRegen + "\n"
            + "Detection: " + Hero.detection + "\n"
            + "Stealth: " + Hero.stealth + "\n";
    }

}
