using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpell : Spell
{
    private void Start()
    {
        Humidity = 3;
        Temperature = -1;
    }


    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnWater(data);
    }
}
