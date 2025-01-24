using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drawing
{
    public class DrawableShapes
    {

        public bool IsShape(DrawableShapeSO shape, Scroll scroll, Vector2 boundingBoxPosition, Vector2 boundingBoxSize)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                int x = (int)(point.x * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(point.y * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                if(scroll.texture.GetPixel(x, y) != Color.blue)
                {
                    return false;
                }
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                int x = (int)(point.x * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(point.y * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                if(scroll.texture.GetPixel(x, y) == Color.blue)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsShape(DrawableShapeSO shape, Scroll scroll)
        {
            return IsShape(shape, scroll, new Vector2(0, 0), new Vector2(scroll.textureSize.x, scroll.textureSize.y));
        }
        
        public void DebugShape(DrawableShapeSO shape, Scroll scroll, int pointSize)
        {
            DebugShape(shape, scroll, new Vector2(0, 0), new Vector2(scroll.textureSize.x, scroll.textureSize.y), pointSize);
        }
        
        public void DebugShape(DrawableShapeSO shape, Scroll scroll, Vector2 boundingBoxPosition, Vector2 boundingBoxSize, int pointSize)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                int x = (int)(point.x * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(point.y * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                scroll.texture.SetPixels(x, y, pointSize, pointSize, Enumerable.Repeat(Color.green, pointSize * pointSize).ToArray());
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                int x = (int)(point.x * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(point.y * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                scroll.texture.SetPixels(x, y, pointSize, pointSize, Enumerable.Repeat(Color.red, pointSize * pointSize).ToArray());
            }
            
            scroll.texture.Apply();
        }
        
        
    }
}


