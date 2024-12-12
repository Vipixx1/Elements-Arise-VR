using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;

public class DrawingRecognizer : MonoBehaviour
{
    private List<Gesture> trainingSet = new List<Gesture>();

    [SerializeField] private TMPro.TextMeshPro text;
    private string message;
    public bool IsRecognized { get; set; } = false;
    private string gestureClass;

    [SerializeField] private Scroll scroll;
    [SerializeField] private ScrollSpellResult scrollSpellResult;

    void Start()
    {
        // Load user-defined gestures
        LoadGestures();
    }

    void Update()
    {
        RecognizeGesture();

        if (IsRecognized)
        {
            SpawnElement();
            IsRecognized = false;
        }
    }

    private void LoadGestures()
    {
        string folderPath = Path.Combine(Application.dataPath, "Shapes");
        string[] filePaths = System.IO.Directory.GetFiles(folderPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    private void RecognizeGesture()
    {
        if (scroll.Points.Count < 300)
        {
            message = "Nothing";
            text.text = message;
            return;
        }


        Gesture candidate = new(scroll.Points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        if (gestureResult.Score < 0.85f)
        {
            scrollSpellResult.gameObject.SetActive(false);
            gestureClass = "Nothing";
            IsRecognized = false;
            return;
        }

        IsRecognized = true;
        gestureClass = gestureResult.GestureClass;
    }

    private void SpawnElement()
    {
        if (scrollSpellResult == null) return;

        scrollSpellResult.transform.position = scroll.transform.position + scroll.transform.TransformDirection(Vector3.forward * 0.4f);
        Renderer spellRenderer = scrollSpellResult.gameObject.GetComponent<Renderer>();
        spellRenderer.enabled = true;
        
        if (gestureClass.StartsWith("square"))
        {
            message = "Square : Earth";
            scrollSpellResult.SetCurrentSpellElement("earth");
            spellRenderer.material.color = new Color(0.65f, 0.16f, 0.16f);
        }
        else if (gestureClass.StartsWith("triangle"))
        {
            message = "Triangle : Fire";
            scrollSpellResult.SetCurrentSpellElement("fire");
            spellRenderer.material.color = Color.red;
        }
        else if (gestureClass.StartsWith("circle"))
        {
            message = "Circle : Water";
            scrollSpellResult.SetCurrentSpellElement("water");
            spellRenderer.material.color = Color.blue;
        }
        else if (gestureClass.StartsWith("spiral"))
        {
            message = "Spiral : Wind";
            scrollSpellResult.SetCurrentSpellElement("wind");
            spellRenderer.material.color = new Color(0.31f, 0.78f, 0.47f);
        }
        else
        {
            scrollSpellResult.SetCurrentSpellElement("unknown");
            spellRenderer.material.color = Color.white;
        }

        text.text = message;
        scrollSpellResult.gameObject.SetActive(true);
    }
}
