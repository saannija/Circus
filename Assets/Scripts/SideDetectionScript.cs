using UnityEngine;

public class SideDetectionScript : MonoBehaviour
{
    DiceRollScript diceRollScript;
    PlayerController currentPlayer;
    private bool hasMoved = false;

    void Awake()
    {
        diceRollScript = FindObjectOfType<DiceRollScript>();
    }

    public void AssignCurrentPlayer(PlayerController player) 
    {
        currentPlayer = player;
        hasMoved = false;
    }

    private void OnTriggerStay(Collider sideCollider)
    {
        if (diceRollScript != null && diceRollScript.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            if (!hasMoved)
            {
                diceRollScript.isLanded = true;
                diceRollScript.diceFaceNum = sideCollider.name;

                if (currentPlayer != null && int.TryParse(diceRollScript.diceFaceNum, out int steps))
                {
                    Debug.Log(currentPlayer.gameObject.name + " rolled " + steps + " and is moving.");
                    currentPlayer.MovePlayer(steps);
                    hasMoved = true;
                }
                else
                {
                    Debug.LogError("No assigned player for movement!");
                }
            }
        }
    }
}
