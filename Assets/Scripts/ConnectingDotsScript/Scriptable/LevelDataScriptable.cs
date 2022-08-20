using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Defines the <see cref="LevelDataScriptable" />.
/// </summary>
[CreateAssetMenu(fileName = "New Data", menuName = "LevelData/CreateData")]
public class LevelDataScriptable : ScriptableObject
{
  public int level;

  public int gridX;

  public int gridY;

  public int moves;

  public List<CircleDatas> circleDatas;

  public void SerializeData()
  {
    Debug.Log("Save");
    GameData data = new GameData()
    {
      Level = level,
      GridX = gridX,
      GridY = gridY,
      Moves = moves,
      CircleDatas = circleDatas
    };
    Debug.Log(data.ToString());
    SaveToJson(data);
  }

  /// <summary>
  /// The SaveToJson.
  /// </summary>
  /// <param name="data">The data<see cref="GameData"/>.</param>
  private void SaveToJson(GameData data)
  {
    var path = Application.streamingAssetsPath + "/Levels/Level " + level + ".txt";
    string saveData = JsonUtility.ToJson(data);
    Debug.Log(saveData);
    if (!File.Exists(path))
    {
      File.WriteAllText(path, saveData);
    }
  }

  /// <summary>
  /// The ResetValue.
  /// </summary>
  public void ResetValue()
  {
    level = 0;
    gridX = 0;
    gridY = 0;
    moves = 0;
    circleDatas.Clear();
    circleDatas = new List<CircleDatas>();
  }
}
