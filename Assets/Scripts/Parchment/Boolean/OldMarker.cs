using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class OldMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 25;
    [SerializeField] private float _tipHeight = 0.03f;
    [SerializeField] private Color _color = Color.blue;

    private Color[] _colors;

    private RaycastHit _touch;
    private Scroll scroll;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    public UnityEvent<int, int, int, int> writingUnityEvent;

    void Start()
    {
        _colors = Enumerable.Repeat(_color, _penSize * _penSize).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        _colors = Enumerable.Repeat(_color, _penSize * _penSize).ToArray();

        bool raycastHit;
        raycastHit = _tip 
            ? Physics.Raycast(_tip.position, _tip.transform.right, out _touch, _tipHeight)
            : Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _touch) && Input.GetMouseButton(0);

        if (raycastHit)
        {
            if (_touch.transform.CompareTag("ScrollA"))
            {
                if (scroll == null)
                {
                    scroll = _touch.transform.GetComponent<Scroll>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * scroll.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * scroll.textureSize.y - (_penSize / 2));

                if (y < 0 || y > scroll.textureSize.y || x < 0 || x > scroll.textureSize.x) return;
                
                scroll.texture.SetPixels(x, y, _penSize, _penSize, _colors);
                writingUnityEvent.Invoke(x, y, _penSize, _penSize);

                if (_touchedLastFrame)
                {

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        scroll.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                        writingUnityEvent.Invoke(lerpX, lerpY, _penSize, _penSize);
                    }

                    transform.rotation = _lastTouchRot;
                }
                
                scroll.texture.Apply();

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        scroll = null;
        _touchedLastFrame = false;
    }
}