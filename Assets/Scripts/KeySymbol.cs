using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySymbol : MonoBehaviour
{
    public Sprite[] keys;
    public int currentKeyIndex;

    public void ChangeSymbol(int newKeyIndex)
    {
        GetComponent<SpriteRenderer>().sprite = keys[newKeyIndex];
        currentKeyIndex = newKeyIndex;
    }

    public int GetIndex()
    {
        return currentKeyIndex;
    }

    public void SetSymbolActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }

}
