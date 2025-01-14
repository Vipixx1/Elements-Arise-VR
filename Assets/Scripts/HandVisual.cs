using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVisual : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer handRenderer;
    [SerializeField] private Material handColorBase;
    [SerializeField] private List<Material> spellMaterials;


    private void Start()
    {
        SetHandColorBySpell("");
    }

    public void SetHandColorBySpell(string element)
    {
            switch (element.ToLower())
        {
            case "fire":
                handRenderer.material = spellMaterials[0];
                break;
            case "water":
                handRenderer.material = spellMaterials[1];
                break;
            case "earth":
                handRenderer.material = spellMaterials[2];
                break;
            case "wind":
                handRenderer.material = spellMaterials[3];
                break;
            case "ice":
                handRenderer.material = spellMaterials[4];
                break;
            case "plant":
                handRenderer.material = spellMaterials[5];
                break;
            case "thunder":
                handRenderer.material = spellMaterials[6];
                break;
            case "volcano":
                handRenderer.material = spellMaterials[7];
                break;
            case "steam":
                handRenderer.material = spellMaterials[8];
                break;
            case "sand":
                handRenderer.material = spellMaterials[9];
                break;
            
            default:
                handRenderer.material = handColorBase;
                break;
        }
    }
}
