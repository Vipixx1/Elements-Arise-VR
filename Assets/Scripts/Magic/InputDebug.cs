using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class InputDebug : MonoBehaviour
{

    Vector3 pos;

    public float offset = 3f;





    void Update()

    {

        pos = Input.mousePosition;

        pos.z = 10;

        transform.position = Camera.main.ScreenToWorldPoint(pos);

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            foreach (Component comp in gameObject.GetComponents<Spell>())
            {
                Destroy(comp);
            }
            FireSpell spell = gameObject.AddComponent<FireSpell>();
            spell.Humidity = -1;
            spell.Temperature = 1;
        }        
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Component comp in gameObject.GetComponents<Spell>())
            {
                Destroy(comp);
            }
            VolcanoSpell spell = gameObject.AddComponent<VolcanoSpell>();
            spell.Humidity = -1;
            spell.Temperature = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (Component comp in gameObject.GetComponents<Spell>())
            {
                Destroy(comp);
            }
            IceSpell spell = gameObject.AddComponent<IceSpell>();
            spell.Humidity = -1;
            spell.Temperature = -1;
        }
        else  if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (Component comp in gameObject.GetComponents<Spell>())
            {
                Destroy(comp);
            }
            gameObject.AddComponent<PlantSpell>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach (Component comp in gameObject.GetComponents<Spell>())
            {
                Destroy(comp);
            }
            WaterSpell spell = gameObject.AddComponent<WaterSpell>();
            spell.Humidity = 1;
            spell.Temperature = -1;
        }
            
            

    }

}
