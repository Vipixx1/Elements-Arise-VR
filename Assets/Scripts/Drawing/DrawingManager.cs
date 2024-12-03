using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    [SerializeField] private GameObject scroll;
    [SerializeField] private GameObject camera;

    private bool isRightHandActivated = false;
    private bool isLeftHandActivated = false;

    void Update()
    {
        if (isRightHandActivated && isLeftHandActivated)
        {
            scroll.SetActive(true);
            scroll.transform.position = camera.transform.position + camera.transform.forward*0.5f;
            scroll.transform.rotation = camera.transform.rotation * Quaternion.Euler(270, 0, 0);
        }
    }

    public void SetRightHand(bool isActivated)
    {
        isRightHandActivated = isActivated;
    }

    public void SetLeftHand(bool isActivated)
    {
        isLeftHandActivated = isActivated;
    }
}
