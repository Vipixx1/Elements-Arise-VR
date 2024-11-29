using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoSpell : Spell
{
    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnVolcano(data);
        data.Material.Burning(data);
    }
}
