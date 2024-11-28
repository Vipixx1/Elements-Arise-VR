using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drawing
{
    public class DrawableShapes
    {

        public bool IsShape(DrawableShapeSO shape, Whiteboard _whiteboard)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                if(_whiteboard.texture.GetPixel((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y)) != Color.blue)
                {
                    return false;
                }
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                if(_whiteboard.texture.GetPixel((int) (point.x * _whiteboard.textureSize.x), (int) (point.y * _whiteboard.textureSize.y)) == Color.blue)
                {
                    return false;
                }
            }

            return true;
        }
        
        public void DebugShape(DrawableShapeSO shape, Whiteboard whiteboard, int pointSize)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                whiteboard.texture.SetPixels((int) (point.x * whiteboard.textureSize.x), (int) (point.y * whiteboard.textureSize.y), pointSize, pointSize, Enumerable.Repeat(Color.green, pointSize * pointSize).ToArray());
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                whiteboard.texture.SetPixels((int) (point.x * whiteboard.textureSize.x), (int) (point.y * whiteboard.textureSize.y), pointSize, pointSize, Enumerable.Repeat(Color.red, pointSize * pointSize).ToArray());
            }
            
            whiteboard.texture.Apply();
        }
    }
}


