using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PDollarGestureRecognizer;

public class DrawingRecognition : MonoBehaviour
{
    private List<Gesture> trainingSet = new List<Gesture>();

    [SerializeField] TMPro.TextMeshPro text;
    private string message;
    private bool recognized;
    private string _gestureClass;
    private Whiteboard _whiteboard;

    [SerializeField] private GameObject element1Prefab;
    [SerializeField] private GameObject element2Prefab;
    [SerializeField] private GameObject element3Prefab;
    [SerializeField] private GameObject element4Prefab;

    void Start()
    {
        _whiteboard = this.GetComponent<Whiteboard>();

        // Load pre-made gestures
        /*TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        Debug.Log(gesturesXml.Length);
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));*/

        // Load user-defined gestures
        string[] filePaths = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    void Update()
    {
        RecognizeGesture();

        if (recognized)
        {
            SpawnElement();
            recognized = false;
            _whiteboard.EraseWhiteboard();
        }
    }

    private void RecognizeGesture()
    {
        Gesture candidate = new Gesture(_whiteboard.Points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        message = gestureResult.GestureClass + " " + gestureResult.Score;
        text.text = message;

        if (gestureResult.Score < 0.85f)
        {
            _gestureClass = "Unknown";
            recognized = false;
            return;
        }

        recognized = true;
        _gestureClass = gestureResult.GestureClass;
    }

    private void SpawnElement()
    {
        switch (_gestureClass)
        {
            case "Square":
                Instantiate(element1Prefab, _whiteboard.transform.position, Quaternion.identity);
                break;
            case "Triangle":
                Instantiate(element2Prefab, _whiteboard.transform.position, Quaternion.identity);
                break;
            case "Circle":
                Instantiate(element3Prefab, _whiteboard.transform.position, Quaternion.identity);
                break;
            case "Spiral":
                Instantiate(element4Prefab, _whiteboard.transform.position, Quaternion.identity);
                break;
        }
    }
}