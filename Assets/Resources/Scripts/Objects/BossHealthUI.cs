using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public Slider bhSlider;
    public BetaHealth bh;
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        bh = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BetaHealth>();
        boss = GameObject.FindGameObjectWithTag("Enemy");
        bhSlider = GetComponent<Slider>();
        bhSlider.maxValue = bh.currentH;
        bhSlider.value = bh.currentH;
    }

    public void SetEnemyHP(int hp)
    {
        bhSlider.value = hp;
    }
}