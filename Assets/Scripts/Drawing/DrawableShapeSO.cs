using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawableShapeSO", menuName = "ScriptableObjects/DrawableShapeSO", order = 1)]
public class DrawableShapeSO : ScriptableObject
{
    public string ShapeName;
    public List<Vector2> MandatoryPoints;
    public List<Vector2> AvoidPoints;
}
