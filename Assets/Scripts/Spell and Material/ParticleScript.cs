using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScript : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy = 2.0f;

    public void DestroyAfter()
    {
        ParticleSystem.EmissionModule emi = GetComponent<ParticleSystem>().emission;
        emi.burstCount = 0;
        this.transform.localScale = Vector3.one * 0.3f;
        StartCoroutine(DeathTimmer());
    }

    IEnumerator DeathTimmer()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(this.gameObject);
    }
}
