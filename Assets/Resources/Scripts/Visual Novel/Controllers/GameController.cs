using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public StoryScene currentScene;
    public BottomBarController bBar;
    public SpriteSwitcher bgController;
    public AudioController aController;
    [SerializeField] private GameObject aCont1;
    [SerializeField] private GameObject aCont2;

    private List<StoryScene> history = new List<StoryScene>();

    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE
    }

    void Start()
    {
        history.Add(currentScene);
        bBar.PlayScene(currentScene);
        bgController.SetImage(currentScene.bg);
        aCont1 = GameObject.FindGameObjectWithTag("Background 2");
        aCont2 = GameObject.FindGameObjectWithTag("Background");
    }

    void Update()
    {
        if(state == State.IDLE)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (bBar.IsCompleted())
                {
                    bBar.StopTyping();
                    if (bBar.IsLastSentence())
                    {
                        PlayScene(currentScene.nextScene);
                    }
                    else
                    {
                        bBar.PlaySentence();
                    }
                    /*if (bBar.IsLastSentence())
                    {
                        SceneManager.LoadSceneAsync("Scene 4-5");
                    }*/
                }
            }
            //figure out to make it so that its just a button rather than right click
            /*if(Input.GetMouseButtonDown(1))
            {
                if(bBar.IsFirstSentence())
                {
                    if(history.Count > 1)
                    {
                        bBar.StopTyping();
                        bBar.HideSprites();
                        history.RemoveAt(history.Count - 1);
                        StoryScene scene = history[history.Count - 1];
                        history.RemoveAt(history.Count - 1);
                        PlayScene(scene, scene.sentences.Count - 2, false);
                    }
                }
                else
                {
                    bBar.Back();
                }
            }*/
            /*if(Input.GetKeyDown(KeyCode.Escape))
            {
                List<int> historyIndicies = new List<int>();
                history.ForEach(scene =>
                {
                    historyIndicies.Add(this.data.scenes.IndexOf(scene));
                });
                SaveData data = new SaveData;
                {
                    sentence = bBar.GetSentenceIndex(), prevScenes = historyIndicies;
                }
                SaveManager.SaveGame(data);
                SceneManager.LoadScene(MainScreen);
            }*/
        }
        
    }

    public void PlayScene(StoryScene scene, bool isAnimated = true)
    {
        StartCoroutine(SwitchScene(scene, isAnimated));
        if(scene == null)
        {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public IEnumerator SwitchScene(StoryScene scene, bool isAnimated = true)
    {
        state = State.ANIMATE;
        currentScene = scene;
        //int sentenceIndex = -1;
        if(isAnimated)
        {
            bBar.Hide();
            yield return new WaitForSeconds(1f);
        }
        if(currentScene)
        {
            //history.Add(currentScene);
            //PlayAudio(currentScene.sentences[sentenceIndex + 1]);
            if(isAnimated)
            {
                bgController.SwitchImage(currentScene.bg);
                bgController.SwitchAnimator(currentScene.animC);
                yield return new WaitForSeconds(1f);
                bBar.ClearText();
                bBar.Show();
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            bgController.SetImage(scene.bg);
            bBar.ClearText();
        }
        bBar.PlayScene(scene, isAnimated);
        state = State.IDLE;
    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        aController.PlayAudio(sentence.music, sentence.sound);
    }
}
