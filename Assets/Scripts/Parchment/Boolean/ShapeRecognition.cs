using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
            ComputeBoundingBoxEveryXSeconds(1f);
        }

        private void ComputeBoundingBoxEveryXSeconds(float seconds)
        {
            InvokeRepeating(nameof(ComputeDrawingBoundingBox), 0f, seconds);
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
                if (!_drawableShapes.IsShape(shape, scroll, boundingBoxPosition, boundingBoxSize)) continue;
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
        
        private bool ComputeDrawingBoundingBox(out Vector2 squarePosition, out Vector2 size)
        {
            int minX = GetMinPixelX();
            int minY = GetMinPixelY();
            int maxX = GetMaxPixelX();
            int maxY = GetMaxPixelY();
            squarePosition = new Vector2(minX, minY);
            size = new Vector2(maxX - minX, maxY - minY);
            return minX != 0 || minY != 0 || maxX != 0 || maxY != 0;
        }
        
        private void ComputeDrawingBoundingBox()
        {
            int minX = GetMinPixelX();
            int minY = GetMinPixelY();
            int maxX = GetMaxPixelX();
            int maxY = GetMaxPixelY();
            _boundingBoxPosition = new Vector2(minX, minY);
            _boundingBoxSize = new Vector2(maxX - minX, maxY - minY);
        }

        private int GetMinPixelX()
        {
            for (int i = 0; i < scroll.textureSize.x; i++)
            {
                for (int j = 0; j < scroll.textureSize.y; j++)
                {
                    if (scroll.texture.GetPixel(i, j) == Color.blue)
                        return i;
                } 
            }

            return 0;
        }
        
        private int GetMinPixelY()
        {
            for (int i = 0; i < scroll.textureSize.y; i++)
            {
                for (int j = 0; j < scroll.textureSize.x; j++)
                {
                    if (scroll.texture.GetPixel(j, i) == Color.blue)
                        return i;
                } 
            }

            return 0; 
        }
        
        private int GetMaxPixelY()
        {
            for (int i = (int) scroll.textureSize.y - 1; i >= 0; i--)
            {
                for (int j = 0; j < scroll.textureSize.x; j++)
                {
                    if (scroll.texture.GetPixel(j, i) == Color.blue)
                        return i;
                } 
            }

            return 0; 
        }
        
        
        private int GetMaxPixelX()
        {
            for (int i = (int) scroll.textureSize.y - 1; i >= 0; i--)
            {
                for (int j = 0; j < scroll.textureSize.y; j++)
                {
                    if (scroll.texture.GetPixel(i, j) == Color.blue)
                        return i;
                } 
            }

            return 0;
        }
        
    }
}
