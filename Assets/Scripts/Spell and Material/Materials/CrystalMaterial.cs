using Magic.Materials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class CrystalMaterial : Material
{
    bool isActivated = false;
    [SerializeField] float MaxDuration = 20;
    [SerializeField] SpellType spellType;
    float timeRemaining;
    Renderer mat;
    UnityEngine.Material baseMaterial;

    private void Start()
    {
        mat = GetComponent<Renderer>();
        baseMaterial = mat.material;
    }


    private void Update()
    {
        if (timeRemaining < 0)
        {
            //mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
            mat.material.color = baseMaterial.color;
        } else 
            timeRemaining -= Time.deltaTime;
    }

    public override void OnEarth(ObjectData data, float[] args = null)
    {
        base.OnEarth(data, args);
        mat.material.color = new Color32(139, 69, 19,0);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.EARTH)
            Glow();
    }

    public override void OnFire(ObjectData data, float[] args = null)
    {
        base.OnFire(data, args);
        mat.material.color = Color.red;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.FIRE)
            Glow();
    }

    public override void OnVolcano(ObjectData data, float[] args = null)
    {
        base.OnVolcano(data, args);
        mat.material.color = Color.black;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.VOLCANO)
            Glow();
    }

    public override void OnIce(ObjectData data, float[] args = null)
    {
        base.OnIce(data, args);
        mat.material.color = Color.cyan;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.ICE)
            Glow();
    }

    public override void OnPlant(ObjectData data, float[] args = null)
    {
        base.OnPlant(data, args);
        mat.material.color = Color.green;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.PLANT)
            Glow();
    }

    public override void OnSand(ObjectData data, float[] args = null)
    {
        base.OnSand(data, args);
        mat.material.color = new Color32(244, 164, 96,0);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.SAND)
            Glow();
    }

    public override void OnSteam(ObjectData data, float[] args = null)
    {
        base.OnSteam(data, args);
        mat.material.color = new Color32(135, 206, 235,0);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.STEAM)
            Glow();
    }

    public override void OnThunder(ObjectData data, float[] args = null)
    {
        base.OnThunder(data, args);
        mat.material.color = Color.yellow;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.THUNDER)
            Glow();
    }

    public override void OnWater(ObjectData data, float[] args = null)
    {
        base.OnWater(data, args);
        mat.material.color = Color.blue;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.WATER)
            Glow();
    }

    public override void OnWind(ObjectData data, float[] args = null)
    {
        base.OnWind(data, args);
        mat.material.color = new Color32(152, 251, 152,0);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.WIND)
            Glow();
    }


    private void Glow()
    {
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 10);
        timeRemaining = MaxDuration;
    }

    public bool IsActivated()
    {
        return timeRemaining > 0;
    }
}
