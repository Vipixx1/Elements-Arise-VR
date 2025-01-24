using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpell : Spell
{
    public new void Start()
    {
        base.Start();
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnPlant(data);
    }


}
