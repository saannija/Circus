using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int currentTileIndex = 0;
    public float moveSpeed = 5f;
    public bool isMoving = false;

    void Start()
    {
        Debug.Log(gameObject.name + " has PlayerController attached!");
    }
    
    public void MovePlayer(int steps)
    {
        Debug.Log(gameObject.name + " is moving " + steps + " steps!");
        StartCoroutine(MoveToTile(currentTileIndex + steps));
    }

    private IEnumerator MoveToTile(int targetIndex, bool isSpecialMove = false)
    {
        isMoving = true;

        if (isSpecialMove)
        {
            Transform targetTile = BoardManager.Instance.GetTilePosition(targetIndex);
            if (targetTile != null)
            {
                Vector3 startPos = transform.position;
                Vector3 hoverMidPoint = new Vector3(targetTile.position.x, transform.position.y + 0.02f, targetTile.position.z);
                Vector3 endPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);

                float hoverTime = 1.2f;
                float elapsedTime = 0f;

                while (elapsedTime < hoverTime / 2)
                {
                    transform.position = Vector3.Lerp(startPos, hoverMidPoint, (elapsedTime / (hoverTime / 2)));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                elapsedTime = 0f;

                while (elapsedTime < hoverTime / 2)
                {
                    transform.position = Vector3.Lerp(hoverMidPoint, endPos, (elapsedTime / (hoverTime / 2)));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                transform.position = endPos;
                currentTileIndex = targetIndex;
            }
        }
        else
        {
            while (currentTileIndex < targetIndex && currentTileIndex < BoardManager.Instance.boardTiles.Count - 1)
            {
                currentTileIndex++;
                Transform targetTile = BoardManager.Instance.GetTilePosition(currentTileIndex);

                if (targetTile != null)
                {
                    Vector3 startPos = transform.position;
                    Vector3 endPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);
                    float elapsedTime = 0f;
                    float journeyTime = 0.5f;

                    while (elapsedTime < journeyTime)
                    {
                        transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / journeyTime));
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }

                    transform.position = endPos;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        Transform landedTile = BoardManager.Instance.GetTilePosition(currentTileIndex);
        TileScript tileScript = landedTile.GetComponent<TileScript>();

        if (tileScript != null && tileScript.tileType != TileScript.TileType.Normal)
        {
            int extraMove = tileScript.GetMoveAmount();
            Debug.Log(gameObject.name + " landed on a " + tileScript.tileType + " tile! Hovering to " + extraMove + " extra steps.");
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(MoveToTile(currentTileIndex + extraMove, true));
        }
        else
        {
            isMoving = false;
        }

        if (currentTileIndex >= BoardManager.Instance.boardTiles.Count - 1)
        {
            currentTileIndex = BoardManager.Instance.boardTiles.Count - 1;
            Debug.Log(gameObject.name + " has reached the final tile!");
            isMoving = false;
        }
    }
}
