using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{

    private static BattleHandler instance;

    public static BattleHandler GetInstance()
    {
        return instance;
    }


    [SerializeField] private Transform pfCharacterBattle;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnCharacter(true);
        SpawnCharacter(false);
    }

    private void SpawnCharacter(bool isPlayerTeam)
    {
        Vector3 position;
        if(isPlayerTeam)
        {
            position = new Vector3(-50, 0);
        }
        else
        {
            position = new Vector3(+50, 0);
        }
        Instantiate(pfCharacterBattle, position, Quaternion.identity);
    }

}
