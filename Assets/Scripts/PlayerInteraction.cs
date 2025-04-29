using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject rotation;

    private GameObject villageStartMarker;
    private GameObject cityStartMarker;

    public GameObject keySymbol;
    public int currentKeyIndex;

    private bool isVillage;

    private bool isHoldingVillageKey;
    private bool isHoldingCityKey;

    public GameObject fadeCanvas;
    private CoreLoopController sceneController;

    private void Start()
    {
        villageStartMarker = GameObject.Find("Village Start Marker");
        cityStartMarker = GameObject.Find("City Start Marker");
        
        isHoldingVillageKey = false;
        isHoldingCityKey = false;

        sceneController = fadeCanvas.GetComponent<CoreLoopController>();
    }

    private void MakeIncorporeal()
    {
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Collider2D>().enabled = false;
    }

    private void MakeCorporeal()
    {
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Village Portal" && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(RotateCoroutine(180f, true)); // rotates player to city 
        }
        if (collision.gameObject.tag == "City Portal" && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(RotateCoroutine(0f, false)); // rotates player to village
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // obstacle collisions
        if (collision.gameObject.tag == "Village Obstacle") // rotates player to city
        {
            //StartCoroutine(RotateCoroutine(180f, true));
            StartCoroutine(MoveToStartCoroutine(transform.position, new Vector2(villageStartMarker.transform.position.x, 3.0f), 1.0f));
        }
        if (collision.gameObject.tag == "City Obstacle") // rotates player to village
        {
            //StartCoroutine(RotateCoroutine(0f, false));
            StartCoroutine(MoveToStartCoroutine(transform.position, new Vector2(cityStartMarker.transform.position.x, 3.0f), 1.0f));
        }

        // key collisions 
        if (collision.gameObject.tag == "Village Key" || collision.gameObject.tag == "City Key" ) // collected in city, used in village
        {
            int index = collision.gameObject.GetComponent<Key>().GetIndex();
            collision.gameObject.SetActive(false);
            keySymbol.GetComponent<KeySymbol>().ChangeSymbol(index);
            keySymbol.SetActive(true);
            currentKeyIndex = index;

            if (collision.gameObject.tag == "Village Key") { isHoldingVillageKey = true; }
            else { isHoldingCityKey = true ; }
        }

        // door colisions
        // if (collision.gameObject.tag == "Village Door" && isHoldingVillageKey)
        // {
        //     collision.gameObject.SetActive(false);
        //     keySymbol.SetActive(false);
        //     isHoldingVillageKey = false;
        // }
        // if (collision.gameObject.tag == "City Door" && isHoldingCityKey)
        // {
        //     collision.gameObject.SetActive(false);
        //     keySymbol.SetActive(false);
        //     isHoldingCityKey = false;
        // }

        if (collision.gameObject.tag == "Village Door" || collision.gameObject.tag == "City Door")
        {
            Door door =  collision.gameObject.GetComponentInParent<Door>();
            door.SetRequestActive(true);
        }
        if ((collision.gameObject.tag == "Village Door" || collision.gameObject.tag == "City Door") && (isHoldingVillageKey || isHoldingCityKey))
        {
            Door door = collision.gameObject.GetComponentInParent<Door>();
            if (door.GetIndex() == currentKeyIndex)
            {
                door.SetRequestActive(false);
                door.SetColliderActive(false);
                keySymbol.SetActive(false);
                isHoldingVillageKey = false;
                isHoldingCityKey = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Game End")
        {
            sceneController.GameOver();
        }
    }

    private IEnumerator RotateCoroutine(float newZValue, bool isVillage)
    {
        MakeIncorporeal();
        rotation.GetComponent<Rotation>().Rotate(newZValue);
        yield return new WaitForSeconds(2.5f);
        float newStartX;
        if (isVillage) { newStartX = cityStartMarker.transform.position.x; }
        else { newStartX = villageStartMarker.transform.position.x; }
        StartCoroutine(MoveToStartCoroutine(transform.position, new Vector2(newStartX, 3.0f), 1.0f));
        yield return new WaitForSeconds(2.0f);
        MakeCorporeal();
    }

    private IEnumerator MoveToStartCoroutine(Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        MakeIncorporeal();
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / duration;
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedPercentage);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        MakeCorporeal();
    }

}