using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Networking;

public class DrawingRecognizer : MonoBehaviour
{
    private List<Gesture> trainingSet = new List<Gesture>();

    [SerializeField] private TMPro.TextMeshPro text;
    private string message;
    public bool IsRecognized { get; set; } = false;
    private string gestureClass;

    [SerializeField] private Scroll scroll;
    [SerializeField] private ScrollSpellResult scrollSpellResult;

    void Awake()
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

    //
    private void LoadGestures()
    {
        string folderPath = Path.Combine(Application.streamingAssetsPath, "Shapes");
        List<string> gestureXmls = new List<string>();

        if (Application.platform == RuntimePlatform.Android)
        {
            // Handle Android's StreamingAssets path
            string manifestPath = Path.Combine(folderPath, "shapes_manifest.txt");
            string[] fileNames = GetFileListFromManifest(manifestPath);

            foreach (string fileName in fileNames)
            {
                string filePath = Path.Combine(folderPath, fileName);
                string xmlContent = ReadFileFromStreamingAssets(filePath);
                if (!string.IsNullOrEmpty(xmlContent))
                {
                    gestureXmls.Add(xmlContent);
                }
            }
        }
        else
        {
            // Use Directory.GetFiles for other platforms
            string[] filePaths = Directory.GetFiles(folderPath, "*.xml");

            foreach (string filePath in filePaths)
            {
                string xmlContent = File.ReadAllText(filePath);
                gestureXmls.Add(xmlContent);
            }
        }

        // Load gestures from the XML content
        foreach (string xml in gestureXmls)
        {
            trainingSet.Add(GestureIO.ReadGestureFromXML(xml));
        }
    }

    // Reads a file from StreamingAssets on Android
    private string ReadFileFromStreamingAssets(string filePath)
    {
        UnityWebRequest request = UnityWebRequest.Get(filePath);
        request.SendWebRequest();
        while (!request.isDone) { }

        if (request.result == UnityWebRequest.Result.Success)
        {
            return request.downloadHandler.text;
        }
        else
        {
            Debug.LogError($"Failed to read file from {filePath}: {request.error}");
            return null;
        }
    }

    // Reads a file list from the manifest
    private string[] GetFileListFromManifest(string manifestPath)
    {
        string content = ReadFileFromStreamingAssets(manifestPath);
        if (string.IsNullOrEmpty(content))
        {
            Debug.LogError("Manifest file is empty or missing.");
            return new string[0];
        }
        return content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    }
    //


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
