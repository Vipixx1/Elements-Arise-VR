using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Spell
{

    private void Start()
    {
        Temperature = 1;
        Humidity = -1;
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnFire(data);
        data.Material.Burning(data);
    }
}
