using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterBattle : MonoBehaviour
{
    private PlayerAnim characterBase;
    private State state;
    private Vector3 slideTargetPosition;
    private Action onSlideComplete;

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
        if(isPlayerTeam)
        {
            //set attack animation
        }
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

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {
        Vector3 slideTargetPosition = targetCharacterBattle.GetPosition() + (GetPosition() - targetCharacterBattle.GetPosition()).normalized * 10f;
        Vector3 startingPosition = GetPosition();

        // Slide to target
        SlideToPosition(slideTargetPosition, () => {
            // Arrived at Target, attack him
            Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
            // play attack animation
            // Attack completete, slide back
            SlideToPosition(startingPosition, () => {
                // Slide back completed, back to idle
                characterBase.ChangeAnimationState(PlayerAnim.KNIGHT_IDLE);
                onAttackComplete();
            });
        });
        /*

        */
    }

    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }
}
