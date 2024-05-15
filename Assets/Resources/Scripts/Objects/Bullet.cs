using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;
    private AudioSource aS;
    public AudioClip shoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aS = GetComponent<AudioSource>();
        Destroy(gameObject, lifeTime);
        aS.PlayOneShot(shoot);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;

    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
