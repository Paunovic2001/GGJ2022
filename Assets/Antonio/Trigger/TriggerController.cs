using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject target;
    public GameObject button;
    Vector3 buttonStartPos;
    public Material pushedColor;

    [Header("Possible controller types: door, platform, spike")]
    public string type;
    //DoorController dc;
    //SpikeController sc;
    PlatformController pc;

    private void Start()
    {
        buttonStartPos = button.transform.position;
        if (type == "door")
        {
            //Get door controller
        }

        if (type == "platform")
        {
            pc = target.GetComponent<PlatformController>();
        }

        if (type == "spike")
        {
            //Get spike controller
        }
    }

    public void Activate()
    {
        StartCoroutine(PushButton());
        if (type == "door")
        {
            //Get door controller
        }

        if (type == "platform")
        {
            pc.EnableMovement();
        }

        if (type == "spike")
        {
            //Get spike controller
        }
    }

    public void Deactivate()
    {
        if (type == "door")
        {
            //Get door controller
        }

        if (type == "platform")
        {
            pc.DisableMovement();
        }

        if (type == "spike")
        {
            //Get spike controller
        }
    }

    bool interactable = true;
    IEnumerator PushButton()
    {
        if(interactable == false)
        {
            yield break; ;
        }
        button.GetComponent<MeshRenderer>().material = pushedColor;
        interactable = false;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            button.transform.Translate(Vector3.down * Time.deltaTime);
            if (Vector3.Distance(buttonStartPos, button.transform.position) >= 0.02f)
            {
                break;               
            }
        }
    }
}
