using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetaHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public int startH;
    [SerializeField] public int currentH;
    private Animator anim;
    public bool dead;
    public bool hit;
    private float cooldown = 0.5f;
    //public float damage = 5;

    [Header("iFrames")]
    [SerializeField] private float iFDur;
    [SerializeField] private int numOfF;
    private SpriteRenderer sr;

    [Header("Comps")]
    private BoxCollider2D bc;
    [SerializeField] private Behaviour[] comps;
    private bool inv;
    private EnemyLogic el;
    [SerializeField] private GameObject[] bosses;
    private AudioSource aS;
    public BossHealthUI bBar;

    [Header("Sounds")]
    [SerializeField] private AudioClip deathS;
    [SerializeField] private AudioClip hurtS;

    private void Awake()
    {
        currentH = startH;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        el = GetComponent<EnemyLogic>();
        aS = GetComponent<AudioSource>();
        
    }

    public void HitReset()
    {
        if (hit == true)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(cooldown);
        el.enabled = true;
        hit = false;
    }

    public void TakeDamage(int damage)
    {
        if (inv) return;
        currentH = Mathf.Clamp(currentH - damage, 0, startH);
        bBar.SetEnemyHP(currentH);

        if (currentH > 0)
        {
            anim.SetTrigger("hurt");
            //hit = true;
            if (!hit)
            {
                el.enabled = false;
                hit = true;
                HitReset();              
            }
            StartCoroutine(Invun());
            
            aS.PlayOneShot(hurtS);
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour comp in comps)
                    comp.enabled = false;
                foreach (GameObject boss in bosses)
                    boss.GetComponent<BossContinue>().SpawnChest();

                anim.SetTrigger("Death");

                dead = true;
                aS.PlayOneShot(deathS);
                el.enabled = false;
            }
        }
    }

    private IEnumerator Invun()
    {
        inv = true;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < numOfF; i++)
        {
            sr.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFDur / (numOfF * 2));
            sr.color = Color.white;
            yield return new WaitForSeconds(iFDur / (numOfF * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        inv = false;
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
