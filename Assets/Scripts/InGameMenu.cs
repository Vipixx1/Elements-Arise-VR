using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject playerPos;

    private Coroutine closeMenuCoroutine;

    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Start))
        {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = playerPos.transform.position + playerPos.transform.forward * 0.5f;
            Quaternion rotation = menu.transform.rotation;
            rotation.y = playerPos.transform.rotation.y;
            rotation = rotation * Quaternion.Euler(0, -90, 0);
            menu.transform.rotation = rotation;
        }
    }
}
