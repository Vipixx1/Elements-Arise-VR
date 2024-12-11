using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : Spell
{

    private void Start()
    {
        Temperature = -1;
        Humidity = -1;
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnIce(data);

    }
}
