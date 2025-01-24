using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private int temperature;
    public int Temperature { get { return temperature; } set { temperature = value; } }
    [SerializeField] private int humidity;
    public int Humidity { get { return humidity; } set { humidity = value; } }
    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private bool shouldDispawn = true;

    public void Start()
    {
        if (shouldDispawn)
        {
            StartCoroutine(DestroyAfterTime(5f));
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroySpell();
    }

    public virtual void Catch(ObjectData data)
    {
        data.Temperature += temperature;
        data.Humidity += humidity;
        Debug.Log(data.Material);
    }

    public void DestroySpell()
    {
        foreach (Transform cc in transform) { cc.parent = null; cc.GetComponent<particleScript>().DestroyAfter(); }
        Destroy(gameObject);
    }
}
