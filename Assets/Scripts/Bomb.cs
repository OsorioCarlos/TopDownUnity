using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private bool isLive;
    [SerializeField] private LayerMask queEsDanhable;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive) {
            anim.SetTrigger("Explode");
        }
    }

    public void Explode() {
        Debug.Log("Boom!");
        Collider2D[] colliderTocados = Physics2D.OverlapCircleAll(this.gameObject.transform.position, explosionRadius, queEsDanhable);
        foreach (Collider2D colliderAhead in colliderTocados)
        {
            if (colliderAhead.TryGetComponent(out SistemaVidas sistemaVidas))
            {
                sistemaVidas.RecibirDanho(damage);
                if (colliderAhead.TryGetComponent(out Player player))
                {
                    player.ShowDamage();
                }
            }
            else {
                Destroy(colliderAhead.gameObject);
            }
        }
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(this.gameObject);
    }

    public void TriggerBomb() {
        isLive = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.gameObject.transform.position, explosionRadius);
    }
    
}
