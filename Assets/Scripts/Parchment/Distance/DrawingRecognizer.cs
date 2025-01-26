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

        if (gestureResult.Score < 0.90f)
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
        ParticleSystem selectedParticles = null;
        ParticleSystem[] allParticles = scrollSpellResult.GetComponentsInChildren<ParticleSystem>();

        scrollSpellResult.transform.position = scroll.transform.position - scroll.transform.TransformDirection(Vector3.forward * 0.25f);
        scrollSpellResult.gameObject.SetActive(true);

        if (gestureClass.StartsWith("earth"))
        {
            message = "Earth (Square)";
            scrollSpellResult.SetCurrentSpellElement("earth");
            selectedParticles = scrollSpellResult.transform.Find("EarthParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("fire"))
        {
            message = "Fire (Triangle)";
            scrollSpellResult.SetCurrentSpellElement("fire");
            selectedParticles = scrollSpellResult.transform.Find("FireParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("water"))
        {
            message = "Water (Circle)";
            scrollSpellResult.SetCurrentSpellElement("water");
            selectedParticles = scrollSpellResult.transform.Find("WaterParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("wind"))
        {
            message = "Wind (Spiral)";
            scrollSpellResult.SetCurrentSpellElement("wind");
            selectedParticles = scrollSpellResult.transform.Find("WindParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("thunder"))
        {
            message = "Thunder (Lightning)";
            scrollSpellResult.SetCurrentSpellElement("thunder");
            selectedParticles = scrollSpellResult.transform.Find("ThunderParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("sand"))
        {
            message = "Sand (Hourglass)";
            scrollSpellResult.SetCurrentSpellElement("sand");
            selectedParticles = scrollSpellResult.transform.Find("SandParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("volcano"))
        {
            message = "Volcano (M)";
            scrollSpellResult.SetCurrentSpellElement("volcano");
            selectedParticles = scrollSpellResult.transform.Find("VolcanoParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("ice"))
        {
            message = "Ice (Diamond)";
            scrollSpellResult.SetCurrentSpellElement("ice");
            selectedParticles = scrollSpellResult.transform.Find("IceParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("plant"))
        {
            message = "Plant (Leaf)";
            scrollSpellResult.SetCurrentSpellElement("plant");
            selectedParticles = scrollSpellResult.transform.Find("PlantParticles").GetComponent<ParticleSystem>();
        }
        else if (gestureClass.StartsWith("steam"))
        {
            message = "Steam (3 Lines)";
            scrollSpellResult.SetCurrentSpellElement("steam");
            selectedParticles = scrollSpellResult.transform.Find("SteamParticles").GetComponent<ParticleSystem>();
        }
        else
        {
            scrollSpellResult.SetCurrentSpellElement("Unknown");
        }

        for (int i = 0; i < allParticles.Length; i++)
        {
            if (allParticles[i] != selectedParticles)
            {
                allParticles[i].gameObject.SetActive(false);
                allParticles[i].gameObject.GetComponent<Renderer>().enabled = false;
                allParticles[i].Stop();
            }
        }

        // Play the selected particle system
        if (selectedParticles != null)
        {
            selectedParticles.gameObject.SetActive(true);
            selectedParticles.gameObject.GetComponent<Renderer>().enabled = true;
            selectedParticles.Play();
        }
    }

}
