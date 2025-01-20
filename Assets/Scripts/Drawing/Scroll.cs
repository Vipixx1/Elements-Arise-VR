using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class Scroll : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);
    private Renderer r;

    // Centralized gesture points
    public List<Point> Points { get; private set; } = new List<Point>();
    private int strokeId = -1; // Keeps track of strokes

    void Start()
    {
        r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

    private void Update()
    {
        // Clear the parchment when the user presses the Start button
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            EraseScroll();
        }

        if (!Input.GetKey(KeyCode.Backspace) ||
            !Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit touch)) return;
        if (touch.transform.GetComponentInChildren<Scroll>().Equals(this))
        {
            EraseScroll();
        }
    }

    public void EraseScroll()
    {
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
        Points.Clear();
        strokeId = -1; // Reset stroke ID
    }

    public void StartStroke()
    {
        strokeId++;
    }

    public void DrawPoint(int x, int y, int width, int height, Color[] colors)
    {
        texture.SetPixels(x, y, width, height, colors);

        // Add gesture point, invert Y for consistency with PDollar
        Points.Add(new Point(x, -y, strokeId));
    }

    public void FinalizeStroke()
    {
        // Additional handling for when a stroke ends, if needed
    }
}
