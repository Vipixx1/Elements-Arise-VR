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

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private GameObject explosionParticle;
    
    public override void OnVolcano(ObjectData data, float[] args = null)
    {
        Explode();

    }

    public override void OnFire(ObjectData data, float[] args = null)
    {
        if (data.Temperature >= explosionTemp) { Explode(); }

    }

    public override void OnSteam(ObjectData data, float[] args = null)
    {
        if (data.Temperature >= explosionTemp) { Explode(); }
    }


    private void Explode()
    {

        GameObject explosionGO = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        ParticleSystem.MainModule mainexplosionParticlesystem = explosionGO.GetComponent<ParticleSystem>().main;
        mainexplosionParticlesystem.startColor = gameObject.GetComponent<Renderer>().material.color;
        StartCoroutine(DestroyAfterTime(1, explosionGO));
        Collider[] colliders = new Collider[30];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius,colliders, layer);

        for (int i = 0; i < numColliders; i++)
        {
            Debug.Log(numColliders);
            if (colliders[i].attachedRigidbody)
                colliders[i].attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        Destroy(this.gameObject);
    }


    public IEnumerator DestroyAfterTime(float time, GameObject go)
    {
        yield return new WaitForSeconds(time);
        Destroy(go);
    }
}
