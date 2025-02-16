using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungus : Enemy
{
    // State
    private StalkState stalkState;
    private WalkState walkState;

    // Start is called before the first frame update
    void Start()
    {
        SetAnimator(GetComponent<Animator>());
        stalkState = GetComponent<StalkState>();
        walkState = GetComponent<WalkState>();
        ChangeState(stalkState);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Animator.GetBool("sleeping") && collision.CompareTag("Player"))
        {
            Animator.SetBool("sleeping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Animator.GetBool("sleeping") && collision.CompareTag("Player"))
        {
            ChangeState(stalkState);
        }
    }

    public void ChangeToWalkState()
    {
        ChangeState(walkState);
    }
}
