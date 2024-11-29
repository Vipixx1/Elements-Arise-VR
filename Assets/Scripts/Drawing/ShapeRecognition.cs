using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Drawing
{
    public class ShapeRecognition : MonoBehaviour
    {
        
        private Renderer _renderer;
        private Whiteboard _whiteboard;
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
            _whiteboard = GetComponent<Whiteboard>();
        }
    
        void Update()
        {
        
            if (Input.GetKeyDown(KeyCode.D))
            {
                _whiteboard.EraseWhiteboard();
                _drawableShapes.DebugShape(DrawableShapesList[_currentShapeIndex], _whiteboard, _pointSize);
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
                if (!_drawableShapes.IsShape(shape, _whiteboard)) continue;
                _text.text = shape.ShapeName;
                return;
            }
            _text.text = "Rien";
        }
    }
}
