using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //Patrol Points
    [SerializeField] private Transform lEdge;
    [SerializeField] private Transform rEdge;

    //Enemy
    [SerializeField] private Transform enemy;

    //Movement
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingL;

    //Idle
    [SerializeField] private float idleTime;
    private float idleTimer;

    //Components
    [SerializeField] private Animator anim;

    void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        //anim.SetBool("walking", false);
    }

    void Update()
    {
        if(movingL)
        {
            if (enemy.position.x >= lEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        //anim.SetBool("walking", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleTime)
            movingL = !movingL;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        //anim.SetBool("walking", true);

        //enemy facing
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //enemy move
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
