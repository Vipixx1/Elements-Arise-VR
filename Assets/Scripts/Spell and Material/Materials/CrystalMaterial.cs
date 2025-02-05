using System;
using Magic.Materials;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Material = Magic.Materials.Material;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class CrystalMaterial : Material
{
    public bool IsActivated => timeRemaining > 0;

    [SerializeField] private float maxDuration = 15f;
    [SerializeField] private SpellType spellType;

    [SerializeField] private EventReference cristalSoundEvent;
    private EventInstance cristalSound;

    private Renderer crystalRenderer;
    private MaterialPropertyBlock propertyBlock;

    private Color baseColor;
    private Color spellColor;

    private float timeRemaining = 0f;

    private static readonly float BlinkDuration = 1.0f;
    private static readonly float BlinkInterval = 0.1f;

    private void Start()
    {
        crystalRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();

        cristalSound = RuntimeManager.CreateInstance(cristalSoundEvent);
        RuntimeManager.AttachInstanceToGameObject(cristalSound, gameObject);
        
        // Initialize base color from the material
        crystalRenderer.GetPropertyBlock(propertyBlock);
        baseColor = crystalRenderer.sharedMaterial.color;
        ResetCrystal();
    }

    private void Update()
    {
        if (IsActivated)
        {
            timeRemaining -= Time.deltaTime;
            //Debug.Log(timeRemaining);

            if (timeRemaining <= 0)
            {
                ResetCrystal();
            }
        }
    }

    public override void OnEarth(ObjectData data, float[] args = null) => ActivateCrystal(new Color32(139, 69, 19, 200), SpellType.EARTH);

    public override void OnFire(ObjectData data, float[] args = null) => ActivateCrystal(Color.red, SpellType.FIRE);

    public override void OnVolcano(ObjectData data, float[] args = null) => ActivateCrystal(new Color32(60, 0, 0, 255), SpellType.VOLCANO);

    public override void OnIce(ObjectData data, float[] args = null) => ActivateCrystal(Color.cyan, SpellType.ICE);

    public override void OnPlant(ObjectData data, float[] args = null) => ActivateCrystal(Color.green, SpellType.PLANT);

    public override void OnSand(ObjectData data, float[] args = null) => ActivateCrystal(new Color32(244, 164, 96, 200), SpellType.SAND);

    public override void OnSteam(ObjectData data, float[] args = null) => ActivateCrystal(new Color32(130, 130, 130, 200), SpellType.STEAM);

    public override void OnThunder(ObjectData data, float[] args = null) => ActivateCrystal(Color.yellow, SpellType.THUNDER);

    public override void OnWater(ObjectData data, float[] args = null) => ActivateCrystal(Color.blue, SpellType.WATER);

    public override void OnWind(ObjectData data, float[] args = null) => ActivateCrystal(new Color32(0, 255, 130, 200), SpellType.WIND);

    private void ActivateCrystal(Color color, SpellType type)
    {
        spellColor = color;
        timeRemaining = 0f;

        StopAllCoroutines(); // Stop any ongoing effects to avoid conflicts
        cristalSound.stop(STOP_MODE.ALLOWFADEOUT);
        cristalSound.setParameterByName("cristal_loop", 0);
        StartCoroutine(BlinkEffect());
        if (spellType == type)
        {
            cristalSound.setParameterByName("cristal_loop", 1);
            timeRemaining = maxDuration;
            StartCoroutine(GlowEffect());
        }
    }

    private IEnumerator BlinkEffect()
    {
        cristalSound.start();
        float elapsed = 0f;

        while (elapsed < BlinkDuration)
        {
            SetCrystalColor(spellColor);
            yield return new WaitForSeconds(BlinkInterval);
            SetCrystalColor(baseColor);
            yield return new WaitForSeconds(BlinkInterval);

            elapsed += BlinkInterval * 2;
        }
    }

    private IEnumerator GlowEffect()
    {
        timeRemaining = maxDuration;
        yield return new WaitForSeconds(BlinkDuration);

        // Glow for the remaining duration
        SetCrystalColor(spellColor);
        yield return new WaitForSeconds(maxDuration - BlinkDuration);
    }

    private void ResetCrystal()
    {
        SetCrystalColor(baseColor);
        crystalRenderer.SetPropertyBlock(propertyBlock);
        cristalSound.setParameterByName("cristal_loop", 0);
    }

    private void SetCrystalColor(Color color)
    {
        crystalRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_Color", color);
        crystalRenderer.SetPropertyBlock(propertyBlock);
    }

    private void OnDestroy()
    {
        cristalSound.stop(STOP_MODE.ALLOWFADEOUT);
        cristalSound.release();
    }
}
