using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameData
{
  public int Level;

  public int GridX;

  public int GridY;

  public int Moves;

  public List<CircleDatas> CircleDatas;
}

[Serializable]
public struct CircleDatas
{
  public string Name;

  public int Id;

  public Color Color;

  public int SourceX;

  public int SourceY;

  public int DestinationX;

  public int DestinationY;
}
