using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Tile;
    [SerializeField] private BoxData m_boxData;

    private float m_tileSize { get => m_boxData.TileSize; }
    private int m_adjScale { get => m_boxData.AdjScale; }
    private Dictionary<int[,], GameObject> m_TileDict;
    private int count = 0;

    public static BoxManager BMinstance { get; set; }

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
        //Creating Dynamic tilemap
        for (int row = 0; row < m_boxData.RowLength; row++)
        {
            for (int col = 0; col < m_boxData.ColLength; col++)
            {
                GameObject tile = Instantiate(m_Tile, gameObject.transform);
                count++;
                tile.name = "Box-" + count.ToString();
                float posX = col * m_tileSize;
                float posY = row * -m_tileSize;
                var start_x = m_boxData.ColLength * m_tileSize;
                var start_y = m_boxData.RowLength * m_tileSize;
                tile.transform.position = new Vector3(-start_x / 2 + m_tileSize / 2 + posX, 1, start_y / 2 - m_tileSize / 2 + posY);

                int[,] arr = new int[row, col];
                m_TileDict.Add(arr, tile);

                tile.GetComponent<BoxController>().SetData(count);
            }
        }
    }

    public void AdjacentsElements(int row, int column, int adjScale)
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
                        adjTileObj.GetComponent<BoxController>().RevealNumber();
                    }
                }
            }
        }
    }

    public void GetClickedObjects(GameObject obj)
    {
        foreach (var item in m_TileDict)
        {
            var val = item.Value.GetComponent<BoxController>().CurrVal;
            var currVal = obj.GetComponent<BoxController>().CurrVal;
            if (val == currVal)
            {
                Debug.Log("true ---- (" + item.Key.GetLength(0) + "," + item.Key.GetLength(1) + ")");
                AdjacentsElements(item.Key.GetLength(0), item.Key.GetLength(1), m_adjScale);
            }
        }
    }
}
