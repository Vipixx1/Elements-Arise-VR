using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class ParchmentManager : MonoBehaviour
{
    [SerializeField] private GameObject parchment;
    [SerializeField] private GameObject camera;

    [SerializeField] private GameObject left_holder;
    [SerializeField] private GameObject right_holder;
    [SerializeField] private Scroll scroll;

    private bool isRightHandPhoto1 = false;
    private bool isLeftHandPhoto1 = false;

    private bool isRightHandPhoto2 = false;
    private bool isLeftHandPhoto2 = false;

    private bool isRightHandPhoto3 = false;
    private bool isLeftHandPhoto3 = false;

    private bool isRightHandPhoto4 = false;
    private bool isLeftHandPhoto4 = false;

    Vector3 baseParchmentScale = new Vector3(1.5f, 1.5f, 1.5f);

    [SerializeField] private EventReference parchmentOpen;
    private EventInstance parchmentOpenInstance;
    
    [SerializeField] private EventReference parchmentClose;
    private EventInstance parchmentCloseInstance;
    
    [SerializeField] private EventReference parchmentSpawn;

    private float previousScaleFactor;

    private void Start()
    {
        baseParchmentScale = parchment.transform.localScale;

        parchmentOpenInstance = RuntimeManager.CreateInstance(parchmentOpen);
        RuntimeManager.AttachInstanceToGameObject(parchmentOpenInstance, parchment);
        
        parchmentCloseInstance = RuntimeManager.CreateInstance(parchmentClose);
        RuntimeManager.AttachInstanceToGameObject(parchmentCloseInstance, parchment);
    }

    void Update()
    {
        if (!parchment) return;

        if (isRightHandPhoto1 && isLeftHandPhoto1 || isRightHandPhoto2 && isLeftHandPhoto2 || isRightHandPhoto3 && isLeftHandPhoto3 || isRightHandPhoto4 && isLeftHandPhoto4)
        {
            parchment.transform.position = camera.transform.position + camera.transform.forward*0.5f;
            parchment.transform.rotation = camera.transform.rotation * Quaternion.Euler(0, 90, -45);
            
            if (!parchment.activeSelf)
            {
                parchment.SetActive(true);
                RuntimeManager.PlayOneShotAttached(parchmentSpawn, scroll.gameObject);
            }
            
            RuntimeManager.AttachInstanceToGameObject(parchmentCloseInstance, parchment);
            RuntimeManager.AttachInstanceToGameObject(parchmentOpenInstance, parchment);
        }


        ScaleParchment();
        
        if (parchment.transform.localScale.z < 0.5f)
            scroll.EraseScroll();
    }

    private void ScaleParchment()
    {
        float scaleFactor = baseParchmentScale.z / parchment.transform.localScale.z;
        Vector3 newScale = new(left_holder.transform.localScale.x, left_holder.transform.localScale.y, scaleFactor);

        left_holder.transform.localScale = newScale;
        right_holder.transform.localScale = newScale;

        PlayParchmentSound(scaleFactor);
        
        previousScaleFactor = scaleFactor;
    }

    private void PlayParchmentSound(float scaleFactor)
    {
        //scaleFactor va de 1 (parchemin ouvert) à 10 (parchemin fermé)
        bool isOpening = scaleFactor < previousScaleFactor;
        bool isMoving = Math.Abs(scaleFactor - previousScaleFactor) > .05f;
        
        EventInstance currentInstance, otherInstance;
        if (isOpening)
        {
            currentInstance = parchmentOpenInstance;
            otherInstance = parchmentCloseInstance;
        }
        else
        {
            currentInstance = parchmentCloseInstance;
            otherInstance = parchmentOpenInstance;
        }
        
        if (Math.Abs(scaleFactor - 1f) < .01f || Math.Abs(scaleFactor - 10f) < .01f)
        {
            currentInstance.setParameterByName("stop", 1);
            otherInstance.setParameterByName("stop", 1);
            return;
        }

        if (isMoving)
        {
            if (!IsPlaying(currentInstance))
            {
                currentInstance.setParameterByName("stop", 0);
                currentInstance.start();
            }
            
            if (IsPlaying(otherInstance)){
                otherInstance.stop(STOP_MODE.ALLOWFADEOUT);
                otherInstance.setParameterByName("stop", 0);
            }
        }
        else
        {
            otherInstance.stop(STOP_MODE.ALLOWFADEOUT);
            otherInstance.setParameterByName("stop", 0);
            currentInstance.stop(STOP_MODE.ALLOWFADEOUT);
            currentInstance.setParameterByName("stop", 0);
        }
    }
    
    bool IsPlaying(EventInstance instance) {
        instance.getPlaybackState(out PLAYBACK_STATE state);
        return state != PLAYBACK_STATE.STOPPED;
    }
    

    public void SetRightHand1(bool isActivated)
    { 
        isRightHandPhoto1 = isActivated;
    }
    public void SetLeftHand1(bool isActivated)
    {
        isLeftHandPhoto1 = isActivated;
    }
    public void SetRightHand2(bool isActivated)
    {
        isRightHandPhoto2 = isActivated;
    }
    public void SetLeftHand2(bool isActivated)
    {
        isLeftHandPhoto2 = isActivated;
    }
    public void SetRightHand3(bool isActivated)
    {
        isRightHandPhoto3 = isActivated;
    }
    public void SetLeftHand3(bool isActivated)
    {
        isLeftHandPhoto3 = isActivated;
    }
    public void SetRightHand4(bool isActivated)
    {
        isRightHandPhoto4 = isActivated;
    }
    public void SetLeftHand4(bool isActivated)
    {
        isLeftHandPhoto4 = isActivated;
    }
}
