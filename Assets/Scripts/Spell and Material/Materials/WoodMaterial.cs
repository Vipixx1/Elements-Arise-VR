using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float scalingX;
    private float scalingY;
    private float scalingZ;

    [SerializeField] private float maxScaling;

    private float maxScalingX;
    private float maxScalingY;
    private float maxScalingZ;

    [SerializeField] private bool isBurning = false;
    [SerializeField] private bool isGrowing = false;

    [SerializeField] private Axis[] axisesToBurn;

    private void Start()
    {
        scalingX = transform.localScale.x;
        scalingY = transform.localScale.y;
        scalingZ = transform.localScale.z;
        for (int i = 0; i < axisesToBurn.Length; i++)
        {
            switch (axisesToBurn[i])
            {
                case Axis.x:
                    maxScalingX = maxScaling * scalingX;
                    break;
                case Axis.y:
                    maxScalingY = maxScaling * scalingY;
                    break;
                case Axis.z:
                    maxScalingZ = maxScaling * scalingZ;
                    break;
            }
        }
    }

    private void Update()
    {
        if (isGrowing)
        {
            if (axisesToBurn.Contains(Axis.x) && scalingX < maxScalingX)
            {
                scalingX += Time.deltaTime;
            }
            if (axisesToBurn.Contains(Axis.y) && scalingY < maxScalingY)
            {
                scalingY += Time.deltaTime;
            }
            if (axisesToBurn.Contains(Axis.z) && scalingZ < maxScalingZ)
            {
                scalingZ += Time.deltaTime;
            }
        }
        else
        {
            isGrowing = false;
        }

        if (isBurning)
        {
            if (axisesToBurn.Contains(Axis.x))
            {
                scalingX -= Time.deltaTime;
                if (scalingX <= 0)
                {
                    Destroy(gameObject);
                }
            }
            if (axisesToBurn.Contains(Axis.y))
            {
                scalingY -= Time.deltaTime;
                if (scalingY <= 0)
                {
                    Destroy(gameObject);
                }
            }
            if (axisesToBurn.Contains(Axis.z))
            {
                scalingZ -= Time.deltaTime;
                if (scalingZ <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        Vector3 newScale = gameObject.transform.localScale;
        for (int i = 0; i < axisesToBurn.Length; i++)
        {
            switch (axisesToBurn[i])
            {
                case Axis.x:
                    newScale.x = scalingX;
                    break;
                case Axis.y:
                    newScale.y = scalingY;
                    break;
                case Axis.z:
                    newScale.z = scalingZ;
                    break;
            }
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
