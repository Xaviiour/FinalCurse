using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private SpriteSwitcher switcher;
    private Animator anim;
    private RectTransform rt;
    private CanvasGroup cGroup;

    private void Awake()
    {
        switcher = GetComponent<SpriteSwitcher>();
        anim = GetComponent<Animator>();
        rt = GetComponent<RectTransform>();
        cGroup = GetComponent<CanvasGroup>();
    }

    public void Set(Sprite sprite)
    {
        switcher.SetImage(sprite);
    }

    public void Show(Vector2 coords, bool isAnimated = true)
    {
        if(isAnimated)
        {
            anim.enabled = true;
            anim.SetTrigger("Show");
        }
        else
        {
            anim.enabled = false;
            cGroup.alpha = 1;
        }
        rt.localPosition = coords;
    }

    public void Hide(bool isAnimated = true)
    {
        if(isAnimated)
        {
            anim.enabled = true;
            switcher.SyncImages();
            anim.SetTrigger("Hide");
        }
        else
        {
            anim.enabled = false;
            cGroup.alpha = 0;
        }
    }

    public void Move(Vector2 coords, float speed, bool isAnimated = true)
    {
        if(isAnimated)
        {
            StartCoroutine(MoveC(coords, speed));
        }
        else
        {
            rt.localPosition = coords;
        }
    }

    private IEnumerator MoveC(Vector2 coords, float speed)
    {
        while(rt.localPosition.x != coords.x || rt.localPosition.y != coords.y)
        {
            rt.localPosition = Vector2.MoveTowards(rt.localPosition, coords, Time.deltaTime * 1000f * speed);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SwitchSprite(Sprite sprite, bool isAnimated = true)
    {
        if(switcher.GetImage() != sprite)
        {
            if(isAnimated)
            {
                switcher.SwitchImage(sprite);
            }
            else
            {
                switcher.SetImage(sprite);
            }

        }
    }
}
