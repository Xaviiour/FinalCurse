using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }

    void Update()
    {
        if (hit)
            return;
        float mSpeed = speed * Time.deltaTime;
        transform.Translate(mSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > rTime)
            gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        //base.OnTriggerEnter2D(collision);
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("break");
        else
            gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
