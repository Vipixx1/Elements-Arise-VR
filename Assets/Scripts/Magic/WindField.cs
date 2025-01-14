using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindField : MonoBehaviour
{

    private Vector3 direction;

    [SerializeField]
    private float strength = 3f;


    void Start()
    {
        ShootingManager shootingManager = FindAnyObjectByType<ShootingManager>();
        Vector3 startingPoint = shootingManager.firstWindSpell.position;
        Vector3 endingPoint = shootingManager.secondWindSpell.position;
        

        direction = endingPoint - startingPoint;

        transform.position = startingPoint + direction/2f;
        transform.localScale = direction/2f;

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

         }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody)
        {
            other.attachedRigidbody.velocity = Vector3.zero;
        }
    }
}
