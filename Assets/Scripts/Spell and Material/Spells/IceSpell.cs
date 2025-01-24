using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceSpell : Spell
{
    public new void Start()
    {
        base.Start();
        Temperature = -1;
        Humidity = -1;
    }

    public override void Catch(ObjectData data)
    {
        base.Catch(data);
        data.Material.OnIce(data, new float[] { transform.position.x , transform.position.y, transform.position.z} );

    }
}
