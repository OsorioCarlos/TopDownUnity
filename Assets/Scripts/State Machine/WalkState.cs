using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State<Enemy>
{
    [SerializeField] private float speed;
    [SerializeField] private float waitingTime;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask whatIsCollisible;

    private bool canMove;
    private float detectionRadius;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    public override void OnEnterState(Enemy controller)
    {
        base.OnEnterState(controller);
        detectionRadius = 0.2f;
        initialPosition = transform.position;
        controller.Animator.SetBool("walking", false);
        StartCoroutine(MovementIEnumarator());
    }

    public override void OnUpdateState()
    {

    }

    public override void OnExitState()
    {
        canMove = false;
        controller.Animator.SetBool("walking", false);
    }

    private IEnumerator MovementIEnumarator()
    {
        do
        {
            NextTargetPosition();
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            controller.Animator.SetBool("walking", false);
            yield return new WaitForSeconds(waitingTime);
        }
        while (canMove);
    }

    private void NextTargetPosition()
    {
        canMove = false;
        List<int> positions = new List<int>();
        int attempts = 0;

        while (!canMove && attempts < 10)
        {
            int random = Random.Range(0, 4);
            if (positions.Contains(random))
            {
                attempts++;
            }
            else
            {
                positions.Add(random);
                SetTargetPosition(random);
                canMove = CanMove();
            }
        }

        if (canMove)
        {
            controller.Animator.SetBool("walking", true);
        }
        else
        {
            targetPosition = transform.position;
        }

    }

    private void SetTargetPosition(int number)
    {
        if (number == 0)
        {
            targetPosition = transform.position + Vector3.left;
            controller.Animator.SetFloat("axisX", -1f);
            controller.Animator.SetFloat("axisY", 0f);
        }
        else if (number == 1)
        {
            targetPosition = transform.position + Vector3.right;
            controller.Animator.SetFloat("axisX", 1f);
            controller.Animator.SetFloat("axisY", 0f);
        }
        else if (number == 2)
        {
            targetPosition = transform.position + Vector3.up;
            controller.Animator.SetFloat("axisX", 0f);
            controller.Animator.SetFloat("axisY", 1f);
        }
        else
        {
            targetPosition = transform.position + Vector3.down;
            controller.Animator.SetFloat("axisX", 0f);
            controller.Animator.SetFloat("axisY", -1f);
        }
    }

    private bool CanMove()
    {
        if (Vector3.Distance(initialPosition, targetPosition) > (maxDistance - 2))
        {
            return false;
        }
        else
        {
            return !Physics2D.OverlapCircle(targetPosition, detectionRadius, whatIsCollisible);
        }
    }
}
