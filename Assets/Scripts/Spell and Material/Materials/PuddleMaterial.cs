using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Material = Magic.Materials.Material;

public class PuddleMaterial : Material
{
    [SerializeField] private int initialMaxScale;
    private int maxScale;
    private Vector3 initialScaleVector;
    [SerializeField] GameObject icePrefab;


    private void Awake()
    {
        maxScale = initialMaxScale;
        initialScaleVector = transform.localScale;
        initialScaleVector.y = 0;
    }


    public override void Burning(ObjectData data)
    {
        base.Burning(data);
        if (maxScale < initialMaxScale*2)
        {
            maxScale +=1;
        }


        transform.localScale -= initialScaleVector/ initialMaxScale;
        Debug.Log(maxScale);

        if (maxScale == initialMaxScale*2)
        {
            Destroy(gameObject);
        }
    }

    public override void OnWater(ObjectData data, float[] args = null)
    {
        base.OnWater(data);
        if (maxScale > 0)
        {
            maxScale -= 1;
            transform.localScale += new Vector3(1, 0, 1) / 3;
        }

    }

    public override void OnIce(ObjectData data, float[] args = null)
    {
        base.OnIce(data);

        if (args != null)
        {
            Vector3 spawnPos = new Vector3(args[0], transform.position.y + transform.localScale.y/2 + icePrefab.transform.localScale.y/2, args[2]);

            GameObject icePaddle = Instantiate(icePrefab, spawnPos, Quaternion.identity);
        }

    }

}
