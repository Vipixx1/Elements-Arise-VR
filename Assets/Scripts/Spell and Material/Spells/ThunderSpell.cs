using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell
{
    public new void Start()
    {
        base.Start();
        Temperature = 1;
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnThunder(data);
    }
}
