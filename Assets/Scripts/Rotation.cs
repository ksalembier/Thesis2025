using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    float reference;
    public float rotationDuration = 2.0f;

    private static GameObject village;
    private static GameObject city;
    private static GameObject divide;
    private static GameObject player;

    private float targetRotation;

    private void Start()
    {
        village = GameObject.Find("Village");
        city = GameObject.Find("City");
        divide = GameObject.Find("Divide");
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        float rotate = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetRotation, ref reference, 0.5f);
        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }

    private void SetPosition()
    {
        this.transform.position = new Vector2(player.transform.position.x, 0f);
    }

    private void AddBackgroundAsChildren()
    {
        village.transform.parent = this.transform;
        city.transform.parent = this.transform;
        divide.transform.parent = this.transform;
    }

    private void RemoveBackgroundAsChildren()
    {
        this.transform.DetachChildren();
    }

    private IEnumerator RotateCoroutine(float newZValue)
    {
        SetPosition();
        AddBackgroundAsChildren();
        targetRotation = newZValue;
        yield return new WaitForSeconds(2.5f);
        transform.rotation = Quaternion.Euler(0, 0, newZValue);
        RemoveBackgroundAsChildren();
    }

    public void Rotate(float newZValue)
    {
        StartCoroutine(RotateCoroutine(newZValue));
    }
}
