using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class WoodMaterial : Material
{
    private float scaling = 1;
    [SerializeField] private float maxScaling;
    private bool isBurning = false;
    private bool isGrowing = false;

    private void Update()
    {
        transform.rotation = Quaternion.identity;

        if (isGrowing && scaling < maxScaling)
        {
            scaling += Time.deltaTime;
        }
        if (isBurning)
        {

            scaling -= Time.deltaTime;
            if (scaling <= 0)
            {
                Destroy(gameObject);
            }
        
        }
        gameObject.transform.localScale = new Vector3(1, scaling, 1);

    
    }


    public override void Burning(ObjectData data)
    {
        base.Burning(data);
        isBurning = true;
        Debug.Log(isBurning);
    }

    public override void OnPlant(ObjectData data)
    {
        if (!isBurning)
        {
            isGrowing = true;
        }
    }

    public override void OnIce(ObjectData data)
    {
        base.OnIce(data);
        isGrowing = false;
        if (data.Humidity > 0 || data.Temperature < 1)
        {
            isBurning = false;
        }

    }

    public override void OnWater(ObjectData data)
    {
        base.OnWater(data);
        if (data.Humidity > 0 || data.Temperature < 1)
        {
            isBurning = false;
        }
    }

}
