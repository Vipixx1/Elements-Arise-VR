using UnityEngine;

public class ScrollSpellResult : MonoBehaviour
{
    public static event System.Action<string, string> OnSpellReady;
    private string currentSpellElement;
    [SerializeField] private Scroll scroll;

    public void OnGrab()
    {
        OnSpellReady?.Invoke(currentSpellElement, "RightHand");
        this.gameObject.SetActive(false);
        scroll.EraseScroll();
    }

    public void SetCurrentSpellElement(string element)
    {
        currentSpellElement = element;
    }
}
