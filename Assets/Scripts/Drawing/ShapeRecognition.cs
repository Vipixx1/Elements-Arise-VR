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
        
    
        void Start()
        { 
            _renderer = GetComponent<Renderer>();
            scroll = GetComponent<Scroll>();
        }
    
        void Update()
        {
        
            if (Input.GetKeyDown(KeyCode.D))
            {
                //parchment.EraseScroll();
                if (DrawableShapesList.Count == 0) return;
                _drawableShapes.DebugShape(DrawableShapesList[_currentShapeIndex], scroll, _pointSize);
                _currentShapeIndex = (_currentShapeIndex + 1) % DrawableShapesList.Count;
            }
            
            
            if(Input.GetKeyDown(KeyCode.B))
                DrawBoundingBox(15);
            
            RecognizeShape();
        }
        
        private void RecognizeShape()
        {
            foreach (DrawableShapeSO shape in DrawableShapesList)
            {
                if (!_drawableShapes.IsShape(shape, scroll)) continue;
                _text.text = shape.ShapeName;
                return;
            }
            _text.text = "Unknown";
        }
        
        
        private void DrawBoundingBox(int strokeWeight)
        {
            if (!ComputeDrawingBoundingBox(out Vector2 upperLeft, out Vector2 size))
                return;

            return;
            
            scroll.texture.SetPixels((int)(upperLeft.x * scroll.textureSize.x),
                (int)(upperLeft.y * scroll.textureSize.y), (int)size.x, strokeWeight,
                Enumerable.Repeat(Color.magenta, (int)size.x * strokeWeight).ToArray());
            scroll.texture.SetPixels((int)(upperLeft.x * scroll.textureSize.x),
                (int)(upperLeft.y * scroll.textureSize.y) + (int) size.y, (int)size.x, strokeWeight,
                Enumerable.Repeat(Color.magenta, (int)size.x * strokeWeight).ToArray());
            scroll.texture.SetPixels((int)(upperLeft.x * scroll.textureSize.x),
                (int)(upperLeft.y * scroll.textureSize.y), strokeWeight, (int) size.y,
                Enumerable.Repeat(Color.magenta, strokeWeight * (int) size.y).ToArray());
            scroll.texture.SetPixels((int) (upperLeft.x * scroll.textureSize.x) + (int) size.x,
                (int)(upperLeft.y * scroll.textureSize.y), strokeWeight, (int) size.y,
                Enumerable.Repeat(Color.magenta, strokeWeight * (int) size.y).ToArray());
            
            scroll.texture.Apply();
        }
        
        private bool ComputeDrawingBoundingBox(out Vector2 upperLeft, out Vector2 size)
        {
            int minX = GetMinPixelX();
            int minY = GetMinPixelY();
            int maxX = GetMaxPixelX();
            int maxY = GetMaxPixelY();
            upperLeft = new Vector2(minX / scroll.textureSize.x, minY / scroll.textureSize.y);
            size = new Vector2(maxX - minX, GetMaxPixelY() - maxY);
            scroll.texture.SetPixels(maxX, maxY, 10, 10, Enumerable.Repeat(Color.red, 100).ToArray());
            scroll.texture.SetPixels(minX, minY, 10, 10, Enumerable.Repeat(Color.cyan, 100).ToArray());
            scroll.texture.Apply();
            return true;
        }

        private int GetMinPixelX()
        {
            for (int i = 0; i < scroll.textureSize.y; i++)
            {
                for (int j = 0; j < scroll.textureSize.x; j++)
                {
                    if (scroll.texture.GetPixel(j, i) == Color.blue)
                        return j;
                } 
            }

            return 0;
        }
        
        private int GetMinPixelY()
        {
            for (int i = 0; i < scroll.textureSize.x; i++)
            {
                for (int j = 0; j < scroll.textureSize.y; j++)
                {
                    if (scroll.texture.GetPixel(i, j) == Color.blue)
                        return j;
                } 
            }

            return 0; 
        }
        
        private int GetMaxPixelY()
        {
            for (int i = (int) scroll.textureSize.x; i > 0; i--)
            {
                for (int j = (int) scroll.textureSize.y; j > 0; j--)
                {
                    if (scroll.texture.GetPixel(i, j) == Color.blue)
                        return j;
                } 
            }

            return 0; 
        }
        
        
        private int GetMaxPixelX()
        {
            for (int i = (int) scroll.textureSize.y; i > 0; i--)
            {
                for (int j = (int) scroll.textureSize.x; j > 0; j--)
                {
                    if (scroll.texture.GetPixel(j, i) == Color.blue)
                        return j;
                } 
            }

            return 0;
        }
        
    }
}
