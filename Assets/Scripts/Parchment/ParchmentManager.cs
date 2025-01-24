using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParchmentManager : MonoBehaviour
{
    [SerializeField] private GameObject parchment;
    [SerializeField] private GameObject camera;

    [SerializeField] private GameObject left_holder;
    [SerializeField] private GameObject right_holder;
    [SerializeField] private Scroll scroll;

    private bool isRightHandActivated = false;
    private bool isLeftHandActivated = false;

    void Update()
    {
        if (isRightHandActivated && isLeftHandActivated)
        {
            parchment.SetActive(true);
            parchment.transform.position = camera.transform.position + camera.transform.forward*0.5f;
            parchment.transform.rotation = camera.transform.rotation * Quaternion.Euler(0, 90, -45);
        }

        ScaleScroll();
    }

    private void ScaleScroll()
    {
        Vector3 newScale = scroll.gameObject.transform.localScale;
        newScale.x = (right_holder.transform.localPosition.z - left_holder.transform.localPosition.z) * 0.1f;
        scroll.gameObject.transform.localScale = newScale;
        scroll.gameObject.transform.position = (right_holder.transform.position + left_holder.transform.position) / 2;

        if (scroll.transform.localScale.x < 0.01f)
        {
            scroll.EraseScroll();
        }
    }

    public void SetRightHand(bool isActivated)
    {
        isRightHandActivated = isActivated;
    }

    public void SetLeftHand(bool isActivated)
    {
        isLeftHandActivated = isActivated;
    }
}
