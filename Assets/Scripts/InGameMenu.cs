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

            // Position the menu in front of the player
            menu.transform.position = playerPos.transform.position + playerPos.transform.forward * 0.5f;

            // Make the menu face the player, then rotate it 90° clockwise (right)
            menu.transform.rotation = Quaternion.LookRotation(menu.transform.position - playerPos.transform.position, Vector3.up) * Quaternion.Euler(0, -90, 0);
        }
    }
}
