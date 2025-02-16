using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask whatIsCollisible;

    private float inputH;
    private float inputV;
    private bool isMoving;
    private Vector3 destinationPoint;
    private Vector3 interactionPoint;
    private Vector3 lastInput;
    private Collider2D colliderAhead;
    private Animator animator;
    private bool interacting;

    public bool Interacting { get => interacting; set => interacting = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        Movement();
    }

    private void CheckInputs()
    {
        if (inputV == 0)
        {
            inputH = Input.GetAxisRaw("Horizontal");
        }
        if (inputH == 0)
        {
            inputV = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        //TODO: Check if can atk
        LanzarAtaque();
    }

    private void LanzarAtaque()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
    }

    private void Movement()
    {
        if (!interacting && !isMoving && (inputH != 0 || inputV != 0))
        {
            animator.SetBool("walking", true);
            animator.SetFloat("inputH", inputH);
            animator.SetFloat("inputV", inputV);

            lastInput = new Vector3(inputH, inputV, 0);
            destinationPoint = transform.position + lastInput;
            interactionPoint = destinationPoint;
            colliderAhead = UseOverlap();
            if (!colliderAhead)
            {
                StartCoroutine(MovementIEnumarator());
            }
        }
        else if (inputH == 0 && inputV == 0)
        {
            animator.SetBool("walking", false);
        }
    }

    IEnumerator MovementIEnumarator()
    {
        isMoving = true;
        while (transform.position != destinationPoint) {
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, speed * Time.deltaTime);
            yield return null;
        }
        interactionPoint = transform.position + lastInput;
        isMoving = false;
    }

    private Collider2D UseOverlap()
    {
        return Physics2D.OverlapCircle(interactionPoint, interactionRadius, whatIsCollisible);
    }

    private void Interact()
    {
        colliderAhead = UseOverlap();
        if (colliderAhead)
        {
            if (colliderAhead.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact();
            }
        }
    }

    //Used as animation event
    private void Attack()
    {
        colliderAhead = UseOverlap();
        if (colliderAhead)
        {
            if (colliderAhead.TryGetComponent(out SistemaVidas sistemaVidas))
            {
                sistemaVidas.RecibirDanho(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(interactionPoint, interactionRadius);
    }
}
