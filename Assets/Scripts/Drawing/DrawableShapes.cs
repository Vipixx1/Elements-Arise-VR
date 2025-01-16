using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drawing
{
    public class DrawableShapes
    {

        public bool IsShape(DrawableShapeSO shape, Scroll scroll)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                if(scroll.texture.GetPixel((int) (point.x * scroll.textureSize.x), (int) (point.y * scroll.textureSize.y)) != Color.blue)
                {
                    return false;
                }
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                if(scroll.texture.GetPixel((int) (point.x * scroll.textureSize.x), (int) (point.y * scroll.textureSize.y)) == Color.blue)
                {
                    return false;
                }
            }

            return true;
        }
        
        public void DebugShape(DrawableShapeSO shape, Scroll scroll, int pointSize)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                scroll.texture.SetPixels((int) (point.x * scroll.textureSize.x), (int) (point.y * scroll.textureSize.y), pointSize, pointSize, Enumerable.Repeat(Color.green, pointSize * pointSize).ToArray());
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                scroll.texture.SetPixels((int) (point.x * scroll.textureSize.x), (int) (point.y * scroll.textureSize.y), pointSize, pointSize, Enumerable.Repeat(Color.red, pointSize * pointSize).ToArray());
            }
            
            scroll.texture.Apply();
        }
        
    }
}


