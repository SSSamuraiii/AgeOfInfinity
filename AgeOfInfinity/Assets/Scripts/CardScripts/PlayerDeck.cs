using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> container = new List<Card>();
    public int x;
    public static int deckSize;
    public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();

    public GameObject cardDeck1;
    public GameObject cardDeck2;
    public GameObject cardDeck3;
    public GameObject cardDeck4;

    public GameObject cardToHand;
    public GameObject[] Clones;
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        deckSize = 40;

        for (int i = 0; i < deckSize; i++)
        {
            x = Random.Range(0, CardDatabase.cardList.Count);
            deck[i] = CardDatabase.cardList[x];
        }

        //Shuffle();

        StartCoroutine(StartGame());
    }

    private void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        staticDeck = deck;

        if (deckSize < 9)
        {
            cardDeck1.SetActive(false);
        }
        if (deckSize < 6)
        {
            cardDeck2.SetActive(false);
        }
        if (deckSize < 3)
        {
            cardDeck3.SetActive(false);
        }
        if (deckSize < 1)
        {
            cardDeck4.SetActive(false);
        }
    }

    IEnumerator StartGame()
    {
        for(int i = 0; i <= 5; i++)
        {
            yield return new WaitForSeconds(0.5f);

            //AudioSource.PlayOneShot(draw, 1f);

            Instantiate(cardToHand, transform.position, transform.rotation);
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
    }
}
