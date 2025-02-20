using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public Transform startTile;
    public Transform finishTile;
    public List<Transform> boardTiles = new List<Transform>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PopulateTiles();
    }

    private void PopulateTiles()
    {
        boardTiles.Clear();

        if (startTile) boardTiles.Add(startTile);

        bool reverseRow = false;

        foreach (Transform row in transform)
        {
            List<Transform> tempRowTiles = new List<Transform>();

            foreach (Transform tile in row)
            {
                if (tile != startTile && tile != finishTile)
                {
                    tempRowTiles.Add(tile);

                    TileScript tileScript = tile.GetComponent<TileScript>();
                    if (!tileScript)
                    {
                        tileScript = tile.gameObject.AddComponent<TileScript>();
                    }
                }
            }

            if (reverseRow)
            {
                tempRowTiles.Reverse();
            }

            boardTiles.AddRange(tempRowTiles);
            reverseRow = !reverseRow;
        }

        if (finishTile) boardTiles.Add(finishTile);
    }

    public Transform GetTilePosition(int index)
    {
        if (index >= 0 && index < boardTiles.Count)
        {
            return boardTiles[index];
        }
        return null;
    }
}
