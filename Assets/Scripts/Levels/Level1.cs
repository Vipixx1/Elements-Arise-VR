using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] GameObject floorOfRoom2;
    [SerializeField] GameObject[] planks;

    private void Update()
    {
        if (!planks.Any())
        {
            foreach (Transform carpet in floorOfRoom2.transform)
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
