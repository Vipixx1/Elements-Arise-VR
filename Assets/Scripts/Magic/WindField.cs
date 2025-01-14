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
        Vector3 startingPoint = (Vector3)shootingManager.firstWindSpell;
        Vector3 endingPoint = (Vector3)shootingManager.secondWindSpell;
        

        direction = endingPoint - startingPoint;

        transform.position = startingPoint + direction/2f;
        float angle = Vector3.Angle(startingPoint, endingPoint);
        transform.Rotate(0,angle,0);



        Vector3 scale = transform.localScale;
        scale.x = direction.magnitude / 2f;
        transform.localScale = scale;

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
