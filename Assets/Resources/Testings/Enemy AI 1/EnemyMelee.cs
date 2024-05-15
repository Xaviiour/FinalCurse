using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    //Attack Param
    [SerializeField] private float attackCD;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    //Collider
    [SerializeField] private float collDist;
    [SerializeField] private BoxCollider2D coll;

    //Player
    [SerializeField] private LayerMask player;
    private float cdTimer = Mathf.Infinity;

    //Refs
    private Animator anim;
    private  BetaHealth h;
    private EnemyPatrol patrol;

    void Awake()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponentInParent<EnemyPatrol>();
    }

    void Update()
    {
        Debug.Log(InSight());

        cdTimer += Time.deltaTime;

        if (InSight())
        {
            if (cdTimer >= attackCD)
            {
                cdTimer = 0;
                anim.SetTrigger("Attack");
            }
        }

        if (patrol != null)
        {
            patrol.enabled = !InSight();
        }
    }

    private bool InSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * range * transform.localScale.x * collDist,
        new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z), 0, Vector2.left, 0, player);

        if (hit.collider != null)
        {
            //h = hit.transform.GetComponent<EnemyHealth>();
        }
            
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * range * transform.localScale.x * collDist,
            new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z));
    }

    /*private void PlayerHit()
    {
        if (InSight())
            h.Damage(damage);
    }*/
}
