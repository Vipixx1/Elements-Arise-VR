using System;
using Oculus.Interaction;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ShootingManager : MonoBehaviour
{
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
    
    private bool _canFuse = true;

    public Vector3? firstWindSpell;
    public Vector3? secondWindSpell;

    [SerializeField] private GameObject fireSpell;
    [SerializeField] private GameObject waterSpell;
    [SerializeField] private GameObject earthSpell;
    [SerializeField] private GameObject windSpell;
    [SerializeField] private GameObject iceSpell;
    [SerializeField] private GameObject plantSpell;
    [SerializeField] private GameObject thunderSpell;
    [SerializeField] private GameObject volcanoSpell;
    [SerializeField] private GameObject steamSpell;
    [SerializeField] private GameObject sandSpell;

    [SerializeField] private EventReference shootEventReference;
    [SerializeField] private EventReference fuseSoundEvent;

    public static event System.Action<string> OnSpellFusion;

    private void Awake()
    {
        OnSpellFusion += PlayFuseSound;
    }

    private void OnDestroy()
    {
        OnSpellFusion -= PlayFuseSound;
    }

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
            rightHandTransform.GetComponentInParent<HandVisual>().SetHandColorBySpell(element);
        }
        else if (handTag == "LeftHand")
        {
            isLeftHandSpellReady = true;
            leftHandElement = element;
            leftHandTransform.GetComponentInParent<HandVisual>().SetHandColorBySpell(element);
        }
    }

    private void AssignSpellToBothHands(string element)
    {
        AssignSpellToHand(element, "LeftHand");
        AssignSpellToHand(element, "RightHand");
    }

    private void ShootSpell(string element, Transform handTransform, int rightOrLeftCoeff)
    {
        GameObject spell;

        switch (element.ToLower())
        {
            case "fire":
                spell = Instantiate(fireSpell, handTransform.position, handTransform.rotation);
                break;
            case "water":
                spell = Instantiate(waterSpell, handTransform.position, handTransform.rotation);
                break;
            case "earth":
                spell = Instantiate(earthSpell, handTransform.position, handTransform.rotation);
                break;
            case "wind":
                spell = Instantiate(windSpell, handTransform.position, handTransform.rotation);
                break;
            case "ice":
                spell = Instantiate(iceSpell, handTransform.position, handTransform.rotation);
                break;
            case "plant":
                spell = Instantiate(plantSpell, handTransform.position, handTransform.rotation);
                break;
            case "thunder":
                spell = Instantiate(thunderSpell, handTransform.position, handTransform.rotation);
                break;
            case "volcano":
                spell = Instantiate(volcanoSpell, handTransform.position, handTransform.rotation);
                break;
            case "steam":
                spell = Instantiate(steamSpell, handTransform.position, handTransform.rotation);
                break;
            case "sand":
                spell = Instantiate(sandSpell, handTransform.position, handTransform.rotation);
                break;
            default:
                //Debug.LogWarning($"Aucun script trouv� pour l'�l�ment {element}. Sort par d�faut utilis�.");
                spell = Instantiate(fireSpell, handTransform.position, handTransform.rotation);
                break;
        }

        // If rightHand, rightOrLeftCoeff = 1, if leftHand, rightOrLeftCoeff = -1
        spell.GetComponent<Rigidbody>().AddForce(rightOrLeftCoeff * (-handTransform.up*0.4f + handTransform.right*0.6f) * 800);
        
        //RuntimeManager.PlayOneShotAttached(shootEventReference, (rightOrLeftCoeff == 1 ? rightHandTransform : leftHandTransform).gameObject);
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
            if (FindAnyObjectByType<WindField>() != null)
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

    public void SetCanFuse(bool canFuse)
    {
        _canFuse = canFuse;
    }
    
    public bool CanFuse()
    {
        return _canFuse;
    }

    public void FuseSpell()
    {
        if (!CanFuse()) return;
        
        List<string> handElements = new List<string> { leftHandElement.ToLower(), rightHandElement.ToLower() };

        if (handElements.Contains("fire") && handElements.Contains("water"))
        {
            OnSpellFusion.Invoke("steam");
            AssignSpellToBothHands("steam");
        }

        else if (handElements.Contains("fire") && handElements.Contains("earth"))
        {
            OnSpellFusion.Invoke("volcano");
            AssignSpellToBothHands("volcano");
        }
        
        else if (handElements.Contains("fire") && handElements.Contains("wind"))
        {
            OnSpellFusion.Invoke("thunder");
            AssignSpellToBothHands("thunder");
        }

        else if (handElements.Contains("earth") && handElements.Contains("water"))
        {
            OnSpellFusion.Invoke("plant");
            AssignSpellToBothHands("plant");
        }

        else if (handElements.Contains("wind") && handElements.Contains("water"))
        {   
            OnSpellFusion.Invoke("ice");
            AssignSpellToBothHands("ice");
        }

        else if (handElements.Contains("earth") && handElements.Contains("wind"))
        {
            OnSpellFusion.Invoke("sand");
            AssignSpellToBothHands("sand");
        }

    }
    
    private void PlayFuseSound(string spell)
    {
        EventInstance collectSoundInstance = RuntimeManager.CreateInstance(fuseSoundEvent);
        collectSoundInstance.setParameterByNameWithLabel("element", spell.ToUpper());
        RuntimeManager.AttachInstanceToGameObject(collectSoundInstance, rightHandTransform);
        collectSoundInstance.start();
        collectSoundInstance.release();
    }
}
