using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PDollarGestureRecognizer;

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
        // Load user-defined gestures
        string[] filePaths = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    private void RecognizeGesture()
    {
        if (scroll.Points.Count < 200)
        {
            message = "Not enough points drawn";
            text.text = message;
            return;
        }


        Gesture candidate = new(scroll.Points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        message = $"{gestureResult.GestureClass} {gestureResult.Score:F2}";
        text.text = message;

        if (gestureResult.Score < 0.85f)
        {
            // Hide the spell result if gesture is unrecognized
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
        if (scrollSpellResult == null) return;

        scrollSpellResult.transform.position = scroll.transform.position;
        Renderer spellRenderer = scrollSpellResult.gameObject.GetComponent<Renderer>();

        if (gestureClass.StartsWith("square"))
        {
            scrollSpellResult.SetCurrentSpellElement("earth");
            spellRenderer.material.color = Color.gray;
        }
        else if (gestureClass.StartsWith("triangle"))
        {
            scrollSpellResult.SetCurrentSpellElement("fire");
            spellRenderer.material.color = Color.red;
        }
        else if (gestureClass.StartsWith("circle"))
        {
            scrollSpellResult.SetCurrentSpellElement("water");
            spellRenderer.material.color = Color.blue;
        }
        else if (gestureClass.StartsWith("spiral"))
        {
            scrollSpellResult.SetCurrentSpellElement("wind");
            spellRenderer.material.color = Color.green;
        }
        else
        {
            scrollSpellResult.SetCurrentSpellElement("unknown");
            spellRenderer.material.color = Color.white;
        }

        scrollSpellResult.gameObject.SetActive(true);
    }
}
