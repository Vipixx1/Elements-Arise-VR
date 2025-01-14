using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionStarter : MonoBehaviour
{

    [SerializeField]
    private ShootingManager receiver;


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "RightHand")
        {
            receiver.FuseSpell();
            
        }

        
    }
        
    
}
