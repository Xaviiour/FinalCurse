using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    //Attack Params
    [SerializeField] private float attackCD;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    //Ranged Attack
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject[] throwables;

    //Colliders
    [SerializeField] private float collDist;
    [SerializeField] private BoxCollider2D coll;

    //Player
    [SerializeField] private LayerMask player;
    private float cdTimer = Mathf.Infinity;

    //Refs
    private Animator anim;
    private EnemyPatrol patrol;

    void Awake()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponentInParent<EnemyPatrol>();
    }

    void Update()
    {
        cdTimer = Time.deltaTime;

        if(InSight())
        {
            if(cdTimer >= attackCD)
            {
                cdTimer = 0;
                anim.SetTrigger("Attack");
            }
        }

        if(patrol != null)
        {
            patrol.enabled = !InSight();
        }
    }

    private void RangedAttack()
    {
        cdTimer = 0;
        throwables[FindThrow()].transform.position = throwPoint.position;
        throwables[FindThrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindThrow()
    {
        for (int i = 0; i < throwables.Length; i++)
        {
            if (!throwables[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool InSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * range * transform.localScale.x * collDist,
            new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z), 0, Vector2.left, 0, player);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * range * transform.localScale.x * collDist,
            new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z));
    }
}
