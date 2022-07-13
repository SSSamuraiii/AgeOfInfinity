using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    private void Awake()
    {
        cardList.Add(new Card(0, "None", "None", 0, 0, 0, 0, Resources.Load<Sprite>("None")));
        cardList.Add(new Card(1, "Practiced Strike", "A Practiced Attack that deals ", 1, 0, 6, 0, Resources.Load<Sprite> ("Attack")));
        cardList.Add(new Card(2, "Block", "A Precise defense. Adds ", 1, 0, 6, 0, Resources.Load<Sprite>("Attack")));
    }
}
