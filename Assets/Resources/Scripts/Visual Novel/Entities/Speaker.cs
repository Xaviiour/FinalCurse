using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")]
[System.Serializable]

public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color textColour;

    public List<Sprite> sprites;
    public SpriteController prefab;
    public Animator anim;
}
