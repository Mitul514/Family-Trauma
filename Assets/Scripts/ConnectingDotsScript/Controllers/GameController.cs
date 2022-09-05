using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private GameObject _circlePrefab;

    [SerializeField] private GameObject _borderclonePrefab;

    private float tileSize = 1f;

    private GameData gameData;

    private List<GameObject> TilesList;
    private List<GameObject> BordersList;

    private bool isInitialized;

    private void Start()
    {
        TilesList = new List<GameObject>();
        BordersList = new List<GameObject>();
        gameData = new GameData();
        ConnectingDotsManager.Instance.GameTrigger += CreateGame;
        ConnectingDotsManager.Instance.GameOverTrigger += ShowGameOver;
    }

    private void OnDestroy()
    {
        ConnectingDotsManager.Instance.GameTrigger -= CreateGame;
        ConnectingDotsManager.Instance.GameOverTrigger -= ShowGameOver;
    }

    private void CreateGame(string level)
    {
        if (!isInitialized)
        {
            isInitialized = true;
            var address = Application.streamingAssetsPath + "/Levels/" + level + ".txt";
            loadJsonFile(address);
            IEnumerator gameDraw = DrawGame(gameData);
            StartCoroutine(gameDraw);
        }
    }

    public void loadJsonFile(string path)
    {
        if (File.Exists(path))
        {
            string saveData = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(saveData);
        }
    }

    private IEnumerator DrawGame(GameData gameData)
    {
        CreateOutsideWall(gameData.GridX, gameData.GridY);
        CreateGrid(gameData);
        ParentPositionUpdate();
        yield return new WaitForSeconds(1f);
    }

    private void CreateGrid(GameData data)
    {
        ConnectingDotsManager.Instance.TotalMoveCount = data.Moves;
        ConnectingDotsManager.Instance.MoveCount = data.Moves;
        for (int row = 0; row < data.GridX; row++)
        {
            for (int col = 0; col < data.GridY; col++)
            {
                var tile = Instantiate(_tilePrefab, gameObject.transform);
                var posX = row * tileSize;
                var posY = col * -tileSize;
                tile.transform.position = new Vector3(posX, posY, _tilePrefab.transform.position.z);
                TilesList.Add(tile);
                CreateDots(row, col, tile.transform, data.CircleDatas);
            }
        }
    }

    private void CreateOutsideWall(int posX, int posY)
    {
        for (int i = -1; i < posX + 1; i++)
        {
            for (int j = -1; j < posY + 1; j++)
            {
                if (i == -1 || i == posX)
                    BorderSpawner(i, j);
                else if (j == -1 || j == posY)
                    BorderSpawner(i, j);
            }
        }
    }

    private void CreateDots(int row, int col, Transform parentPos, List<CircleDatas> circleData)
    {
        foreach (var item in circleData)
        {
            if (item.DestinationX == row && item.DestinationY == col ||
              item.SourceX == row && item.SourceY == col)
            {
                var circleClone = Instantiate(_circlePrefab, parentPos);
                circleClone.name = item.Name;
                circleClone.GetComponent<SpriteRenderer>().color = item.Color;
                circleClone.GetComponent<PlayerService>().SetData(item);
            }
        }
    }

    private void ParentPositionUpdate()
    {
        var refX = tileSize * gameData.GridX;
        var refY = tileSize * gameData.GridY;
        transform.position = new Vector2(-refX / 2 + tileSize / 2, refY / 2 - tileSize / 2);
    }

    private void BorderSpawner(int i, int j)
    {
        var border = Instantiate(_borderclonePrefab, gameObject.transform);
        border.name = "Border_" + (i + j);
        var bosX = i * tileSize;
        var bosY = j * -tileSize;
        BordersList.Add(border);
        border.transform.position = new Vector3(bosX, bosY, transform.position.z);
    }

    private void ShowGameOver()
    {
        if (TilesList != null && BordersList != null)
        {
            foreach (var item in TilesList)
            {
                Destroy(item);
            }
            TilesList.Clear();
            
            foreach (var item in BordersList)
            {
                Destroy(item);
            }
            BordersList.Clear();

            transform.position = Vector2.zero;
        }
    }
}
