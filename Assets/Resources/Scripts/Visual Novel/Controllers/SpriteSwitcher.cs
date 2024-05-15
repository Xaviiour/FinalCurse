using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpriteSwitcher : MonoBehaviour
{
    public bool isSwitched = false;
    public bool newScene = false;
    [SerializeField] private Image i1;
    [SerializeField] private Image i2;
    //[SerializeField] private Image i3;
    private Animator anim;
    [SerializeField] private GameObject aCont1;
    [SerializeField] private GameObject aCont2;
    private BottomBarController bBar;
    private Scene scene;

    //private enum BackgroundState { night, school, elric, castle, bus, field, osaka}
    //public BackgroundState bgState;

    public void Start()
    {
        anim = GetComponent<Animator>();
        aCont1 = GameObject.FindGameObjectWithTag("Background 2");
        aCont2 = GameObject.FindGameObjectWithTag("Background");
        scene = GetComponent<Scene>();
    }

    public void SwitchImage(Sprite sprite)
    {
        if (!isSwitched)
        {
            i2.sprite = sprite;
            anim.SetTrigger("SwitchFirst");
        }
        else
        {
            i1.sprite = sprite;
            anim.SetTrigger("SwitchSecond");
        }
        isSwitched = !isSwitched;
    }

    public void SwitchAnimator(RuntimeAnimatorController animC)
    {
        if(!newScene)
        {
            StartCoroutine(SwitchBG());
            anim.SetTrigger("SwitchFirst");
        }
        else
        {
            StartCoroutine(SwitchBG2());
            anim.SetTrigger("SwitchSecond");
        }
        newScene = !newScene;
    }

    public void SetImage(Sprite sprite)
    {
        if (!isSwitched)
        {
            i1.sprite = sprite;
        }
        else
        {
            i2.sprite = sprite;
        }
    }

    public void SyncImages()
    {
        if(!isSwitched)
        {
            i2.sprite = i1.sprite;
        }
        else
        {
            i1.sprite = i2.sprite;
        }
    }

    public Sprite GetImage()
    {
        if (!isSwitched)
        {
            return i1.sprite;
        }
        else
        {
            return i2.sprite;
        }
    }

    public IEnumerator SwitchBG()
    {
        yield return new WaitForSeconds(2f);
        var cont1 = aCont1.GetComponent<Animator>();
        cont1.runtimeAnimatorController = Resources.Load("Animations/Backgrounds/City Elric") as RuntimeAnimatorController;

    }

    public IEnumerator SwitchBG2()
    {
        yield return new WaitForSeconds(2f);
        var cont2 = aCont2.GetComponent<Animator>();
        cont2.runtimeAnimatorController = Resources.Load("Animations/Backgrounds/Castle Inside") as RuntimeAnimatorController;
        /*if (scene.buildIndex == 17)
        {
            cont2.runtimeAnimatorController = Resources.Load("Animations/Backgrounds/") as RuntimeAnimatorController;
        }*/
    }
}