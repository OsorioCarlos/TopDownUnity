using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Components
    private Animator animator;

    // StateMachine
    protected State<Enemy> currentState;

    public Animator Animator { get => animator; }

    protected void ChangeState(State<Enemy> newState)
    {
        if (currentState != null)
        {
            currentState.OnExitState();
        }
        currentState = newState;
        currentState.OnEnterState(this);
    }

    protected void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }
}
