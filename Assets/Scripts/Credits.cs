using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayFunction());
    }

    IEnumerator DelayFunction()
    {
        Debug.Log("Before delay");
        yield return new WaitForSeconds(34); // Delay for 5 seconds
        Debug.Log("After delay");
        SceneManager.LoadSceneAsync("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
