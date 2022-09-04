using System;
using UnityEngine;

[Serializable]
public struct Dialogue
{
    public string Name;
    [TextArea(5, 5)]
    public string[] Speech;
};


[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue Data")]
public class DialogueScriptable : ScriptableObject
{
    public string id;
    public Dialogue[] dialogue;
}
