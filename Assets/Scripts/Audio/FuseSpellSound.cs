using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FuseSpellSound : MonoBehaviour
    {
        [SerializeField] private EventReference fuseSoundEvent;
    
        private void PlayFuseSound(string spell)
        {
            EventInstance collectSoundInstance = RuntimeManager.CreateInstance(fuseSoundEvent);
            collectSoundInstance.setParameterByNameWithLabel("element", spell.ToUpper());
            RuntimeManager.AttachInstanceToGameObject(collectSoundInstance, gameObject);
            collectSoundInstance.start();
            collectSoundInstance.release();
        }
    }
}
