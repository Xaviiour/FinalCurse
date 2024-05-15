using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Transform rc;
    public LayerMask rcM;
    public float rcL;
    public float aDist; //min distance
    public float mSpeed;
    public float timer; //cd timer for attack
    public Transform lLimit;
    public Transform rLimit;
    public Transform firePoint;
    public float fireRate;
    public GameObject projectile;

    //[SerializeField] private int damage = 10;
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //player / enemy
    private bool attacking;
    private bool inRange;
    private bool cd;
    private float intTimer;
    private float timeToFire;

    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!attacking)
        {
            Move();
        }

        if (!IOL() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            SelectTarget();
        }

        if(inRange)
        {
            hit = Physics2D.Raycast(rc.position, transform.right, rcL, rcM);
            RaycastDebugger();
        }

        if(hit.collider != null)
        {
            EnemyAI();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange == false)
        {
            StopAttack();
        }
    }



    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            target = coll.transform;
            inRange = true;
            Flip();
        }
    }

    private void EnemyAI()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > aDist)
        {
            StopAttack();
        }
        else if (aDist >= distance && cd == false)
        {
            Attack();
        }

        if(cd)
        {
            Cooldown();
            anim.SetBool("attack", false);
            //anim.SetBool("Walking", true);
        }
    }
    private void Move()
    {
        anim.SetBool("Walking", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targPos = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targPos, mSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        timer = intTimer;
        attacking = true;
        anim.SetBool("attack", true);
        anim.SetBool("Walking", false);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            cd = true;
            EnemyAI();
        }
    }

    public void Shoot()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }

    private void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cd && attacking)
        {
            cd = false;
            timer = intTimer;
        }
    }

    private void StopAttack()
    {
        cd = false;
        attacking = false;
        anim.SetBool("attack", false);
    }

    private void RaycastDebugger()
    {
        if(distance > aDist)
        {
            Debug.DrawRay(rc.position, transform.right * rcL, Color.red);
        }
        else if(aDist > distance)
        {
            Debug.DrawRay(rc.position, transform.right * rcL, Color.green);
        }
    }

    private void TrigCooling()
    {
        cd = true;
    }

    private bool IOL()
    {
        return transform.position.x > lLimit.position.x && transform.position.x < rLimit.position.x;
    }

    private void SelectTarget()
    {
        float distToL = Vector3.Distance(transform.position, lLimit.position);
        float distToR = Vector3.Distance(transform.position, rLimit.position);

        if (distToL > distToR)
        {
            target = lLimit;
        }
        else
        {
            target = rLimit;
        }
        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {

            rotation.y = 0;
        }
        transform.eulerAngles = rotation;
    }
}
