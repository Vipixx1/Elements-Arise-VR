using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindField : MonoBehaviour
{

    private Vector3 direction;

    [SerializeField]
    private float strength = 3f;

    [SerializeField]
    private ParticleSystem trails;


    void Start()
    {
        ShootingManager shootingManager = FindAnyObjectByType<ShootingManager>();
        Vector3 startingPoint = (Vector3)shootingManager.firstWindSpell;
        Vector3 endingPoint = (Vector3)shootingManager.secondWindSpell;
        

        direction = endingPoint - startingPoint;

        transform.position = startingPoint + direction/2f;
        float angle =- Mathf.Atan2(endingPoint.z - startingPoint.z, endingPoint.x - startingPoint.x);
        transform.Rotate(0, Mathf.Rad2Deg * angle, 0);



        Vector3 scale = transform.localScale;
        scale.x = direction.magnitude;
        transform.localScale = scale;

        trails.startLifetime = 0.11f * scale.x;
        trails.gameObject.transform.position = startingPoint + new Vector3(0,direction.y/2,0) ;
    }

    
    void ForceField(Rigidbody otherBody)
    {
        Vector3 normalized = direction.normalized;

        Vector3 force = normalized * strength;

        otherBody.velocity = force*Time.deltaTime;

    }


    private void OnTriggerStay(Collider other)
    {

        if (other.attachedRigidbody) { 
            
            ForceField(other.attachedRigidbody);
            if (other.gameObject.GetComponent<Spell>())
                other.gameObject.GetComponent<Spell>().StopAllCoroutines();
            other.attachedRigidbody.useGravity = false;
         }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody && other.gameObject.tag != "Spell")
        {
            other.attachedRigidbody.velocity = Vector3.zero;
            other.attachedRigidbody.useGravity = true;
        }
        if (other.gameObject.GetComponent<Spell>())
            other.gameObject.GetComponent<Spell>().StartCoroutine(other.gameObject.GetComponent<Spell>().DestroyAfterTime(5f));
    }

}
