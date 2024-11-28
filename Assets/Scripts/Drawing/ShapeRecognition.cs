using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShapeRecognition : MonoBehaviour
{
    private Renderer _renderer;
    private Whiteboard _whiteboard;
    private Color _colorMandatory = Color.green;
    private Color _colorAvoid = Color.red;
    private Color[] _point;
    private int _pointSize = 10;
    
    [SerializeField] private TextMeshPro _text;
    
    private List<Vector2> _squareMandatory = new List<Vector2>
    {
        new Vector2(0.2f, 0.2f),
        new Vector2(0.8f, 0.8f),
        new Vector2(0.2f, 0.8f),
        new Vector2(0.8f, 0.2f),
    };
    
    private List<Vector2> _squareAvoid = new List<Vector2>
    {
        new Vector2(0.5f, 0.5f),
    };
    
    
    private List<Vector2> _triangleMandatory = new List<Vector2>
    {
        new Vector2(0.5f, 0.2f),
        new Vector2(0.2f, 0.8f),
        new Vector2(0.8f, 0.8f),
    };
    
    private List<Vector2> _triangleAvoid = new List<Vector2>
    {
        new Vector2(0.5f, 0.5f),
        new Vector2(0.2f, 0.2f),
        new Vector2(0.8f, 0.2f),
    };
    
    void Start()
    { 
        _renderer = GetComponent<Renderer>();
        _whiteboard = GetComponent<Whiteboard>();
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            _whiteboard.EraseWhiteboard();
            DrawSquare();
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            _whiteboard.EraseWhiteboard();
            DrawTriangle();
        }
        
        if (IsSquare())
        {
            _text.text = "Carré";
            return;
        }
        
        if (IsTriangle())
        {
            _text.text = "Triangle";
            return;
        }
        
        _text.text = "Rien";
    }

    private bool IsSquare()
    {
        foreach (Vector2 point in _squareMandatory)
        {
            if(_whiteboard.texture.GetPixel((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y)) != Color.blue)
            {
                return false;
            }
        }
        
        foreach (Vector2 point in _squareAvoid)
        {
            if(_whiteboard.texture.GetPixel((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y)) == Color.blue)
            {
                return false;
            }
        }

        return true;
    }
    
    private bool IsTriangle()
    {
        foreach (Vector2 point in _triangleMandatory)
        {
            if(_whiteboard.texture.GetPixel((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y)) != Color.blue)
            {
                return false;
            }
        }
        
        foreach (Vector2 point in _triangleAvoid)
        {
            if(_whiteboard.texture.GetPixel((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y)) == Color.blue)
            {
                return false;
            }
        }

        return true;
    }
    
    private void DrawSquare()
    {
        foreach (Vector2 point in _squareMandatory)
        {
            _whiteboard.texture.SetPixels((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y), _pointSize, _pointSize, Enumerable.Repeat(_colorMandatory, _pointSize * _pointSize).ToArray());
        }
        
        foreach (Vector2 point in _squareAvoid)
        {
            _whiteboard.texture.SetPixels((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y), _pointSize, _pointSize, Enumerable.Repeat(_colorAvoid, _pointSize * _pointSize).ToArray());
        }
        
        _whiteboard.texture.Apply();
    }

    private void DrawTriangle()
    {
        foreach (Vector2 point in _triangleMandatory)
        {
            _whiteboard.texture.SetPixels((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y), _pointSize, _pointSize, Enumerable.Repeat(_colorMandatory, _pointSize * _pointSize).ToArray());
        }
        
        foreach (Vector2 point in _triangleAvoid)
        {
            _whiteboard.texture.SetPixels((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y), _pointSize, _pointSize, Enumerable.Repeat(_colorAvoid, _pointSize * _pointSize).ToArray());
        }
        
        _whiteboard.texture.Apply();
    }
}
