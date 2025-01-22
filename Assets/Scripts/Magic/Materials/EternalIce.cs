using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class EternalIce : Material
{

    private IEnumerator MeltingCoroutine;
    private bool isMelting = false;
    private Vector3 scale;
    private WaitForSeconds delay;

    [SerializeField]
    private float minScale;


    [SerializeField]
    private float growingDelay;


    // Start is called before the first frame update
    void Start()
    {
        scale = gameObject.transform.lossyScale;
        MeltingCoroutine = Melting();
        delay = new WaitForSeconds(growingDelay);

    }


    public override void OnIce(ObjectData data, float[] args = null)
    {
        base.OnIce(data, args);
        isMelting = false;
        StopAllCoroutines();
        StartCoroutine(Growing());
    }


    public override void Burning(ObjectData data)
    {
       base.Burning(data);
        isMelting=true;
        StopAllCoroutines();
        StartCoroutine(Melting());

    }

    IEnumerator Melting()
    {

        while (isMelting)
        {
            float elapsedTime = Time.deltaTime;
            scale.x -= elapsedTime;
            scale.y -= elapsedTime;
            scale.z -= elapsedTime;

            if (scale.x > minScale)
            {
                isMelting = false;
            }
            else
            {
                gameObject.transform.localScale = scale;
                yield return null;
            }
        }

        scale.x = minScale;
        scale.y = minScale;
        scale.z = minScale;
        gameObject.transform.localScale = scale;

        yield return delay;
        yield return StartCoroutine(Growing());

    }

    IEnumerator Growing()
    {
        

        while (scale.x < 1)
        {
            float elapsedTime = Time.deltaTime;
            scale.x += elapsedTime;
            scale.y += elapsedTime;
            scale.z += elapsedTime;
            yield return null;
        }

    }


}
