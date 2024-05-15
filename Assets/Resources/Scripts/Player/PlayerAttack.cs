using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    private bool attacking = false;
    private Animator anim;

    private float attackTime = 0.25f;
    private float timer = 0f;
    int UILayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            anim.Play("Attack");
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if(timer >= attackTime)
            {
                timer = 0;
                attacking = false;
            }
        }
    }
    private void Attack()
    {
        attacking = true;
    }

    public bool OverUI()
    {
        return OverUI(GetEventSystemRaycastResults());
    }

    private bool OverUI(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
