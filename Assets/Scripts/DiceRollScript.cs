using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceRollScript : MonoBehaviour
{

    Rigidbody body;
    Vector3 position;
    [SerializeField]private float maxRandForceVal = 3000, startRollingForce = 1000;
    float forceX, forceY, forceZ;
    public string diceFaceNum;
    public bool isLanded = false;
    public bool firstThrow = false;

 
    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        position = transform.position;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }

    public void RollDice()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        body.isKinematic = true;

        transform.rotation = Quaternion.Euler(Random.Range(0, 10), Random.Range(0, 360), Random.Range(0, 10));

        body.isKinematic = false;

        float upForce = isLanded ? Random.Range(300, startRollingForce) : Random.Range(800, startRollingForce);
        forceX = Random.Range(500, maxRandForceVal / 2); 
        forceY = Random.Range(500, maxRandForceVal / 2);
        forceZ = Random.Range(500, maxRandForceVal / 2);

        body.AddForce(Vector3.up * upForce);
        body.AddTorque(forceX, forceY, forceZ);
    }

    public void ResetDice()
    {
        body.isKinematic = true;
        firstThrow = false;
        isLanded = false;
        transform.position = position;
    }

    private void Update()
    {
        if (body != null)
        {
            PlayerController mainPlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();

            if (mainPlayer != null && mainPlayer.isMoving) return;

            if (Input.GetMouseButton(0) && isLanded || Input.GetMouseButton(0) && !firstThrow)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                    {
                        if (!firstThrow)
                        {
                            firstThrow = true;
                        }

                        if (mainPlayer != null)
                        {
                            SideDetectionScript sideDetection = FindObjectOfType<SideDetectionScript>();
                            if (sideDetection != null)
                            {
                                sideDetection.AssignCurrentPlayer(mainPlayer);
                            }
                        }
                        else
                        {
                            Debug.LogError("Main player not found!");
                        }

                        RollDice();
                    }
                }
            }
        }
    }
}
