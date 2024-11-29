using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpell : Spell
{
    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnEarth(data);
    }
}
