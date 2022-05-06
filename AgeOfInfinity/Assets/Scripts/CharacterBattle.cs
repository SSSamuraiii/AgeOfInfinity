using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private Character_Base characterBase;

    private void Awake()
    {
        characterBase = GetComponent<Character_Base>();
    }

    public void Setup(bool isPlayerTeam)
    {

    }
}
