using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class Whiteboard : MonoBehaviour
{
    private Whiteboard _whiteboard;

    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);
    private Renderer r;

    public List<Point> Points { get; set; } = new List<Point>();

    void Start()
    {
        r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

    private void Update()
    {
        // Clear the whiteboard when the user presses the Start button
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            EraseWhiteboard();
        }
    }

    public void EraseWhiteboard()
    {
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
        Points.Clear();

    }

    public void DrawPoint(int x, int y, int width, int height, Color[] colors)
    {
        texture.SetPixels(x, y, width, height, colors);
        //texture.Apply();
        Points.Add(new Point(x, y, 0));
    }
}
