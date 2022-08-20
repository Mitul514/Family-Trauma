using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxData", menuName = "ScriptableObjects/BoxData")]
public class BoxData : ScriptableObject
{
    public int RowLength;
    public int ColLength;
    [Tooltip("Set the box size between 1 to 5 tiles")]
    [Range(0f, 5f)]
    public float TileSize;
    [Tooltip("Set the AOI between 1 to 5 tiles")]
    [Range(0, 5)]
    public int AdjScale;
}
