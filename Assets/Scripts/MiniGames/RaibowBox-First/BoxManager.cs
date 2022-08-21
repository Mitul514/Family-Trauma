using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Tile;
    [SerializeField] private BoxData m_boxData;
    [SerializeField] private NonRepeatedRandomNumbers nonRepeatedRandomNumbers;

    private float m_tileSize { get => m_boxData.TileSize; }
    private int m_adjScale { get => m_boxData.AdjScale; }
    private Dictionary<int[,], GameObject> m_TileDict;
    private int count = 0;

    public Action<int> OnChanceUse;
    public Action OnMineFound, OnWinOrLose;

    public BoxData BoxData { get => m_boxData; }
    public static BoxManager BMinstance { get; set; }

    int[] arrBoxes;
    int[] minesArr;

    private void Awake()
    {
        if (BMinstance == null)
        {
            BMinstance = this;
        }
    }

    private void Start()
    {
        m_TileDict = new Dictionary<int[,], GameObject>();
        CreateGrid();
    }

    private void CreateGrid()
    {
        arrBoxes = new int[m_boxData.RowLength * m_boxData.ColLength];
        minesArr = new int[m_boxData.minesCount];

        for (int i = 0; i < arrBoxes.Length; i++)
        {
            arrBoxes[i] = i + 1;
        }

        minesArr = nonRepeatedRandomNumbers.GetRandomNumber(arrBoxes, minesArr);

        //Creating Dynamic tilemap
        for (int row = 0; row < m_boxData.RowLength; row++)
        {
            for (int col = 0; col < m_boxData.ColLength; col++)
            {
                GameObject tile = Instantiate(m_Tile, nonRepeatedRandomNumbers.gameObject.transform);
                count++;
                tile.name = "Box-" + count.ToString();
                float posX = col * m_tileSize;
                float posY = row * -m_tileSize;
                var start_x = m_boxData.ColLength * m_tileSize;
                var start_y = m_boxData.RowLength * m_tileSize;
                tile.transform.position = new Vector3(-start_x / 2 + m_tileSize / 2 + posX, 1, start_y / 2 - m_tileSize / 2 + posY);

                int[,] arr = new int[row, col];
                m_TileDict.Add(arr, tile);

                tile.GetComponent<BoxController>().SetData(count, minesArr);
            }
        }
    }

    public IEnumerator AdjacentsElements(int row, int column, int adjScale)
    {
        foreach (var kvp in m_TileDict)
        {
            //ROW x COLUMN indexes
            int rHorInd = kvp.Key.GetLength(0);
            int cVerInd = kvp.Key.GetLength(1);

            for (int j = row - adjScale; j <= row + adjScale; j++)
            {
                for (int i = column - adjScale; i <= column + adjScale; i++)
                {
                    if (i >= 0 && j >= 0 && i < m_boxData.ColLength && j < m_boxData.RowLength &&
                        (rHorInd == j && cVerInd == i))
                    {
                        var adjTileObj = kvp.Value;
                        var boxController = adjTileObj.GetComponent<BoxController>();
                        boxController.RevealNumber();
                        if (boxController.CheckIfBoxIsMine(minesArr))
                        {
                            OnMineFound?.Invoke();
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(1f);
        Debug.Log("Check Win/Lose");
        OnWinOrLose?.Invoke();
    }

    public void GetClickedObjects(GameObject obj)
    {
        OnChanceUse?.Invoke(m_boxData.totalChances);
        foreach (var item in m_TileDict)
        {
            var val = item.Value.GetComponent<BoxController>().CurrVal;
            var currVal = obj.GetComponent<BoxController>().CurrVal;
            if (val == currVal)
            {
                StartCoroutine(AdjacentsElements(item.Key.GetLength(0), item.Key.GetLength(1), m_adjScale));
            }
        }
    }
}
