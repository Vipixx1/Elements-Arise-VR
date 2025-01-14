using UnityEngine;

#nullable enable

public class ScrollSpellResult : MonoBehaviour
{
    public static event System.Action<string, string> OnSpellReady;
    [SerializeField] private string currentSpellElement = "fire";
    [SerializeField] private Scroll? scroll;
    private Vector3 spawnCoords;

    private string currentHandTag = "";

    public void Start()
    {
        spawnCoords = this.gameObject.transform.position;
    }

    public void OnSelect()
    {
        if (currentHandTag == "")
        {
            return;
        }
        OnSpellReady?.Invoke(currentSpellElement, currentHandTag);
        scroll?.EraseScroll();
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void OnUnselect()
    {
        this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        this.gameObject.transform.position = spawnCoords;
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.gameObject.SetActive(true);
    }

    public void SetCurrentSpellElement(string element)
    {
        currentSpellElement = element;
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
}
