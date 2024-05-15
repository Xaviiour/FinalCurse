using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    private Animator anim;
    //private ButtonSwitch bs;
    public GameObject button;
    private float delay = 0.5f;
    //private bool hasOpened = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            button.SetActive(true);
            if (Input.GetButton("Interact"))
            {
            anim.SetBool("open", true);
            Invoke("LoadScene", delay);
            }
        }
        
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //xhasOpened = false;
        anim.SetBool("open", false);
        button.SetActive(false);
    }
}