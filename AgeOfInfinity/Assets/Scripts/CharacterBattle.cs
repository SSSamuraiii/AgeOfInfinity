using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations;
using CodeMonkey.Utils;

public class CharacterBattle : MonoBehaviour
{
    private PlayerAnim characterBase;
    private State state;
    private Vector3 slideTargetPosition;
    private Action onSlideComplete;
    private bool isPlayerTeam;
    private HealthSystem healthSystem;
    public Transform pfHealthBar;
    private Transform healthBarTransform;
    public Transform fillBar;
    private Transform chosenBar;
    public int damageAmount;


    //Player Animations

    public const string KNIGHT_IDLE = "Knight_Idle";
    public const string KNIGHT_ATTACK = "Knight_Attack";

    private enum State
    {
        Idle,
        Sliding,
        Busy,
    }

    private void Awake()
    {
        characterBase = GetComponent<PlayerAnim>();
        state = State.Idle;
    }

    public void Setup(bool isPlayerTeam)
    {
        this.isPlayerTeam = isPlayerTeam;
        if (isPlayerTeam)
        {
            
        }
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(pfHealthBar, new Vector3(GetPosition().x, 12f), Quaternion.identity);
        getChosenHealthBar(healthBarTransform);
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        fillBar.transform.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }

    private void getChosenHealthBar(Transform healthBarTransform)
    {
        chosenBar = this.healthBarTransform;
        fillBar = chosenBar.transform.Find("Fill");
    }

    private void Update()
    {
        switch(state)
        {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                float slideSpeed = 10f;
                transform.position += (slideTargetPosition - GetPosition()) * slideSpeed * Time.deltaTime;

                float reachedDistance = 1f;
                if(Vector3.Distance(GetPosition(), slideTargetPosition) < reachedDistance)
                {
                    // Arrived at Slide Target Position
                    transform.position = slideTargetPosition;
                    onSlideComplete();
                }
                break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Damage(CharacterBattle attacker,int damageAmount)
    {
        healthSystem.Damage(damageAmount);

        Vector3 dirFromAttack = (GetPosition() - attacker.GetPosition()).normalized;
        //Blood particles
        //Changing Character Tint to show he has been hit
        DamagePopup.Create(GetPosition(), damageAmount);

        CodeMonkey.Utils.UtilsClass.ShakeCamera(1f, .1f);

        //CodeMonkey.CMDebug.TextPopup("Hit " + healthSystem.GetHealthAmount(), GetPosition());
        if(healthSystem.IsDead())
        {
            //Died
            //Play death animation
        }
    }

    public bool IsDead()
    {
        return healthSystem.IsDead();
    }

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {
        Vector3 slideTargetPosition = targetCharacterBattle.GetPosition() + (GetPosition() - targetCharacterBattle.GetPosition()).normalized * 10f;
        Vector3 startingPosition = GetPosition();

        // Slide to target
        SlideToPosition(slideTargetPosition, () => {
            // Arrived at Target, attack him
            state = State.Busy;
            Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
            //characterBase.AttackAnimation();
            targetCharacterBattle.Damage(this, damageAmount);
            // Attack completete, slide back
            SlideToPosition(startingPosition, () => {
                // Slide back completed, back to idle
                state = State.Idle;
                //characterBase.IdleAnimation();
                onAttackComplete();
            });
        });

    }

    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }
}
