using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpell : Spell
{
    public new void Start()
    {
        base.Start();
        Humidity = -1;
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnWind(data);
    }

    private void OnDestroy()
    {
        FindAnyObjectByType<ShootingManager>().WindFieldPlacement(transform.position);
    }
}
