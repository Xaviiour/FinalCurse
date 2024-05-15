using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataHolder", menuName = "Data/NewDataHolder")]
[System.Serializable]
public class DataHolder : ScriptableObject
{
    public List<StoryScene> scenes;
}
