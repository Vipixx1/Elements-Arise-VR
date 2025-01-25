using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParchmentManager : MonoBehaviour
{
    [SerializeField] private GameObject parchment;
    [SerializeField] private GameObject camera;

    [SerializeField] private GameObject left_holder;
    [SerializeField] private GameObject right_holder;
    [SerializeField] private Scroll scroll;

    private bool isRightHandPhoto = false;
    private bool isLeftHandPhoto = false;

    Vector3 baseParchmentScale = new Vector3(1.5f, 1.5f, 1.5f);


    private void Start()
    {
        baseParchmentScale = parchment.transform.localScale;
    }
    void Update()
    {
        if (!parchment) return;

        if (isRightHandPhoto && isLeftHandPhoto)
        {
            parchment.SetActive(true);
            parchment.transform.position = camera.transform.position + camera.transform.forward*0.5f;
            parchment.transform.rotation = camera.transform.rotation * Quaternion.Euler(0, 90, -45);
        }

        ScaleParchment();
        
        if (parchment.transform.localScale.z < 0.5f)
            scroll.EraseScroll();
    }

    private void ScaleParchment()
    {
        left_holder.transform.localScale = new Vector3(left_holder.transform.localScale.x, left_holder.transform.localScale.y, baseParchmentScale.z / parchment.transform.localScale.z);
        right_holder.transform.localScale = new Vector3(right_holder.transform.localScale.x, right_holder.transform.localScale.y, baseParchmentScale.z / parchment.transform.localScale.z);
    }

    public void SetRightHand(bool isActivated)
    {
        isRightHandPhoto = isActivated;
    }

    public void SetLeftHand(bool isActivated)
    {
        isLeftHandPhoto = isActivated;
    }
}
