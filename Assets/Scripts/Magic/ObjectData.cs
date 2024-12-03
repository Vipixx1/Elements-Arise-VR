using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = Magic.Materials.Material;

public class ObjectData : MonoBehaviour
{

    [SerializeField]
    private int temperature;

    public int Temperature {  get { return temperature; } set { temperature = value; } }


    [SerializeField]
    private int humidity;

    public int Humidity { get { return humidity; } set { humidity = value; } }

    [SerializeField]
    private float mass;

    [SerializeField] Material material;
    public Material Material { get { return material; } set { material = value; } }


    public bool electrified;



    private void OnTriggerEnter(Collider other)
    {

        Spell spell = other.GetComponent<Spell>();

        if (spell != null)
        {
            spell.Catch(this);
        }



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