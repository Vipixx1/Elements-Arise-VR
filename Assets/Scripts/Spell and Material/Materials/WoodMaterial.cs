using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public enum Axis
{
    x,
    y,
    z
}
public class WoodMaterial : Material
{
    private float scaling;
    [SerializeField] private float maxScaling;
    private bool isBurning = false;
    private bool isGrowing = false;

    [SerializeField] private Axis axisToBurn;

    private void Start()
    {
        switch (axisToBurn)
        {
            case Axis.x:
                scaling = transform.localScale.x;
                break;
            case Axis.y:
                scaling = transform.localScale.y;
                break;
            case Axis.z:
                scaling = transform.localScale.z;
                break;
        }
        maxScaling *= scaling;
    }


    private void Update()
    {
        if (isGrowing && scaling < maxScaling)
        {
            scaling += Time.deltaTime;
        }
        else
        {
            isGrowing = false;
        }
        if (isBurning)
        {
            scaling -= Time.deltaTime;
            if (scaling <= 0)
            {
                Destroy(gameObject);
            }

        }
        Vector3 newScale = gameObject.transform.lossyScale;
        switch (axisToBurn)
        {
            case Axis.x:
                newScale.x = scaling;
                break;
            case Axis.y:
                newScale.y = scaling;
                break;
            case Axis.z:
                newScale.z = scaling;
                break;
        }
        gameObject.transform.localScale = newScale;
    }

    public override void Burning(ObjectData data)
    {
        base.Burning(data);
        isBurning = true;
    }

    public override void OnPlant(ObjectData data, float[] args = null)
    {
        if (!isBurning)
            isGrowing = true;
    }

    public override void OnIce(ObjectData data, float[] args = null)
    {
        base.OnIce(data);
        isGrowing = false;
        if (data.Humidity > 0 || data.Temperature < 1)
            isBurning = false;
    }

    public override void OnWater(ObjectData data, float[] args = null)
    {
        base.OnWater(data);
        if (data.Humidity > 0 || data.Temperature < 1)
            isBurning = false;
    }

}
