using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;

    private bool isRightHandSpellReady = false;
    private bool isLeftHandSpellReady = false;

    private string rightHandElement = null;
    private string leftHandElement = null;

    private bool isRightHandClosed = false;
    private bool isLeftHandClosed = false;

    private void OnEnable()
    {
        ScrollSpellResult.OnSpellReady += AssignSpellToHand;
    }

    private void OnDisable()
    {
        ScrollSpellResult.OnSpellReady -= AssignSpellToHand;
    }

    public void SetRightHandClosed(bool isClosed)
    {
        if (isRightHandClosed && !isClosed && isRightHandSpellReady)
        {
            ShootSpell(rightHandElement, rightHandTransform);
            isRightHandSpellReady = false;
        }
        isRightHandClosed = isClosed;
    }

    public void SetLeftHandClosed(bool isClosed)
    {
        if (isLeftHandClosed && !isClosed && isLeftHandSpellReady)
        {
            ShootSpell(leftHandElement, leftHandTransform);
            isLeftHandSpellReady = false;
        }
        isLeftHandClosed = isClosed;
    }

    private void AssignSpellToHand(string element, string handTag)
    {
        if (handTag == "RightHand")
        {
            isRightHandSpellReady = true;
            rightHandElement = element;
        }
        else if (handTag == "LeftHand")
        {
            isLeftHandSpellReady = true;
            leftHandElement = element;
        }
    }

    private void ShootSpell(string element, Transform handTransform)
    {
        GameObject spell = Instantiate(spellPrefab, handTransform.position, handTransform.rotation);

        switch (element.ToLower())
        {
            case "fire":
                spell.AddComponent<FireSpell>();
                spell.GetComponent<Renderer>().material.color = Color.red;
                break;
            case "water":
                spell.AddComponent<WaterSpell>();
                spell.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case "wind":
                spell.AddComponent<WindSpell>();
                spell.GetComponent<Renderer>().material.color = Color.green;
                break;
            case "earth":
                spell.AddComponent<EarthSpell>();
                spell.GetComponent<Renderer>().material.color = Color.gray;
                break;
            default:
                Debug.LogWarning($"Aucun script trouvé pour l'élément {element}. Sort par défaut utilisé.");
                break;
        }

        spell.GetComponent<Rigidbody>().AddForce((-handTransform.up*0.4f + handTransform.right*0.6f) * 900);
    }
}
