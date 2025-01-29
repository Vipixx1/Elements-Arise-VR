using Oculus.Interaction;
using Oculus.Interaction.Locomotion;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Locomotion
{
    public class LocomotionTeleportInteractorEventWrapper : MonoBehaviour
    {
        [SerializeField] private TeleportInteractor _teleporter;
   
        [SerializeField]
        private UnityEvent _whenTeleporting;
   
        public UnityEvent WhenTeleporting => _whenTeleporting;
   
        protected bool _started;
   
        protected virtual void Start()
        {
            this.BeginStart(ref _started);
            this.AssertField(_teleporter, nameof(_teleporter));
            this.EndStart(ref _started);
        }
   
        protected virtual void OnEnable()
        {
            if (_started)
            {
                _teleporter.WhenLocomotionPerformed += HandleTeleported;
            }
        }
   
        protected virtual void OnDisable()
        {
            if (_started)
            {
                _teleporter.WhenLocomotionPerformed -= HandleTeleported;
            }
        }
   
        private void HandleTeleported(LocomotionEvent locomotionEvent)
        {
            if(locomotionEvent.Translation != LocomotionEvent.TranslationType.None)
                _whenTeleporting.Invoke();
        }
        
    }
}
