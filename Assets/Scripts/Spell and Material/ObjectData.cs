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

    private GameObject temperatureGO;
    private GameObject humidityGO;

    [SerializeField] bool hasParticle = false;

    private void OnDestroy()
    {
        if (temperatureGO != null) Destroy(temperatureGO);
        if (humidityGO != null) Destroy(humidityGO);
    }

    private void Start()
    {
        if (hasParticle)
        {
            UpdateHumidityParticle();
            UpdateTemperatureParticle();
        }
    }

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
            if (hasParticle)
            {
                UpdateHumidityParticle();
                UpdateTemperatureParticle();
            }
        }



    }

    private void UpdateHumidityParticle()
    {
        if (humidity == 0 ) { if (humidityGO != null) Destroy(humidityGO); return; }
        if (humidityGO == null)
        {
            humidityGO = Instantiate(new GameObject(), transform.position, Quaternion.identity);
            humidityGO.transform.eulerAngles = new Vector3(0, 0, 180);

            humidityGO.AddComponent<ParticleSystem>();
        }
        if (humidity > 0)
            humidityGO.transform.localScale = Vector3.one * 0.1f;
        else
        {
            humidityGO.transform.localScale = Vector3.one * 0.02f;
        }
        ParticleSystem particleSystem = humidityGO.GetComponent<ParticleSystem>();
        particleSystem.Stop();
        ParticleSystem.MainModule main = particleSystem.main;
        
        UnityEngine.Material matWat = humidity > 0 ? Resources.Load("orb") as UnityEngine.Material : Resources.Load("smoke") as UnityEngine.Material;
        particleSystem.GetComponent<ParticleSystemRenderer>().material = matWat;
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        emission.rateOverTime = (humidity > 0) ? Mathf.Clamp(Mathf.Abs(humidity),0,5) * 5 : Mathf.Clamp(Mathf.Abs(humidity), 0, 5) * 50;
        
        main.startRotation3D = true;
       
        var shapeModule = particleSystem.shape;
        shapeModule.angle = 1;
        shapeModule.rotation = new Vector3(-90, 0, 0);
        shapeModule.radius = transform.lossyScale.z * ((humidity > 0) ? 5.5f : 27f);
        shapeModule.scale = new Vector3(1, 1, 0.10f);
        main.gravityModifier = 0.5f;
        float alpha = 1.0f;
        //float ratio = Mathf.Abs(temperature) / ( Mathf.Abs(humidity) + (float)Mathf.Abs(temperature));
        var grad = new Gradient();
        //Debug.Log(ratio);
        //Color tempColor = (temperature < 0) ? Color.cyan : Color.red;
        Color HumiColor = (humidity < 0) ? Color.yellow : Color.blue;
        grad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(HumiColor, 1) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f) }
        );
        main.startColor = grad;
        particleSystem.Play();
    }

    void UpdateTemperatureParticle() { 

        if (temperature == 0 ) { if (temperatureGO != null) Destroy(temperatureGO); return; }

        if (temperatureGO == null)
        {
            temperatureGO = Instantiate(new GameObject(), transform.position, Quaternion.identity);
            temperatureGO.transform.eulerAngles = new Vector3(0, 0, 180);
            temperatureGO.transform.localScale = Vector3.one * 0.1f;
            temperatureGO.AddComponent<ParticleSystem>();
        }



        ParticleSystem particleSystemtemp = temperatureGO.GetComponent<ParticleSystem>();
        particleSystemtemp.Stop();
        ParticleSystem.MainModule maintemp = particleSystemtemp.main;
        UnityEngine.Material mattemp = temperature>0 ? Resources.Load("Smoke") as UnityEngine.Material : Resources.Load("snowflake") as UnityEngine.Material;
        particleSystemtemp.GetComponent<ParticleSystemRenderer>().material = mattemp;
        ParticleSystem.EmissionModule emissiontemp = particleSystemtemp.emission;
        emissiontemp.rateOverTime = Mathf.Clamp(Mathf.Abs(temperature), 0, 5)*5;
        maintemp.startRotation3D = true;
        
        var shapeModuletemp = particleSystemtemp.shape;
        shapeModuletemp.rotation = (temperature < 0) ?  new Vector3(-90, 0, 0) : new Vector3(90, 0, 0);
        shapeModuletemp.angle = 1;
        shapeModuletemp.radius = transform.lossyScale.z * 5.5f;
        shapeModuletemp.scale = new Vector3(1, 1, 0.10f);
        maintemp.gravityModifier = (temperature > 0) ? 0 : 0.5f;
        // float ratio = Mathf.Abs(temperature) / (Mathf.Abs(humidity) + (float)Mathf.Abs(temperature));
        float alpha = 1.0f;
        var gradtemp = new Gradient();
        Color tempColor = (temperature < 0) ? Color.cyan : Color.grey;

        gradtemp.SetKeys(
            new GradientColorKey[] {  new GradientColorKey(tempColor, 1) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1.0f) }
        );
        maintemp.startColor = gradtemp;
        particleSystemtemp.Play();
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