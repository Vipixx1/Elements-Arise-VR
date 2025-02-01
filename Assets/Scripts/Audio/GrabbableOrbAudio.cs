using System;
using System.Reflection;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio
{
    [RequireComponent(typeof(SphereCollider))]
    public class GrabbableOrbAudio : MonoBehaviour
    {
        [SerializeField] private EventReference orbSoundEvent;
        [SerializeField] private string[] acceptedTags;
        private EventInstance orbSoundInstance;

        private Collider _currentCollider = null;

        private SphereCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _currentCollider = null;
        }

        private void OnEnable()
        {
            _currentCollider = null;
            if (orbSoundInstance.hasHandle()) orbSoundInstance.release();
            orbSoundInstance = RuntimeManager.CreateInstance(orbSoundEvent);
            RuntimeManager.AttachInstanceToGameObject(orbSoundInstance, transform.gameObject);
        }

        private void OnDisable()
        {
            orbSoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
            orbSoundInstance.release();
            _currentCollider = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_currentCollider) return;
            foreach (string acceptedTag in acceptedTags)
            {
                if (!other.CompareTag(acceptedTag))
                    continue;
                Debug.Log("ENTER");
                orbSoundInstance.start();
                _currentCollider = other;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other != _currentCollider) return;
            orbSoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
            _currentCollider = null;
            Debug.Log("EXIT");
        }

        private void Update()
        {
            if (!_currentCollider) return;
            
            //Debug.Log(Vector3.Dot(_currentCollider.transform.position, transform.position));
            orbSoundInstance.setParameterByName("distance", (Mathf.Cos(_currentCollider.transform.rotation.z) + 1f) / 2f);
        }

        public void OnGrabb()
        {
            orbSoundInstance.setParameterByName("end", 1);
            orbSoundInstance.release();
        }

    }
}
