using System.Linq;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using STOP_MODE = FMODUnity.STOP_MODE;

namespace Parchment.BoolDistance
{
    public class NewMarker : MonoBehaviour
    {
        [SerializeField] private Transform rightFinger;
        [SerializeField] private Transform leftFinger; 
        [SerializeField] private int penSize = 25;
        [SerializeField] private float tipHeight = 0.03f;
        [SerializeField] public Color color = Color.blue;

        [SerializeField] private Scroll scroll;

        private Color[] colors;
        private RaycastHit touch;
        private Vector2 touchPos, lastTouchPos;
        private bool touchedLastFrame;
        private Quaternion lastTouchRot;
        
        public UnityEvent<int, int, int, int> writingUnityEvent;

        [SerializeField] private EventReference markerSoundEvent;
        private EventInstance markerSoundEventInstance;

        void Start()
        {
            colors = Enumerable.Repeat(color, penSize * penSize).ToArray();
            SetupSound();
        }

        private void SetupSound()
        {
            markerSoundEventInstance = RuntimeManager.CreateInstance(markerSoundEvent);
            RuntimeManager.AttachInstanceToGameObject(markerSoundEventInstance, scroll.gameObject);
            markerSoundEventInstance.setParameterByName("changing_direction", 0);
            markerSoundEventInstance.setParameterByName("end", 0); 
        }

        private void OnDestroy()
        {
            markerSoundEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            markerSoundEventInstance.release();
        }

        void Update()
        {
            Draw();
        }

        private void Draw()
        {
            colors = Enumerable.Repeat(color, penSize * penSize).ToArray();
        
            bool raycastHit;
            raycastHit = rightFinger || leftFinger
                ? Physics.Raycast(rightFinger.position, rightFinger.transform.right, out touch, tipHeight)
                  || Physics.Raycast(leftFinger.position, - leftFinger.transform.right, out touch, tipHeight)
                : Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out touch) && Input.GetMouseButton(0);

            if (raycastHit)
            {
                if (touch.transform.CompareTag("NewScroll"))
                {
                    if (scroll == null)
                    {
                        scroll = touch.transform.GetComponent<Scroll>();
                    }
                
                    touchPos = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                    var x = (int)(touchPos.x * scroll.textureSize.x - (penSize / 2));
                    var y = (int)(touchPos.y * scroll.textureSize.y - (penSize / 2));

                    if (y < 0 || y > scroll.textureSize.y || x < 0 || x > scroll.textureSize.x) return;

                    scroll.DrawPoint(x, y, penSize, penSize, colors);
                    writingUnityEvent.Invoke(x, y, penSize, penSize);

                    if (!touchedLastFrame)
                    {
                        scroll.StartStroke();
                        markerSoundEventInstance.setParameterByName("end", 0);
                        markerSoundEventInstance.start();
                    }

                    if (touchedLastFrame)
                    {
                        for (float f = 0.01f; f < 1.00f; f += 0.01f)
                        {
                            var lerpX = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                            var lerpY = (int)Mathf.Lerp(lastTouchPos.y, y, f);

                            scroll.DrawPoint(lerpX, lerpY, penSize, penSize, colors);
                            writingUnityEvent.Invoke(lerpX, lerpY, penSize, penSize);
                        }

                        transform.rotation = lastTouchRot;
                    
                        float changing_direction = Vector2.Angle(lastTouchPos.normalized, new Vector2(x, y).normalized);
                        markerSoundEventInstance.setParameterByName("changing_direction", changing_direction > 3f ? 1 : 0);
                    
                        float speed = Vector2.Distance(lastTouchPos, new Vector2(x, y));
                        markerSoundEventInstance.setParameterByName("speed", speed / 100f);
                    }

                    scroll.texture.Apply();

                    lastTouchPos = new Vector2(x, y);
                    lastTouchRot = transform.rotation;
                    touchedLastFrame = true;

                    return;
                }
            }
        
            if (touchedLastFrame)
            {
                scroll.FinalizeStroke();
                markerSoundEventInstance.setParameterByName("end", 1);
            }

            scroll = null;
            touchedLastFrame = false;
        }
    }
}
