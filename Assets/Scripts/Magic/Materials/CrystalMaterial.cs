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

    private void Start()
    {
        mat = GetComponent<Renderer>();
    }


    private void Update()
    {
        if (timeRemaining < 0)
        {
            mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        } else 
            timeRemaining -= Time.deltaTime;
    }

    public override void OnEarth(ObjectData data)
    {
        base.OnEarth(data);
        mat.material.color =new Color(139, 69, 19);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.EARTH)
            Glow();
    }

    public override void OnFire(ObjectData data)
    {
        base.OnFire(data);
        mat.material.color = Color.red;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.FIRE)
            Glow();
    }

    public override void OnVolcano(ObjectData data)
    {
        base.OnVolcano(data);
        mat.material.color = Color.black;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.VOLCANO)
            Glow();
    }

    public override void OnIce(ObjectData data)
    {
        base.OnIce(data);
        mat.material.color = Color.cyan;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.ICE)
            Glow();
    }

    public override void OnPlant(ObjectData data)
    {
        base.OnPlant(data);
        mat.material.color = Color.green;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.PLANT)
            Glow();
    }

    public override void OnSand(ObjectData data)
    {
        base.OnSand(data);
        mat.material.color = new Color(244, 164, 96);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.SAND)
            Glow();
    }

    public override void OnSteam(ObjectData data)
    {
        base.OnSteam(data);
        mat.material.color = new Color(135, 206, 235);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.STEAM)
            Glow();
    }

    public override void OnThunder(ObjectData data)
    {
        base.OnThunder(data);
        mat.material.color = Color.yellow;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.THUNDER)
            Glow();
    }

    public override void OnWater(ObjectData data)
    {
        base.OnWater(data);
        mat.material.color = Color.blue;
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.WATER)
            Glow();
    }

    public override void OnWind(ObjectData data)
    {
        base.OnWind(data);
        mat.material.color = new Color(152, 251, 152);
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color") * 0);
        if (spellType == SpellType.WIND)
            Glow();
    }


    private void Glow()
    {
        Debug.Log("oui");
        mat.material.SetColor("_EmissionColor", mat.material.GetColor("_Color")*10);
        timeRemaining = MaxDuration;
    }
}
