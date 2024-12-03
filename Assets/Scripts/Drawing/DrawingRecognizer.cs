using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PDollarGestureRecognizer;

public class DrawingRecognizer : MonoBehaviour
{
    private List<Gesture> trainingSet = new List<Gesture>();

    [SerializeField] TMPro.TextMeshPro text;
    private string message;
    public bool IsRecognizing { get; set; } = false;
    public bool IsRecognized { get; set; } = false;
    private string gestureClass;
    [SerializeField] private Scroll scroll;

    [SerializeField] private ScrollSpellResult scrollSpellResult;

    void Start()
    {
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
        if (IsRecognizing)
        {
            RecognizeGesture();
            IsRecognizing = false;
        }

        if (IsRecognized)
        {
            SpawnElement();
            IsRecognized = false;
        }
    }

    private void RecognizeGesture()
    {
        Gesture candidate = new Gesture(scroll.Points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        message = gestureResult.GestureClass + " " + gestureResult.Score;
        text.text = message;

        if (gestureResult.Score < 0.85f)
        {
            scrollSpellResult.gameObject.SetActive(false);
            gestureClass = "Unknown";
            IsRecognized = false;
            return;
        }

        IsRecognized = true;
        gestureClass = gestureResult.GestureClass;
    }

    private void SpawnElement()
    {
        scrollSpellResult.transform.position = scroll.transform.position;
        switch (gestureClass)
        {
            case "Square":
                scrollSpellResult.SetCurrentSpellElement("earth");
                scrollSpellResult.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                break;
            case "Triangle":
                scrollSpellResult.SetCurrentSpellElement("fire");
                scrollSpellResult.gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case "Circle":
                scrollSpellResult.SetCurrentSpellElement("water");
                scrollSpellResult.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case "Spirale":
                scrollSpellResult.SetCurrentSpellElement("wind");
                scrollSpellResult.gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
        }
        scrollSpellResult.gameObject.SetActive(true);


    }
}