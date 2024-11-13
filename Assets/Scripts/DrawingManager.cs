using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    [SerializeField] private GameObject whiteboard;
    [SerializeField] private GameObject camera;

    private bool isRightHandActivated = false;
    private bool isLeftHandActivated = false;

    // Update is called once per frame
    void Update()
    {
        if (isRightHandActivated && isLeftHandActivated)
        {
            whiteboard.GetComponent<Whiteboard>().EraseWhiteboard();
            whiteboard.SetActive(true);
            whiteboard.transform.position = camera.transform.position + camera.transform.forward*0.5f;
            whiteboard.transform.rotation = camera.transform.rotation * Quaternion.Euler(270, 0, 0);

            Debug.Log("Whiteboard activated");
        }
    }

    public void ActivateRightHand()
    {
        isRightHandActivated = true;
    }

    public void DeactivateRightHand()
    {
        isRightHandActivated = false;
    }

    public void ActivateLeftHand()
    {
        isLeftHandActivated = true;
    }

    public void DeactivateLeftHand()
    {
        isLeftHandActivated = false;
    }
}
