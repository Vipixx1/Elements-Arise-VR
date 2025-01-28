using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class PlayerAudio : MonoBehaviour
    {

        [SerializeField] private EventReference tpSound;

        public void PlayTpSound()
        {
            RuntimeManager.PlayOneShot(tpSound);
        }
    }
}
