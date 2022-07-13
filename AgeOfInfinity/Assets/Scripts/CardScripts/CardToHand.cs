using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand : MonoBehaviour
{
    public GameObject hand;
    public GameObject handCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("Hand");
        handCard.transform.SetParent(hand.transform);
        handCard.transform.localScale = Vector3.one;
        handCard.transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        handCard.transform.eulerAngles = new Vector3(25, 0, 0);
    }
}
