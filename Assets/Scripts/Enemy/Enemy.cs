using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float health;

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

    protected void DamageToThePlayer(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player.TryGetComponent(out SistemaVidas playerHealthSystem))
            {
                playerHealthSystem.RecibirDanho(damage);
            }
        }
    }

    protected void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }
}
