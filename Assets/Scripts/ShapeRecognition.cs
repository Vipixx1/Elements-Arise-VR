using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRecognition : MonoBehaviour
{
    private Renderer _renderer;
    private Whiteboard _whiteboard;
    void Start()
    { 
        _renderer = GetComponent<Renderer>();
        _whiteboard = GetComponent<Whiteboard>();
    }


    void Update()
    {
        
    }
}
