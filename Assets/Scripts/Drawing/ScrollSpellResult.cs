using UnityEngine;

#nullable enable

public class ScrollSpellResult : MonoBehaviour
{
    public static event System.Action<string, string> OnSpellReady;
    [SerializeField] private string currentSpellElement = "fire";
    [SerializeField] private Scroll? scroll;
    private Vector3 spawnCoords;

    public void Start()
    {
        spawnCoords = this.gameObject.transform.position;
    }

    public void OnSelect()
    {
        OnSpellReady?.Invoke(currentSpellElement, "RightHand");
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
}
