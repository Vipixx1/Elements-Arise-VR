using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] GameObject floorOfRoom2;
    [SerializeField] List<GameObject> planks;

    private void Update()
    {
        if (planks.Exists((elt) => !elt.IsUnityNull())) return;
        foreach (Transform carpet in floorOfRoom2.transform)
        {
            SetInteractable(carpet.gameObject);
        }
    }

    void SetInteractable(GameObject item)
    {
        item.GetComponent<TeleportInteractable>().AllowTeleport = true;
    }
}
