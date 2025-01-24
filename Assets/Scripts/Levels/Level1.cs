using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] GameObject floor;

    private void Update()
    {
        if (transform.childCount == 0)
        {
            foreach (Transform carpet in floor.transform)
            {
                SetInteractable(carpet.gameObject);
            }
        }
    }

    void SetInteractable(GameObject item)
    {
        item.GetComponent<TeleportInteractable>().AllowTeleport = true;
    }
}
