using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Drawing;
using Parchment.BoolDistance;
using PDollarGestureRecognizer;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BoolDistanceMethod : MonoBehaviour
{
    private Renderer _renderer;
    private Color[] _point;
    private int _pointSize = 10;

    [SerializeField] private NewMarker marker;
    [SerializeField] private Scroll scroll;
        
    private DrawableShapes _drawableShapes = new ();
    private int _currentShapeIndex = 0;
       
    public List<DrawableShapeSO> DrawableShapesList = new ();
    [SerializeField] private TextMeshPro _textA;
    [SerializeField] private TextMeshPro _textV;
    [SerializeField] private TextMeshPro _textFinal;
    
    private List<Gesture> _trainingSet = new ();
        
    private Vector2 _boundingBoxPosition = Vector2.zero;
    private Vector2 _boundingBoxSize = Vector2.zero;
    
    private string _gestureClass;
    
    public bool IsRecognized { get; set; } = false;

    private void Awake()
    {
        LoadGestures();
    }

    private void Start()
    { 
        _renderer = GetComponent<Renderer>();
        marker.writingUnityEvent.AddListener(
            (x, y, sizeX, sizeY) => _drawableShapes.CheckForBoundingBox(x, y, sizeX, sizeY, _boundingBoxPosition, _boundingBoxSize));
        scroll.EraseScrollEvent.AddListener(() => _drawableShapes.ResetBoundingBox(scroll, _boundingBoxPosition, _boundingBoxSize));
        _drawableShapes.ResetBoundingBox(scroll, _boundingBoxPosition, _boundingBoxSize);
    }
    
    private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                //parchment.EraseScroll();
                if (DrawableShapesList.Count == 0) return;
                _drawableShapes.DebugShape(DrawableShapesList[_currentShapeIndex], scroll, _boundingBoxPosition, _boundingBoxSize, _pointSize, true);
                _currentShapeIndex = (_currentShapeIndex + 1) % DrawableShapesList.Count;
            }
            
            
            if(Input.GetKeyDown(KeyCode.B))
                _drawableShapes.DrawBoundingBox(10, _boundingBoxPosition, _boundingBoxSize, scroll);
            
            
            RecognizeShape(_boundingBoxPosition, _boundingBoxSize);
            RecognizeGesture();
        }
        
        private void RecognizeShape(Vector2 boundingBoxPosition, Vector2 boundingBoxSize)
        {
            foreach (DrawableShapeSO shape in DrawableShapesList)
            {
                if (!_drawableShapes.IsShape(shape, scroll, boundingBoxPosition, boundingBoxSize, marker.color, true)) continue;
                _textA.text = shape.ShapeName;
                return;
            }
            _textA.text = "Unknown";
        }
        private void RecognizeGesture()
        {
            if (scroll.Points.Count < 300)
            {
                _textV.text = "Nothing";
                return;
            }

            Gesture candidate = new(scroll.Points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, _trainingSet.ToArray());

            if (gestureResult.Score < 0.90f)
            {
                //scrollSpellResult.gameObject.SetActive(false);
                _gestureClass = "Nothing";
                IsRecognized = false;
                return;
            }

            IsRecognized = true;
            _gestureClass = gestureResult.GestureClass;

            _textV.text = _gestureClass;
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
                _trainingSet.Add(GestureIO.ReadGestureFromXML(xml));
            }
        }
        
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
}
