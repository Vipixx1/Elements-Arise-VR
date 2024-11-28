using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamSpell : Spell
{
    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnSteam(data);
    }
}
