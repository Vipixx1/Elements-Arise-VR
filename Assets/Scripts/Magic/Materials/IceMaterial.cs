using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class IceMaterial : Material
{
    [SerializeField] private int initialMaxScale;
    private int maxScale;
    private Vector3 initialScaleVector;
    private float timeDuration;
    [SerializeField] private float maxTimeDuration;

    private void Awake()
    {
        maxScale = initialMaxScale;
        initialScaleVector = transform.localScale;
        initialScaleVector.y = 0;
        timeDuration = maxTimeDuration;
    }

    private void Update()
    {
        timeDuration -= Time.deltaTime;
        if (timeDuration < 0) { 
            Destroy(gameObject);
        }
    }


    public override void Burning(ObjectData data)
    {
        base.Burning(data);
        if (maxScale < initialMaxScale * 2)
        {
            maxScale += 1;
        }


        transform.localScale -= initialScaleVector / initialMaxScale;
        Debug.Log(maxScale);

        if (maxScale == initialMaxScale * 2)
        {
            Destroy(gameObject);
        }
    }


    public override void OnIce(ObjectData data)
    {
        base.OnIce(data);
        timeDuration = maxTimeDuration;
    }


}
