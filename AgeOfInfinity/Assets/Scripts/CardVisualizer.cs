using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardVisualizer : MonoBehaviour
{
    public List<Card> displayCard = new List<Card>();
    public int displayID;

    public int id;
    public string cardName;
    public int actionCost;
    public int manaCost;
    public int attack;
    public int defense;
    public string description;
    public Sprite artwork;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI manaCostText;
    public TextMeshProUGUI actionCostText;
    public TextMeshProUGUI descriptionText;

    public Image artWork;

    public bool cardBack;
    public static bool staticCardBack;

    public GameObject hand;
    public int numberOfCardsInDeck;


    private void Start()
    {
        numberOfCardsInDeck = PlayerDeck.deckSize;
        displayCard[0] = CardDatabase.cardList[displayID];
    }

    private void Update()
    {
        id = displayCard[0].ID;
        cardName = displayCard[0].cardName;
        actionCost = displayCard[0].actionCost;
        manaCost = displayCard[0].manaCost;
        defense = displayCard[0].defense;
        attack = displayCard[0].attack;
        description = displayCard[0].description;

        artwork = displayCard[0].artwork;

        //setting values on the cards

        nameText.text = " " + cardName;
        manaCostText.text = " " + manaCost;
        actionCostText.text = " " + actionCost;
        if (attack > 0)
        {
            if (defense == 0)
            {
                descriptionText.text = " " + description + attack + " damage.";
            }
            else
            {
                descriptionText.text = " " + description + defense + " Defense and " + +attack + " damage.";
            }
        }

        artWork.sprite = artwork;


        hand = GameObject.Find("Hand");
        if(this.transform.parent == hand.transform.parent)
        {
            cardBack = false;
        }

        staticCardBack = cardBack;

        if(this.tag == "Clone")
        {
            displayCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            cardBack = false;
            this.tag = "Untagged";
        }
        
    }
}
