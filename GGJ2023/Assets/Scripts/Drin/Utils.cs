using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour {

    public float dieTime = 4f;

    private static Utils instance;

    float amount = 0;

    float originalTimeScale = 1;

    void Awake()
    {
        instance = this;
    }

    public static void Freeze(float newamount, float multiplier = 1f)
    {
        instance.StopAllCoroutines();
        instance.amount = newamount * multiplier;
        instance.StartCoroutine(instance.DoFreeze());
    }

    IEnumerator DoFreeze()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        while (amount > 0)
        {
            amount -= Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = originalTimeScale;
    }

    public static void RestartGame()
    {
        instance.StartCoroutine(instance.RestartIn(instance.dieTime));
    }

    IEnumerator RestartIn(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}