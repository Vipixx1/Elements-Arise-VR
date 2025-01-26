using UnityEngine;
using UnityEngine.SceneManagement;

#nullable enable

public class ScrollSpellResult : MonoBehaviour
{
    public static event System.Action<string, string> OnSpellReady;
    [SerializeField] private string currentSpellElement = "fire";
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
        OnSpellReady?.Invoke(currentSpellElement, currentHandTag);
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
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            currentHandTag = "";
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
}
