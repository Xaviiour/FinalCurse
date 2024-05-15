using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PolygonCollider2D coll;
    public Animator anim;
    public Health h;

    [SerializeField] private LayerMask jumpGround;
    [SerializeField] private float speed = 7f;
    private bool isFlipped = false;
    private bool isOpen = false;
    

    public enum MovementState { idle, running, attack, hit}
    public MovementState state;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        UpdateAnimationState();
    }

    public void UpdateAnimationState()
    {
        MovementState state;
        float x = Input.GetAxisRaw("Horizontal");
        float a = Input.GetAxisRaw("Fire1");

        if (x == 1)
        {
            state = MovementState.running;
            Scale();
            isFlipped = false;
        }
        else if (x == -1)
        {
            state = MovementState.running;
            Scale();
            isFlipped = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (h.isHit == true)
        {
            state = MovementState.hit;
        }

        if (a == 1)
        {
            state = MovementState.attack;
        }

        anim.SetInteger("state", (int)state);
    }

    private void Scale()
    {
        if (rb.velocity.x < 0 && isFlipped == false)
        {
            Vector3 lTemp = transform.localScale;
            lTemp.x *= -1;
            transform.localScale = lTemp;
            isFlipped = true;
        }
        if (rb.velocity.x > 0 && isFlipped == true)
        {
            Vector3 lTemp = transform.localScale;
            lTemp.x *= -1;
            transform.localScale = lTemp;
            isFlipped = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Interactable" && isOpen == false)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.Play("open");
                isOpen = true;
            }
        }
    }
} 