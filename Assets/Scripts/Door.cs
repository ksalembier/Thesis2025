using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int index;
    public GameObject keyRequest;
    public GameObject barrier;

    public int GetIndex()
    {
        return index;
    }

    public void SetRequestActive(bool isActive)
    {
        keyRequest.SetActive(isActive);
    }

    public void SetColliderActive(bool isActive)
    {
        barrier.SetActive(isActive);
    }
}
