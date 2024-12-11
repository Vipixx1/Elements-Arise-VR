using UnityEngine;

public class ScrollSpellResult : MonoBehaviour
{
    public static event System.Action<string, string> OnSpellReady;
    private string currentSpellElement;
    [SerializeField] private Scroll scroll;

    public void OnSelect()
    {
        OnSpellReady?.Invoke(currentSpellElement, "RightHand");
        scroll.EraseScroll();
        this.gameObject.GetComponent<Renderer>().enabled = false;
        //this.gameObject.SetActive(false);
    }

    public void OnUnselect()
    {
        this.gameObject.SetActive(false);
    }

    public void SetCurrentSpellElement(string element)
    {
        currentSpellElement = element;
    }
}
