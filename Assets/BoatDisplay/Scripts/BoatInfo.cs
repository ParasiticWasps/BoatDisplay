using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInfo : MonoBehaviour
{
    public string boatName;

    [SerializeField] private float smallSize = 20.0f;

    [SerializeField] private float largeSize = 45.0f;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetLargeScale()
    {
        SetScale(largeSize);
    }

    public void SetSmallScale()
    {
        SetScale(smallSize);
    }

    private void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
