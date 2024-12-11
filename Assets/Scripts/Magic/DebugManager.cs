using Meta.WitAi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Material = Magic.Materials.Material;

public class DebugManager : MonoBehaviour
{

    [SerializeField] private GameObject[] prefabs2;


    private void Start()
    {
        transform.position = new Vector3(0, 5, 0);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(prefabs2[0],transform.position,Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {

            Instantiate(prefabs2[1], transform.position, Quaternion.identity);
        }        else if (Input.GetKeyDown(KeyCode.E))
        {

            Instantiate(prefabs2[2], transform.position, Quaternion.identity);
        }        else if (Input.GetKeyDown(KeyCode.R))
        {

            Instantiate(prefabs2[3], transform.position, Quaternion.identity);
        }        else if (Input.GetKeyDown(KeyCode.T))
        {

            Instantiate(prefabs2[4], transform.position, Quaternion.identity);
        }        else if (Input.GetKeyDown(KeyCode.Y))
        {

            Instantiate(prefabs2[5], transform.position, Quaternion.identity);
        }else if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (GameObject child in GameObject.FindGameObjectsWithTag("Material"))
            {

                Destroy(child);
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) { transform.position += new Vector3(-0.5f, 0 , 0);

        }
         else if (Input.GetKeyDown(KeyCode.RightArrow )) { transform.position += new Vector3(0.5f, 0 , 0);


        }
         else if (Input.GetKeyDown(KeyCode.UpArrow)) { transform.position += new Vector3(0, 0 , 0.5f);


        }
         else if (Input.GetKeyDown(KeyCode.DownArrow)) { transform.position += new Vector3(0, 0 ,-0.5f);

        }
    }
}
