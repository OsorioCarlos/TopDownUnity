using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetAnimator(GetComponent<Animator>());
        ChangeState(GetComponent<WalkState>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageToThePlayer(collision);
    }
}
