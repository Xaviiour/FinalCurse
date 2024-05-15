/******************************************************************************************
 * Name: UIStatBar.cs
 * Created by: Jeremy Voss
 * Created on: 01/15/2019
 * Last Modified: 01/16/2019
 * Description:
 * A simple script on the Health, Magic, and Energy bars that will take the current
 * value and maximum value and translate it into a slider value ranging 0 to 1. So if my
 * character takes damage I can do my damage calculations for to calculate my new player
 * health.  Then call the SetValue method of this script and pass in my current health and
 * maximum health and it will update the slider accordingly.
 ******************************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatBar : MonoBehaviour
{
    public Slider slider;
    public Slider distance;
    public Health h;
    public BetaHealth bh;
    public GameObject Chest;
    public GameObject player;
    public float totalDistance;


    private void Awake()
    {
        totalDistance = Mathf.Abs(Chest.transform.position.x - player.transform.position.x);
        h = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player");
        slider = GetComponent<Slider>();
        slider.maxValue = h.health;
        slider.value = h.health;
        distance.maxValue = 1;
    }
    private void Update()
    {
        distance.value = player.transform.position.x / totalDistance;
    }
    public void SetValue(int hp)
    {
        slider.value = hp;
    }
    public void SetDistance(float dist)
    {
        distance.value = dist;
    }
}

