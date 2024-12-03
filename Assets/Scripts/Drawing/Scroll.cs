using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class Scroll : MonoBehaviour
{
    private Scroll scroll;

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
        // Clear the scroll when the user presses the Start button
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            EraseScroll();
        }
    }

    public void EraseScroll()
    {
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
        Points.Clear();

    }

    public void DrawPoint(int x, int y, int width, int height, Color[] colors)
    {
        texture.SetPixels(x, y, width, height, colors);
        Points.Add(new Point(x, y, 0));
    }
}
