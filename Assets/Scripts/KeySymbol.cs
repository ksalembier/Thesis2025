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

    public void SetSymbolActive()
    {
        this.gameObject.SetActive(true);
    }

    public void SetSymbolInactive()
    {
        this.gameObject.SetActive(false);
    }
}
