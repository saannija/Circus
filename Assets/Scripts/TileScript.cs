using UnityEngine;

public class TileScript : MonoBehaviour
{
    public enum TileType { Normal, Good, Bad }

    [Header("Tile Settings")]
    public TileType tileType = TileType.Normal;
    public int moveAmount = 0;

    private void Start()
    {
        if (tileType != TileType.Good && tileType != TileType.Bad)
        {
            tileType = TileType.Normal;
            moveAmount = 0;
        }
    }

    public int GetMoveAmount()
    {
        return tileType == TileType.Normal ? 0 : moveAmount;
    }
}
