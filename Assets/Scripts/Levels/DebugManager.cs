using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private List<SpawnedObject> spawnedObjects = new List<SpawnedObject>();

    private void Start()
    {
        // Automatically track all existing GameObjects with the "Material" tag
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Material"))
        {
            TrackExistingObject(obj);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnAll();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            DespawnAll();
        }
    }

    private void TrackExistingObject(GameObject obj)
    {
        PrefabLinker prefabLinker = obj.GetComponent<PrefabLinker>();

        if (prefabLinker != null && prefabLinker.prefab != null)
        {
            spawnedObjects.Add(new SpawnedObject(prefabLinker.prefab, obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj));
        }
        else
        {
            spawnedObjects.Add(new SpawnedObject(null, obj.transform.position, obj.transform.rotation, obj.transform.localScale, obj));
        }
    }

    private void DespawnAll()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj.CurrentInstance != null)
            {
                Destroy(obj.CurrentInstance);
                obj.CurrentInstance = null;
            }
        }
    }

    private void RespawnAll()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj.CurrentInstance == null)
            {
                if (obj.Prefab != null)
                {
                    obj.CurrentInstance = Instantiate(obj.Prefab, obj.InitialPosition, obj.InitialRotation);
                    obj.CurrentInstance.transform.localScale = obj.InitialScale;
                }
                else
                {
                    obj.CurrentInstance = new GameObject("Respawned Object");
                    obj.CurrentInstance.transform.position = obj.InitialPosition;
                    obj.CurrentInstance.transform.rotation = obj.InitialRotation;
                    obj.CurrentInstance.transform.localScale = obj.InitialScale;
                    obj.CurrentInstance.tag = "Material";
                }
            }
        }
    }

    // Helper class to track spawned objects
    private class SpawnedObject
    {
        public GameObject Prefab { get; }
        public Vector3 InitialPosition { get; }
        public Quaternion InitialRotation { get; }
        public Vector3 InitialScale { get; }
        public GameObject CurrentInstance { get; set; }

        public SpawnedObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, GameObject instance)
        {
            Prefab = prefab;
            InitialPosition = position;
            InitialRotation = rotation;
            InitialScale = scale;
            CurrentInstance = instance;
        }
    }
}
