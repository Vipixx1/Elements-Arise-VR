using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Material = Magic.Materials.Material;

public class ObjectData : MonoBehaviour
{

    [SerializeField]
    private int temperature;

    public int Temperature { get { return temperature; } set { temperature = value; } }


    [SerializeField]
    private int humidity;

    public int Humidity { get { return humidity; } set { humidity = value; } }

    [SerializeField]
    private float mass;

    [SerializeField] Material material;
    public Material Material { get { return material; } set { material = value; } }


    public bool electrified;

    private ParticleSystem particleSystem = null;

    private void Update()
    {
        //Debug.Log(particleSystem.GetComponent<ParticleSystemRenderer>().material);
    }

    private void OnTriggerEnter(Collider other)
    {

        Spell spell = other.GetComponent<Spell>();

        if (spell != null)
        {
            spell.Catch(this);
            spell.DestroySpell();
        }


        updateParticleSystem();

    }

    private void updateParticleSystem()
    {
        if (temperature == 0 && humidity == 0) { Destroy(particleSystem); particleSystem = null; return; }
        if (particleSystem == null)
            particleSystem = gameObject.AddComponent<ParticleSystem>();

        ParticleSystem.MainModule main = particleSystem.main;
        UnityEngine.Material mat = Resources.Load("Particle") as UnityEngine.Material;
        particleSystem.GetComponent<ParticleSystemRenderer>().material = mat;
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        emission.rateOverTime = Mathf.Abs(temperature) + Mathf.Abs(humidity);
        main.startRotation3D = true ;
        main.duration = 1;
        var shapeModule = particleSystem.shape;
        shapeModule.rotation = new Vector3(-90, 0, 0);
        shapeModule.angle = 1;
        shapeModule.radius = 0.25f;
        shapeModule.scale = new Vector3(1, 1, 0.10f);
        main.gravityModifier = 0.5f;
        float alpha = 1.0f;
        float ratio = Mathf.Abs(temperature) / ( Mathf.Abs(humidity) + (float)Mathf.Abs(temperature));
        var grad = new Gradient();
        Debug.Log(ratio);
        Color tempColor = (temperature < 0) ? Color.cyan : Color.red;
        Color HumiColor = (humidity < 0) ? Color.yellow : Color.blue;
        grad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(HumiColor, 1-ratio - 0.001f), new GradientColorKey(tempColor, 1-ratio+0.001f ) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        main.startColor = grad;

    }
}

public enum SpellType
{
    EARTH,
    FIRE,
    ICE,
    PLANT,
    SAND,
    STEAM,
    THUNDER,
    VOLCANO,
    WATER,
    WIND,
}