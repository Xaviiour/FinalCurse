using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 50;
    private BoxCollider2D area;

    private void Update()
    {
        area = GetComponent<BoxCollider2D>();
        if (Input.GetButton("Fire1"))
        {
            area.enabled = true;
            StartCoroutine(AttackTime());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Enemy")
        {
            if (collider.GetComponent<BetaHealth>() != null)
            {
                BetaHealth eh = collider.GetComponent<BetaHealth>();
                eh.TakeDamage(damage);
            }
        }
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(0.5f);
        area.enabled = false;
    }
}