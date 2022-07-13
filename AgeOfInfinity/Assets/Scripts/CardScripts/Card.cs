using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card
{
    public int ID;
    public string cardName;
    public string description;
    public int manaCost;
    public int actionCost;
    public int attack;
    public int defense;

    public Sprite artwork;

    public Card()
    {

    }

    public Card(int iD, string cardname, string cardDescription, int actCost, int manacost, int attackPower, int defenseValue, Sprite cardImage)
    {
        ID = iD;
        cardName = cardname;
        description = cardDescription;
        actionCost = actCost;
        manaCost = manacost;
        attack = attackPower;
        defense = defenseValue;

        artwork = cardImage;
    }

}
