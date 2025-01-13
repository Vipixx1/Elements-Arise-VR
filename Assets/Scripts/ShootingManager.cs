using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private List<Material> spellMaterials;

    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;

    private bool isRightHandSpellReady = false;
    private bool isLeftHandSpellReady = false;

    private string rightHandElement = null;
    private string leftHandElement = null;
    public string RightHandElement { get => rightHandElement; set => rightHandElement = value; }
    public string LeftHandElement { get => leftHandElement; set => leftHandElement = value; }

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
                spell.GetComponent<Renderer>().material = spellMaterials[0];
                break;
            case "water":
                spell.AddComponent<WaterSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[1];
                break;
            case "earth":
                spell.AddComponent<EarthSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[2];
                break;
            case "wind":
                spell.AddComponent<WindSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[3];
                break;
            case "ice":
                spell.AddComponent<IceSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[4];
                break;
            case "plant":
                spell.AddComponent<PlantSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[5];
                break;
            case "thunder":
                spell.AddComponent<ThunderSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[6];
                break;
            case "volcano":
                spell.AddComponent<VolcanoSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[7];
                break;
            case "steam":
                spell.AddComponent<SteamSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[8];
                break;
            case "sand":
                spell.AddComponent<SandSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[9];
                break;
            
            default:
                Debug.LogWarning($"Aucun script trouvé pour l'élément {element}. Sort par défaut utilisé.");
                spell.AddComponent<FireSpell>();
                spell.GetComponent<Renderer>().material = spellMaterials[0];
                break;
        }

        spell.GetComponent<Rigidbody>().AddForce((-handTransform.up*0.4f + handTransform.right*0.6f) * 800);
    }
}
