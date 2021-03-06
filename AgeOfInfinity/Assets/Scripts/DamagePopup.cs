using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamagePopup : MonoBehaviour
{
    // Create a Damage Popup
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textMesh.fontSize = 45;
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(1, 1) * 30f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;


        if(disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            // First half of the popup lifetime
            float increaseScaleAmount = 1.5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
            moveVector -= moveVector * 4f * Time.deltaTime;
        }
        else
        {
            // Second half of the popup lifetime
            float decreaseScaleAmount = 1.3f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
            moveVector += moveVector * -15f * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

    }


}
