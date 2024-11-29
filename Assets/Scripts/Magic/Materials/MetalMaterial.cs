using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class MetalMaterial : Material
{
    bool isMelting = false;

    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (isMelting)
        {
            Melt();
        }
    }




    private void Awake()
    {
        conductivite = true;
    }

    public override void Burning(ObjectData data)
    {
        base.Burning(data);
        if (data.Temperature > 3)
        {
            isMelting=true;
        }
    }

    private void Melt()
    {
        if (transform.localScale.y < 0.1)
        {
            return;
        }


        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        for (var i = 0; i < 20; i++)
        {
            vertices[i] += Time.deltaTime * normals[i] / 10;

        }

        vertices[44] += Time.deltaTime * normals[44] / 10;
        vertices[47] += Time.deltaTime * normals[47] / 10;
        vertices[42] += Time.deltaTime * normals[42] / 10;



        transform.localScale -= Time.deltaTime / 10 * new Vector3(0, 1, 0);
        transform.position -= Time.deltaTime / 10 * new Vector3(0, 1, 0);



        mesh.vertices = vertices;
    }

    public override void OnIce(ObjectData data)
    {
        base.OnIce(data);
        if (data.Humidity > 0 || data.Temperature < 1)
        {
            isMelting = false;
        }

    }

    public override void OnWater(ObjectData data)
    {
        base.OnWater(data);
        if (data.Humidity > 0 || data.Temperature < 1)
        {
            isMelting = false;
        }
    }

}
