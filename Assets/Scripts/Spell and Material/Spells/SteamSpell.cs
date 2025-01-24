using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamSpell : Spell
{

    private void Start()
    {
        Humidity = 1;
        Temperature = 1;
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnSteam(data);
    }
}
