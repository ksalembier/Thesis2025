using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
   [SerializeField]
    private float sceneFadeDuration;
    private SceneFade sceneFade;

    private void Awake()
    {
        sceneFade = GetComponentInChildren<SceneFade>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartSceneFade());
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator StartSceneFade()
    {
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(sceneFadeDuration);
        SceneManager.LoadScene("Vacillate");
    }
}
