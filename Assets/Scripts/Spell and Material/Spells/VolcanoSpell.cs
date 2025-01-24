using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoSpell : Spell
{

    private void Start()
    {
        Temperature = 3;
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnVolcano(data);
        data.Material.Burning(data);
    }
}
