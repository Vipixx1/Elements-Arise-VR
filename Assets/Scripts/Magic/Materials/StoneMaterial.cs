using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class StoneMaterial : Material
{
    [SerializeField]
    private int explosionTemp;

    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private float explosionForce;
    
    public override void OnVolcano(ObjectData data)
    {
        Explode();

    }

    public override void OnFire(ObjectData data)
    {
        if (data.Temperature >= explosionTemp) { Explode(); }

    }

    public override void OnSteam(ObjectData data)
    {
        if (data.Temperature >= explosionTemp) { Explode(); }
    }


    private void Explode()
    {
        Collider[] colliders = new Collider[20];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius,colliders);

        for (int i = 0; i < numColliders; i++)
        {
            Debug.Log(numColliders);
            if (colliders[i].attachedRigidbody)
                colliders[i].attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        Destroy(this.gameObject);

    }

}
