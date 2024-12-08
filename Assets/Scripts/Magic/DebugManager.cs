using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    private void Update()
    {
        transform.position = new Vector3(0, 5, 0);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(prefabs[0],transform);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {

            Instantiate(prefabs[1], transform);
        }        else if (Input.GetKeyDown(KeyCode.E))
        {

            Instantiate(prefabs[2], transform);
        }        else if (Input.GetKeyDown(KeyCode.R))
        {

            Instantiate(prefabs[3], transform);
        }        else if (Input.GetKeyDown(KeyCode.T))
        {

            Instantiate(prefabs[4], transform);
        }        else if (Input.GetKeyDown(KeyCode.Y))
        {

            Instantiate(prefabs[5], transform);
        }else if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (Transform child in this.transform)
            {

                Destroy(child.gameObject);
            }
        }
    }
}
