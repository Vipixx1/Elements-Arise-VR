using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSpell : Spell
{
    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnSand(data);
    }
}
