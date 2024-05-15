using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barTxt;
    public TextMeshProUGUI pNameTxt;

    public int sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Animator anim;
    private bool isHidden = false;
    //public int sceneIndex = -1;
    //public List<StoryScene> scenes;

    private Dictionary<Speaker, SpriteController> sprites;
    public GameObject sPrefab;
    private Coroutine typing;
    public GameObject arrow;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Awake()
    {
        sprites = new Dictionary<Speaker, SpriteController>();
        anim = GetComponent<Animator>();
    }

    /*public int GetSentenceIndex()
    {
        return sentenceIndex;
    }

    public void SetSentenceIndex(int sentenceIndex)
    {
        this.sentenceIndex = sentenceIndex;
    }*/

    public void Hide()
    {
        if (!isHidden)
        {
            //if(Input.GetButtonDown())
            anim.SetTrigger("Dialog Hide");
            isHidden = true;
        }
    }

    public void Show()
    {
        anim.SetTrigger("Dialog Show");
        isHidden = false;
    }

    public void ClearText()
    {
        barTxt.text = "";
    }

    public void PlayScene(StoryScene scene, bool isAnimated = true)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlaySentence(isAnimated);
    }

    public void PlaySentence(bool isAnimated = true)
    {
        typing = StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        pNameTxt.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        pNameTxt.color = currentScene.sentences[sentenceIndex].speaker.textColour;
        ActSpeakers(isAnimated);
    }

    /*public void PlayNextSentence(bool isAnimated = true)
    {
        sentenceIndex++;
        PlaySentence(isAnimated);
    }*/

    /*public void Back()
    {
        //sentenceIndex--;
        StopTyping();
        HideSprites();
        PlaySentence(false);
    }*/

    public bool IsCompleted()
    {
        return state == State.COMPLETED;

    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public bool IsFirstSentence()
    {
        return sentenceIndex == 0;
    }

    public void StopTyping()
    {
        state = State.COMPLETED;
        StopCoroutine(typing);
    }

    public void HideSprites()
    {
        while (sPrefab.transform.childCount > 0)
        {
            DestroyImmediate(sPrefab.transform.GetChild(0).gameObject);
        }
        sprites.Clear();
    }

    /*public bool IsLastScene()
    {
        return sceneIndex + 1 == currentScene.scenes.Count;
    }*/

    private IEnumerator TypeText(string text)
    {
        barTxt.text = "";
        state = State.PLAYING;
        arrow.SetActive(false);
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barTxt.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                barTxt.text = text;
                state = State.COMPLETED;
                break;
            }
            else if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
        if (barTxt.text == text)
        {
            arrow.SetActive(true);
            if (IsLastSentence())
            {
                arrow.SetActive(false);
            }
        }
    }

    private void ActSpeakers(bool isAnimated = true)
    {
        List<StoryScene.Sentence.Action> actions = currentScene.sentences[sentenceIndex].actions;
        for (int i = 0; i < actions.Count; i++)
        {
            ActSpeaker(actions[i], isAnimated);
        }
    }

    private void ActSpeaker(StoryScene.Sentence.Action action, bool isAnimated = true)
    {
        SpriteController cont;
        if (!sprites.ContainsKey(action.speaker))
        {
            cont = Instantiate(action.speaker.prefab.gameObject, sPrefab.transform)
                .GetComponent<SpriteController>();
            sprites.Add(action.speaker, cont);
        }
        else
        {
            cont = sprites[action.speaker];
        }
        switch (action.aType)
        {
            case StoryScene.Sentence.Action.Type.APPEAR:
                cont.Set(action.speaker.sprites[action.spriteIndex]);
                cont.Show(action.coords, isAnimated);
                return;
            case StoryScene.Sentence.Action.Type.MOVE:
                cont.Move(action.coords, action.mSpeed, isAnimated);
                break;
            case StoryScene.Sentence.Action.Type.DISAPPEAR:
                cont.Hide(isAnimated);
                break;
        }
            cont.SwitchSprite(action.speaker.sprites[action.spriteIndex], isAnimated);
    }
}