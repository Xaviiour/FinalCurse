using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    //public BetaHealth h;
    //public GameObject player;
    public int damage;

    /*private void Awake()
    {
        h = GetComponent<BetaHealth>();
    }*/

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            coll.GetComponent<Health>().Damage(damage);
        }
    }
}