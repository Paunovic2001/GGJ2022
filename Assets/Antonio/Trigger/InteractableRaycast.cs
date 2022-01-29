using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRaycast : MonoBehaviour
{
    Ray globalRay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(LayerMask.LayerToName(9));
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            globalRay = ray;
            if (Physics.Raycast(ray, out hit, 0.9f/*, LayerMask.NameToLayer("Interactable")*/))
            {
                if (hit.transform.tag == "Interactable")
                {
                    Debug.Log(hit.transform.gameObject.layer);
                    hit.transform.GetComponent<TriggerController>().Activate();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(globalRay);
    }
}
