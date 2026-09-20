using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 3.0f; // How long the fade takes in seconds

    private void Start()
    {
        // Automatically start fading in from black when the scene begins
        StartCoroutine(FadeInRoutine());
    }

    // Call this public function from a button, trigger, or death script to leave the scene
    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(FadeOutRoutine(sceneName));
    }

    IEnumerator FadeInRoutine()
    {
        float timer = 0f;
        Color color = fadeImage.color;

        // Ensure the screen starts completely black
        color.a = 1f;
        fadeImage.color = color;

        // Smoothly lower alpha to 0 over time
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null; // Wait for the next frame
        }

        // Disable the image raycast target so it doesn't block player clicks
        fadeImage.raycastTarget = false;
    }

    IEnumerator FadeOutRoutine(string sceneName)
    {
        // Re-enable raycast target so player can't click anything while fading out
        fadeImage.raycastTarget = true;

        float timer = 0f;
        Color color = fadeImage.color;

        // Smoothly raise alpha to 1 over time
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}