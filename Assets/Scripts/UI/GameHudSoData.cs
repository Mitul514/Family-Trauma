using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hud Data", menuName = "ScriptableObjects/Hud Data")]
public class GameHudSoData : ScriptableObject
{
    public int id;
    public int timeInSecs;
    public int totalTimeInSecs;
    public int meterValue;
}
