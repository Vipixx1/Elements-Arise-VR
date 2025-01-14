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

    public string rightHandElement = null;
    public string leftHandElement = null;

    private bool isRightHandClosed = false;
    private bool isLeftHandClosed = false;

    [SerializeField]
    private GameObject windFieldPrefab;

    public Vector3? firstWindSpell;
    public Vector3? secondWindSpell;

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
            ShootSpell(rightHandElement, rightHandTransform, 1);
            //isRightHandSpellReady = false;
        }
        isRightHandClosed = isClosed;
    }

    public void SetLeftHandClosed(bool isClosed)
    {
        if (isLeftHandClosed && !isClosed && isLeftHandSpellReady)
        {
            ShootSpell(leftHandElement, leftHandTransform, -1);
            //isLeftHandSpellReady = false;
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

    private void ShootSpell(string element, Transform handTransform, int rightOrLeftCoeff)
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

        // If rightHand, rightOrLeftCoeff = 1, if leftHand, rightOrLeftCoeff = -1
        spell.GetComponent<Rigidbody>().AddForce(rightOrLeftCoeff * (-handTransform.up*0.4f + handTransform.right*0.6f) * 800);
    }


    public void WindFieldPlacement(Vector3 spellPosition)
    {

        bool windFieldReady = false;

        if (firstWindSpell == null)
        {
            firstWindSpell = spellPosition;
            Debug.Log("Placed first wind spot");
            
        }
        else if (firstWindSpell != null && secondWindSpell == null)
        {
            secondWindSpell = spellPosition;
            windFieldReady = true;
            Destroy(FindAnyObjectByType<WindField>().gameObject);
            Debug.Log("Placed second wind spot");
        }

        else
        {
            firstWindSpell = spellPosition;
            secondWindSpell = null;
            Debug.Log("Reset wind spot positions");
        }

        if (windFieldReady) {

            Instantiate(windFieldPrefab);
            Debug.Log("Placed wind field");
        }

    }

    public void FuseSpell()
    {

        List<string> handElements = new List<string> { leftHandElement.ToLower(), rightHandElement.ToLower() };

        if (handElements.Contains("fire") && handElements.Contains("water"))
        {
            leftHandElement = "steam";
            rightHandElement = "steam";
        }

        else if (handElements.Contains("fire") && handElements.Contains("earth"))
        {
            leftHandElement = "volcano";
            rightHandElement = "volcano";
        }


        else if (handElements.Contains("fire") && handElements.Contains("wind"))
        {
            leftHandElement = "thunder";
            rightHandElement = "thunder";
        }

        else if (handElements.Contains("earth") && handElements.Contains("water"))
        {
            leftHandElement = "plant";
            rightHandElement = "plant";
        }

        else if (handElements.Contains("wind") && handElements.Contains("water"))
        {
            leftHandElement = "ice";
            rightHandElement = "ice";
        }

        else if (handElements.Contains("earth") && handElements.Contains("wind"))
        {
            leftHandElement = "sand";
            rightHandElement = "sand";
        }

    }
}
