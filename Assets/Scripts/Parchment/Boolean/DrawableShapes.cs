using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drawing
{
    public class DrawableShapes
    {

        public bool IsShape(DrawableShapeSO shape, Scroll scroll, Vector2 boundingBoxPosition, Vector2 boundingBoxSize, Color color, bool invert = false)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                float pointX = invert ? 1 - point.x : point.x;
                float pointY = invert ? 1 - point.y : point.y;
                int x = (int)(pointX * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(pointY * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                if(scroll.texture.GetPixel(x, y) != color)
                {
                    return false;
                }
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                float pointX = invert ? 1 - point.x : point.x;
                float pointY = invert ? 1 - point.y : point.y;
                int x = (int)(pointX * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(pointY * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                if(scroll.texture.GetPixel(x, y) == color)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsShape(DrawableShapeSO shape, Scroll scroll, Color color, bool invert = false)
        {
            return IsShape(shape, scroll, new Vector2(0, 0), new Vector2(scroll.textureSize.x, scroll.textureSize.y), color, invert);
        }
        
        public void DebugShape(DrawableShapeSO shape, Scroll scroll, int pointSize, bool invert = false)
        {
            DebugShape(shape, scroll, new Vector2(0, 0), new Vector2(scroll.textureSize.x, scroll.textureSize.y), pointSize, invert);
        }
        
        public void DebugShape(DrawableShapeSO shape, Scroll scroll, Vector2 boundingBoxPosition, Vector2 boundingBoxSize, int pointSize, bool invert = false)
        {
            foreach (Vector2 point in shape.MandatoryPoints)
            {
                float pointX = invert ? 1 - point.x : point.x;
                float pointY = invert ? 1 - point.y : point.y;
                int x = (int)(pointX * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(pointY * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                scroll.texture.SetPixels(x, y, pointSize, pointSize, Enumerable.Repeat(Color.green, pointSize * pointSize).ToArray());
            }
        
            foreach (Vector2 point in shape.AvoidPoints)
            {
                float pointX = invert ? 1 - point.x : point.x;
                float pointY = invert ? 1 - point.y : point.y;
                int x = (int)(pointX * boundingBoxSize.x) + (int)boundingBoxPosition.x;
                int y = (int)(pointY * boundingBoxSize.y) + (int)boundingBoxPosition.y;
                scroll.texture.SetPixels(x, y, pointSize, pointSize, Enumerable.Repeat(Color.red, pointSize * pointSize).ToArray());
            }
            
            scroll.texture.Apply();
        }
        
        
        public void DrawBoundingBox(int strokeWeight, Vector2 squarePosition, Vector2 size, Scroll scroll)
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

        public void CheckForBoundingBox(int x, int y, int sizeX, int sizeY, Vector2 boundingBoxPosition, Vector2 boundingBoxSize)
        {
            Vector2 size = new (sizeX, sizeY);
            Vector2 p2 = boundingBoxPosition + boundingBoxSize;
            boundingBoxPosition.x = Mathf.Min(x, boundingBoxPosition.x);
            boundingBoxPosition.y = Mathf.Min(y, boundingBoxPosition.y);
            p2.x = Mathf.Max(x + size.x, p2.x);
            p2.y = Mathf.Max(y + size.y, p2.y);

            if (boundingBoxSize.Equals(Vector2.zero))
            {
                boundingBoxSize = size;
                return;
            }
            
            boundingBoxSize = p2 - boundingBoxPosition;
        }

        public void ResetBoundingBox(Scroll scroll, Vector2 boundingBoxPosition, Vector2 boundingBoxSize)
        {
            boundingBoxPosition = scroll.textureSize;
            boundingBoxSize = Vector2.zero;
        }
        
    }
}


