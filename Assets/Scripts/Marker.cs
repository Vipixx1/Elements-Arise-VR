using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PDollarGestureRecognizer;
using System;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 25;
    [SerializeField] private float _tipHeight = 0.03f;
    [SerializeField] private Color _color = Color.blue;

    private Color[] _colors;
    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    private List<Gesture> trainingSet = new List<Gesture>();
    private List<Point> points = new List<Point>();
    private int strokeId = -1;
    private bool recognized;
    private string message = "";

    void Start()
    {
        _colors = Enumerable.Repeat(_color, _penSize * _penSize).ToArray();

        // Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        Debug.Log(gesturesXml.Length);
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        // Load user-defined gestures
        string[] filePaths = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    void Update()
    {
        Draw();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecognizeGesture();
        }

        if (OVRInput.Get(OVRInput.Button.Start))
        {
            points.Clear();
            strokeId = -1;
        }
    }

    private void Draw()
    {
        _colors = Enumerable.Repeat(_color, _penSize * _penSize).ToArray();

        if (Physics.Raycast(_tip.position, _tip.transform.right, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

                _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                points.Add(new Point(_touchPos.x, _touchPos.y, strokeId)); // Track gesture points

                if (_touchedLastFrame)
                {
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _lastTouchRot;
                }

                _whiteboard.texture.Apply();

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }

    private void RecognizeGesture()
    {
        recognized = true;

        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        message = gestureResult.GestureClass + " " + gestureResult.Score;
        Debug.Log(message);

        // Clear for the next gesture
        /*points.Clear();
        strokeId = -1;*/
    }

    public void AddNewGesture(string gestureName)
    {
        if (points.Count > 0 && !string.IsNullOrEmpty(gestureName))
        {
            string fileName = $"{Application.persistentDataPath}/{gestureName}-{DateTime.Now.ToFileTime()}.xml";

            GestureIO.WriteGesture(points.ToArray(), gestureName, fileName);
            trainingSet.Add(new Gesture(points.ToArray(), gestureName));

            Debug.Log($"Gesture '{gestureName}' added!");
        }
    }
}
