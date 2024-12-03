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
                scroll.EraseScroll();
                _drawableShapes.DebugShape(DrawableShapesList[_currentShapeIndex], scroll, _pointSize);
                _currentShapeIndex = (_currentShapeIndex + 1) % DrawableShapesList.Count;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                RecognizeShape();
            }
            
        }
        
        private void RecognizeShape()
        {
            foreach (DrawableShapeSO shape in DrawableShapesList)
            {
                if (!_drawableShapes.IsShape(shape, scroll)) continue;
                _text.text = shape.ShapeName;
                return;
            }
            _text.text = "Rien";
        }
    }
}
