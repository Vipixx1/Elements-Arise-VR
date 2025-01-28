using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using UnityEngine.Events;

public class Scroll : MonoBehaviour
{
    [SerializeField] ScrollSpellResult scrollSpellResult;
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);
    private Renderer r;

    // Centralized gesture points
    public List<Point> Points { get; private set; } = new List<Point>();
    private int strokeId = -1; // Keeps track of strokes

    //public UnityEvent EraseScrollEvent;

    void Start()
    {
        r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

    private void Update()
    {
        // Clear the parchment when the user presses the Start button
        /*if (OVRInput.Get(OVRInput.Button.Start))
        {
            EraseScroll();
        }*/

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
        if (scrollSpellResult.gameObject.GetComponent<Renderer>())
        {
            scrollSpellResult.gameObject.GetComponent<Renderer>().enabled = false;
        }
        //EraseScrollEvent?.Invoke();
    }

    public void StartStroke()
    {
        strokeId++;
    }

    public void DrawPoint(int x, int y, int width, int height, Color[] colors)
    {
        // Ensure the drawing starts and ends within the texture's bounds
        int clampedX = Mathf.Clamp(x, 0, texture.width - 1);
        int clampedY = Mathf.Clamp(y, 0, texture.height - 1);

        int clampedWidth = Mathf.Clamp(width, 0, texture.width - clampedX);
        int clampedHeight = Mathf.Clamp(height, 0, texture.height - clampedY);

        if (clampedWidth > 0 && clampedHeight > 0)
        {
            texture.SetPixels(clampedX, clampedY, clampedWidth, clampedHeight, colors);

            // Add gesture point, invert Y for consistency with PDollar
            Points.Add(new Point(clampedX, -clampedY, strokeId));
        }
    }

    public void FinalizeStroke()
    {
        // Additional handling for when a stroke ends, if needed
    }
}
