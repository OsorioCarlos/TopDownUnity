using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Down,
    Up,
    Left,
    Right
}

public class StalkState : State<Enemy>
{
    [SerializeField] private Direction initialDirection;

    private bool setDirection = true;

    public override void OnEnterState(Enemy controller)
    {
        base.OnEnterState(controller);
        if (setDirection)
        {
            SetAxisDirection();
        }
        controller.Animator.SetBool("walking", false);
        controller.Animator.SetBool("sleeping", true);
    }

    public override void OnUpdateState()
    {

    }

    public override void OnExitState()
    {
        controller.Animator.SetBool("sleeping", false);
    }

    private void SetAxisDirection()
    {
        if (initialDirection == Direction.Right)
        {
            controller.Animator.SetFloat("axisX", 1);
        }
        else if (initialDirection == Direction.Left)
        {
            controller.Animator.SetFloat("axisX", -1);
        }
        else if (initialDirection == Direction.Up)
        {
            controller.Animator.SetFloat("axisY", 1);
        }
        else
        {
            controller.Animator.SetFloat("axisY", -1);
        }
        setDirection = false;
    }
}
