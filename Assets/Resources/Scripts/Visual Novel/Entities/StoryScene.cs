using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Story Scene")]
[System.Serializable]

public class StoryScene : ScriptableObject
{
    public List<Sentence> sentences;
    public Sprite bg;
    public StoryScene nextScene;
    public RuntimeAnimatorController animC;
    //public List<SceneSequence> scenes;

    [System.Serializable]
    public struct SceneSequence
    {
        public StoryScene sceneIndex;
    }

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker;
        public List<Action> actions;

        public AudioClip music;
        public AudioClip sound;


        [System.Serializable]
        public struct Action
        {
            public Speaker speaker;
            public int spriteIndex;
            public Type aType;
            public Vector2 coords;
            public float mSpeed;

            public enum Type
            {
                NONE, APPEAR, MOVE, DISAPPEAR
            }
        }
    }
}
