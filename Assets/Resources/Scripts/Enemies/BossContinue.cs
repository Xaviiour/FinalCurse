using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContinue : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject chest;
    [SerializeField] private float waitTime;

    public void SpawnChest()
    {
        StartCoroutine(Delay());
    }

    public void SpawnImmediate()
    {
        chest.SetActive(true);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitTime); //dragon 1.6
        chest.SetActive(true);
    }
}