using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DestroyAfter()
    {
        ParticleSystem.EmissionModule emi = GetComponent<ParticleSystem>().emission;
        emi.burstCount = 0;
        this.transform.localScale = Vector3.one *0.3f;
        StartCoroutine(DeathTimmer());
    }

    IEnumerator DeathTimmer()
    {

        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
