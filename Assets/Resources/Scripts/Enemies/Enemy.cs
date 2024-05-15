using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    public LayerMask enemyLayer;

    [SerializeField] private int damage = 10;
    [SerializeField] private float speed = 1.5f;
    //[SerializeField] private EnemyData data;
    //[SerializeField] private float seconds = 3f;

    public bool isHit = false, isDead = false, flipped = false;
    public float hitTimer = 0, deadTimer = 0, aRange = .35f;
    public Vector2 hitDir = Vector2.zero;
    int exeNumber = 0;

    private Rigidbody2D rb;
    private Vector2 screen;
    private SpriteRenderer sprite;
    private Animator anim;
    private Health h;
    //public EnemyAttack ea;

    private GameObject player;

    private enum MovementState { idle, walking, attack }
    private MovementState state = MovementState.walking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationState();
        Swarm();
    }

    private void UpdateAnimationState()
    {
        if (this.transform.tag == "Enemy")
        {
            if (player.transform.position.x > sprite.transform.position.x)
            {
                state = MovementState.walking;
                sprite.flipX = false;
            }
            else if (player.transform.position.x < sprite.transform.position.x)
            {
                state = MovementState.walking;
                sprite.flipX = true;
            }

            if (sprite.transform.position.x <= player.transform.position.x)
            {
                state = MovementState.attack;
            }

            anim.SetInteger("state", (int)state);

        }
        else
        {
            if (player.transform.position.x > sprite.transform.position.x)
            {
                sprite.flipX = true;
            }
            else if (player.transform.position.x < sprite.transform.position.x)
            {
                sprite.flipX = false;
            }

            anim.SetInteger("state", 0);
        }
    }

    public void StartAttack(Collider2D collider)
    {
        //GetComponent<AudioSource>().PlayOneShot(aSwing);

        exeNumber++;
        if (exeNumber == 1)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.GetChild(0).position, aRange, enemyLayer);

            if(hitTargets.Length > 0 && hitTargets[0] != null)
            {
                anim.SetBool("hasTarget", true);
                collider.GetComponent<Health>().Damage(damage);
            }
        }
    }

    public void Reset()
    {
        exeNumber = 0;
    }

    /*private void SetEnemyValues()
    {
        //GetComponent<EnemyHealth>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }*/

    public void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + 1.6f, transform.position.y), speed * Time.deltaTime);
        if (sprite.flipX == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + -1.2f, transform.position.y), speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            anim.SetBool("hasTarget", true);
            collision.GetComponent<Health>().Damage(damage);
        }
    }
}