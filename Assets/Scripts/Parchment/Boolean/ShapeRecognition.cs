using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drawing
{
    public class ShapeRecognition : MonoBehaviour
    {
        
        private Renderer _renderer;
        private Scroll scroll;
        private Color _colorMandatory = Color.green;
        private Color _colorAvoid = Color.red;
        private Color[] _point;
        private int _pointSize = 10;

        [SerializeField] private OldMarker marker;
        
        private DrawableShapes _drawableShapes = new DrawableShapes();
        private int _currentShapeIndex = 0;
       
        public List<DrawableShapeSO> DrawableShapesList = new List<DrawableShapeSO>();
        [SerializeField] private TextMeshPro _text;
        
        private Vector2 _boundingBoxPosition = Vector2.zero;
        private Vector2 _boundingBoxSize = Vector2.zero;
        
    
        private void Start()
        { 
            _renderer = GetComponent<Renderer>();
            scroll = GetComponent<Scroll>();
            marker.writingUnityEvent.AddListener(CheckForBoundingBox);
            scroll.EraseScrollEvent.AddListener(ResetBoundingBox);
            ResetBoundingBox();
        }
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                //parchment.EraseScroll();
                if (DrawableShapesList.Count == 0) return;
                _drawableShapes.DebugShape(DrawableShapesList[_currentShapeIndex], scroll, _boundingBoxPosition, _boundingBoxSize, _pointSize);
                _currentShapeIndex = (_currentShapeIndex + 1) % DrawableShapesList.Count;
            }
            
            
            if(Input.GetKeyDown(KeyCode.B))
                DrawBoundingBox(10, _boundingBoxPosition, _boundingBoxSize);
            
            
            RecognizeShape(_boundingBoxPosition, _boundingBoxSize);
        }
        
        private void RecognizeShape(Vector2 boundingBoxPosition, Vector2 boundingBoxSize)
        {
            foreach (DrawableShapeSO shape in DrawableShapesList)
            {
                if (!_drawableShapes.IsShape(shape, scroll, boundingBoxPosition, boundingBoxSize, Color.blue)) continue;
                _text.text = shape.ShapeName;
                return;
            }
            _text.text = "Unknown";
        }
        
        
        private void DrawBoundingBox(int strokeWeight, Vector2 squarePosition, Vector2 size)
        {
            scroll.texture.SetPixels((int)squarePosition.x, (int)squarePosition.y, (int)size.x, strokeWeight,
                Enumerable.Repeat(Color.magenta, (int)size.x * strokeWeight).ToArray());
            scroll.texture.SetPixels((int)squarePosition.x, (int)squarePosition.y + (int) size.y, (int)size.x, strokeWeight,
                Enumerable.Repeat(Color.magenta, (int)size.x * strokeWeight).ToArray());
            scroll.texture.SetPixels((int)squarePosition.x, (int)squarePosition.y, strokeWeight, (int) size.y,
                Enumerable.Repeat(Color.magenta, strokeWeight * (int) size.y).ToArray());
            scroll.texture.SetPixels((int)squarePosition.x + (int) size.x, (int)squarePosition.y, strokeWeight, (int) size.y + strokeWeight,
                Enumerable.Repeat(Color.magenta, strokeWeight * ((int) size.y + strokeWeight)).ToArray());
            
            scroll.texture.Apply();
        }

        private void CheckForBoundingBox(int x, int y, int sizeX, int sizeY)
        {
            Vector2 size = new (sizeX, sizeY);
            Vector2 p2 = _boundingBoxPosition + _boundingBoxSize;
            _boundingBoxPosition.x = Mathf.Min(x, _boundingBoxPosition.x);
            _boundingBoxPosition.y = Mathf.Min(y, _boundingBoxPosition.y);
            p2.x = Mathf.Max(x + size.x, p2.x);
            p2.y = Mathf.Max(y + size.y, p2.y);

            if (_boundingBoxSize.Equals(Vector2.zero))
            {
                _boundingBoxSize = size;
                return;
            }
            
            _boundingBoxSize = p2 - _boundingBoxPosition;
        }

        private void ResetBoundingBox()
        {
            _boundingBoxPosition = scroll.textureSize;
            _boundingBoxSize = Vector2.zero;
        }
        
    }
}
