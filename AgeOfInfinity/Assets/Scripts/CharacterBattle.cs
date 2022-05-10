using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private PlayerAnim characterBase;

    private void Awake()
    {
        characterBase = GetComponent<PlayerAnim>();
    }

    public void Setup(bool isPlayerTeam)
    {

    }
}
