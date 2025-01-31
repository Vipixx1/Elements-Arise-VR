using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] CrystalMaterial[] crystals;
    [SerializeField] GameObject door;

    [SerializeField] GameObject plank;

    [SerializeField] GameObject floor;

    [SerializeField] DrawingRecognizer drawingRecognizer;

    [SerializeField] GameObject questionMarkImage;
    [SerializeField] GameObject iceImage;

    private void OnEnable()
    {
        ShootingManager.OnSpellFusion += EnableDrawingFusion;
    }

    private void OnDisable()
    {
        ShootingManager.OnSpellFusion -= EnableDrawingFusion;
    }

    private void Start()
    {
        drawingRecognizer.CanDrawFire = true;
        drawingRecognizer.CanDrawWater = true;
        drawingRecognizer.CanDrawEarth = true;
        drawingRecognizer.CanDrawWind = true;
        drawingRecognizer.CanDrawThunder = false;
        drawingRecognizer.CanDrawSand = false;
        drawingRecognizer.CanDrawVolcano = false;
        drawingRecognizer.CanDrawIce = false;
        drawingRecognizer.CanDrawPlant = false;
        drawingRecognizer.CanDrawSteam = false;

        questionMarkImage.SetActive(true);
        iceImage.SetActive(false);
    }

    private void Update()
    {
        if (door != null)
        {
            bool check = true;

            foreach (var crystal in crystals)
                if (!crystal.IsActivated) check = false;

            if (plank == null)
                check = true;

            if (door.gameObject.GetComponent<Rigidbody>().GetAccumulatedForce() != Vector3.zero)
                check = true;

            if (check)
            {
                if (door != null) Destroy(door);
                foreach (Transform carpet in floor.transform)
                {
                    SetInteractable(carpet.gameObject);
                }
            }
        }
    }

    void SetInteractable(GameObject item)
    {
        item.GetComponent<TeleportInteractable>().AllowTeleport = true;
    }

    void EnableDrawingFusion(string element)
    {
        if (element == "ice")
        {
            questionMarkImage.SetActive(false);
            iceImage.SetActive(true);
            drawingRecognizer.CanDrawIce = true;
        }
    }
}
