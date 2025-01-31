using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Sandbox: MonoBehaviour
{
    [SerializeField] DrawingRecognizer drawingRecognizer;

    private void Start()
    {
        drawingRecognizer.CanDrawFire = true;
        drawingRecognizer.CanDrawWater = true;
        drawingRecognizer.CanDrawEarth = true;
        drawingRecognizer.CanDrawWind = true;
        drawingRecognizer.CanDrawThunder = true;
        drawingRecognizer.CanDrawSand = true;
        drawingRecognizer.CanDrawVolcano = true;
        drawingRecognizer.CanDrawIce = true;
        drawingRecognizer.CanDrawPlant = true;
        drawingRecognizer.CanDrawSteam = true;
    }
}
