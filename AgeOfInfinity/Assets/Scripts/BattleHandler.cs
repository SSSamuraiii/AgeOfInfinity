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

    private CharacterBattle playerCharacterBattle;
    private CharacterBattle enemyCharacterBattle;
    private CharacterBattle activeCharacterBattle;
    private State state;

    private enum State
    {
        WaitingForPlayer,
        Busy,
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerCharacterBattle = SpawnCharacter(true);
        enemyCharacterBattle = SpawnCharacter(false);

        SetActiveCharacterBattle(playerCharacterBattle);
        state = State.WaitingForPlayer;
    }

    private void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //when attacks (unfinished)
                state = State.Busy;
                playerCharacterBattle.damageAmount = 30;
                playerCharacterBattle.Attack(enemyCharacterBattle, () =>
                {
                    ChooseNextActiveCharacter();
                });
            }
        }
    }

    private CharacterBattle SpawnCharacter(bool isPlayerTeam)
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
        Transform characterTransform = Instantiate(pfCharacterBattle, position, Quaternion.identity);
        CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
        characterBattle.Setup(isPlayerTeam);

        return characterBattle;
    }

    private void SetActiveCharacterBattle(CharacterBattle characterBattle)
    {
        activeCharacterBattle = characterBattle;
    }

    private void ChooseNextActiveCharacter()
    {
        if (TestBattleOver())
        {
            return;
        }
        if (activeCharacterBattle == playerCharacterBattle)
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            enemyCharacterBattle.damageAmount = 60;

            enemyCharacterBattle.Attack(playerCharacterBattle, () =>
            {
                ChooseNextActiveCharacter();
            });
        }
        else
        {
            SetActiveCharacterBattle(playerCharacterBattle);
            state = State.WaitingForPlayer;
        }
    }

    private bool TestBattleOver()
    {
        if (playerCharacterBattle.IsDead())
        {
            //Player dead, enemy wins
            //CodeMonkey.CMDebug.TextPopupMouse("Enemy Wins!");
            int randomEndScreen = Random.Range(1, 3);
            switch(randomEndScreen)
            {
                case 1:
                    BattleOverWindow.Show_Static("IS THAT ALL?... HOW DISAPPOINTING...");
                    break;
                case 2:
                    BattleOverWindow.Show_Static("DEAD ALREADY?... LET'S HOPE THE NEXT ONE DOESN'T DIE AS QUICKLY");
                    break;
                case 3:
                    BattleOverWindow.Show_Static("YOU DISAPPOINT ME, I HAD HOPED YOU'D LAST A LITTLE LONGER");
                    break;
            }
            return true;
        }
        if (enemyCharacterBattle.IsDead())
        {
            //Enemy dead, player wins
            //CodeMonkey.CMDebug.TextPopupMouse("Player Wins!");
            BattleOverWindow.Show_Static("Victory!");
            return true;
        }

        return false;
    }
}
