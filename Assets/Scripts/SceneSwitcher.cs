using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    
    int currentSceneIndex;
    int nextSceneIndex;
    
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
            SceneManager.LoadScene(nextSceneIndex);
    }
}
