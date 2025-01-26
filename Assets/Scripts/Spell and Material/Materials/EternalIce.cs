using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class EternalIce : Material
{

    private IEnumerator MeltingCoroutine;
    private bool isMelting = false;
    private Vector3 maxScale;
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
        maxScale = gameObject.transform.lossyScale;
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
        isMelting = true;
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
            Debug.Log(scale.x);
            gameObject.transform.localScale = scale;
            Debug.Log(gameObject.transform.localScale);
            yield return null;
            if (scale.x < minScale)
            {
                isMelting = false;
            }

        }
        Debug.Log(gameObject.transform.localScale);
        scale.x = minScale;
        scale.y = minScale;
        scale.z = minScale;
        gameObject.transform.localScale = scale;
        Debug.Log(gameObject.transform.localScale);
        yield return delay;
        yield return StartCoroutine(Growing());

    }

    IEnumerator Growing()
    {
        scale = gameObject.transform.localScale;

        while (scale.x < maxScale.x)
        {
            float elapsedTime = Time.deltaTime;
            scale.x += elapsedTime;
            scale.y += elapsedTime;
            scale.z += elapsedTime;
            gameObject.transform.localScale = scale;
            yield return null;
        }

    }


}
