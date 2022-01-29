using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    Vector3 targetPosition;
    public float moveSpeed;
    int positionIndex = 1;
    public float pauseOnArrivalTime = 1;
    public bool enabled = true;
    bool delay = false;
    private void Start()
    {
        //positions.Add(transform.position);
        //inicijaliziraj prvu poziciju
        targetPosition = positions[positionIndex];
    }
    private void Update()
    {
        if (transform.position != targetPosition && delay == false && enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if (transform.position == targetPosition && delay == false && enabled)
        {
            StartCoroutine(ChangePositionWithDelay());
        }
        
    }

    public void EnableMovement()
    {
        enabled = true;
    }
    public void DisableMovement()
    {
        StartCoroutine(StopMoving());
    }

    IEnumerator StopMoving()
    {
        yield return new WaitUntil(() => transform.position == targetPosition);
        enabled = false;
    }


    IEnumerator ChangePositionWithDelay()
    {
        delay = true;
        yield return new WaitForSeconds(pauseOnArrivalTime);
        positionIndex++;
        if (positionIndex == positions.Count)
        {
            positionIndex = 0;
        }
        targetPosition = positions[positionIndex];
        delay = false;
    }


    private void OnDrawGizmos()
    {
        foreach (var e in positions)
        {
            if (e == targetPosition)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(e, 0.2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.SetParent(transform);
            Debug.Log("Platform enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Platform exit");
            other.transform.parent = null;
        }
    }
}
