using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public PlayerMovement pm;
    public UIStatBar hBar;
    private UIStatBar dBar;
    private AudioSource aS;

    public GameObject spawner;
    private GameObject player;
    public GameObject[] enemyScript;
    public GameObject Chest;

    [SerializeField] private AudioClip deathS;
    [SerializeField] private AudioClip hurtS;

    private float totalDistance;
    private int MaxH = 100;
    public bool isDead = false;
    public bool isHit = false;
    public float cooldown = 0.1f;
    [SerializeField] public int health = 100;
    [SerializeField] public int damage = 50;
    [SerializeField] public float dist = 333f;

    private void Update()
    {
        Debug.Log(isDead);
        Debug.Log(health);
    }

    public void HitReset()
    {
        if (isHit == true)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(cooldown);
        isHit = false;
    }

    public void Start()
    {
        spawner = GameObject.Find("Enemy Spawner");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        aS = GetComponent<AudioSource>();
    }

    public void SetHealth(int maxH, int health)
    {
        this.MaxH = maxH;
        this.health = health;
    }

    public void SetDistance(float trav)
    {
        this.dist = trav;
    }

    public void Damage(int amount)
    {
        this.health -= amount;
        isHit = true;
        aS.PlayOneShot(hurtS);
        HitReset();
        hBar.SetValue(health);

        if (this.health <= 0)
        {
            spawner.SetActive(false);
            isDead = true;
            Die();
            
        }
    }

    public void Die()
    {
        //isDead = true;
        anim = GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
        aS.PlayOneShot(deathS);
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        enemyScript = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject tagged in enemyScript)
        {
            tagged.GetComponent<EnemyLogic>().enabled = false;
            tagged.GetComponent<Animator>().enabled = false;
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Enemy" || collider.transform.tag == "Boss")
        {
            if (collider.GetComponent<Health>() != null)
            {
                collider.GetComponent<Health>().Damage(damage);
            }
        if (isDead == true)
            {
                anim.SetTrigger("Death");
            }
        }
    }
}
