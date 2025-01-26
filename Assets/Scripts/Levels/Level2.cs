using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] CrystalMaterial[] crystals;
    [SerializeField] GameObject secretDoor;

    [SerializeField] GameObject door;

    [SerializeField] GameObject floor;


    private void Update()
    {
        bool check = true;

        foreach (var crystal in crystals)
        {
            if (!crystal.IsActivated())
            {
                check = false;
            }
        }
        if (secretDoor == null) check = true;
        if (check)
        {
            Destroy(door);
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
