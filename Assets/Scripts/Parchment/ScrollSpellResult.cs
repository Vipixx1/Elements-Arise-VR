using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

#nullable enable

public class ScrollSpellResult : MonoBehaviour
{
    public static event System.Action<string, string> OnSpellReady;
    [SerializeField] private string currentSpellElement = "fire";
    [SerializeField] private EventReference collectSoundEvent;
    private Vector3 spawnCoords;
    

    private string currentHandTag = "";

    public void Awake()
    {
        spawnCoords = this.gameObject.transform.position;
        Debug.Log("Spawn coords: " + spawnCoords);
    }

    public void OnSelect()
    {

        if (currentHandTag == "")
        {
            return;
        }
        HideGrabbableSphere();

        PlayCollectSound(currentSpellElement);
        OnSpellReady?.Invoke(currentSpellElement, currentHandTag);
    }

    private void PlayCollectSound(string spell)
    {
        EventInstance collectSoundInstance = RuntimeManager.CreateInstance(collectSoundEvent);
        collectSoundInstance.setParameterByNameWithLabel("element", spell.ToUpper());
        RuntimeManager.AttachInstanceToGameObject(collectSoundInstance, gameObject);
        collectSoundInstance.start();
        collectSoundInstance.release();
    }

    public void Reset()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = spawnCoords;
        if (this.gameObject.GetComponent<Renderer>())
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
        }
        if (this.gameObject.GetComponent<ParticleSystem>())
        {
            this.gameObject.GetComponent<ParticleSystem>().Play();
        }
        this.gameObject.SetActive(true);
    }

    public void SetCurrentSpellElement(string element)
    {
        currentSpellElement = element;
    }
    public string GetCurrentSpellElement()
    {
        return currentSpellElement;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            currentHandTag = other.gameObject.tag;
            Debug.Log("ENTER : " + currentHandTag);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            currentHandTag = "";
            Debug.Log("EXIT");
        }
    }

    public void OnSelectLevel(string title)
    {
        SceneManager.LoadScene(title);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HideGrabbableSphere()
    {
        if (this.gameObject.GetComponent<Renderer>())
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }

        if (this.gameObject.GetComponent<ParticleSystem>())
        {
            this.gameObject.GetComponent<ParticleSystem>().Stop();
        }

        foreach (Transform child in this.gameObject.transform)
        {
            if (child.gameObject.GetComponent<Renderer>())
            {
                child.gameObject.GetComponent<Renderer>().enabled = false;
            }

            if (child.gameObject.GetComponent<ParticleSystem>())
            {
                child.gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
