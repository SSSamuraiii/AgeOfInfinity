using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleOverWindow : MonoBehaviour
{

    private static BattleOverWindow instance;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        Hide();
    }

    private void Awake()
    {
        instance = this;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show(string winnerString)
    {
        gameObject.SetActive(true);
        text.SetText(winnerString);
    }

    public static void Show_Static(string winnerString)
    {
        instance.Show(winnerString);
    }

}
