using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    // public Stage currentStage, lastStage;

    public AnimationCurve shakeCurve;
    public float smoothTime;

    public float traumaMult = 1f;
    private float trauma;
    public float rotationShake;
    public float positionShake;

    private bool shaking = false;

    private Vector3 vel;

    public Vector2 kickBackDir;

    private Coroutine lastFocusUp;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Focus();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.Translate(-180, 0, 0);
            // Focus();
        }
    }

    // public static void SetStage(Stage newStage)
    // {
    //     if (newStage.stageData == instance.currentStage.stageData)
    //         return;

    //     instance.lastStage = instance.currentStage;

    //     instance.currentStage = newStage;

    //     instance.Focus();
    // }

    // private void Focus()
    // {
    //     if (currentStage == null)
    //         return;

    //     kickBackDir = Vector2.zero;

    //     transform.rotation = Quaternion.identity;

    //     shaking = false;
    //     trauma = 0f;

    //     if (lastFocusUp != null)
    //         StopCoroutine(lastFocusUp);

    //     lastFocusUp = StartCoroutine(FocusUp());
    // }

    // IEnumerator FocusUp()
    // {
    //     Vector3 goal = (Vector2)currentStage.stageData.startingPoint.position;

    //     //if (instance.lastStage != null)
    //         //instance.lastStage.entrance.isTrigger = false;

    //     goal.z = -10;

    //     while (Vector2.Distance(transform.position, goal) > 0.5f)
    //     {
    //         trauma = 0;

    //         transform.position = Vector3.SmoothDamp(transform.position, goal, ref vel, smoothTime);

    //         yield return new WaitForEndOfFrame();
    //     }

    //     if (instance.lastStage != null)
    //         StartCoroutine(EntranceEnabledReset());
    //     yield break;
    // }

    // IEnumerator EntranceEnabledReset()
    // {
    //     instance.lastStage.entrance.enabled = false;
    //     yield return new WaitForEndOfFrame();
    //     instance.lastStage.entrance.enabled = true;
    //     //instance.lastStage.entrance.isTrigger = true;
    //     yield break;
    // }

    public static void Shake(float addTrauma)
    {
        instance.trauma = Mathf.Clamp01(instance.trauma + addTrauma);

        if (!instance.shaking)
            instance.StartCoroutine(instance.DoShake());
    }

    public static float GetRandom(int seed, float time)
    {
        return Mathf.PerlinNoise(seed, time);
    }

    public static float GetRandomNegToPos(int seed, float time)
    {
        return (GetRandom(seed, time) - 0.5f) * 2f;
    }

    IEnumerator DoShake()
    {
        Vector3 defaultRot = transform.rotation.eulerAngles;
        Vector3 defaultPos = transform.position;
        int seed = Random.Range(0, 999999);
        float timeTick = 0f;

        float traumaSqr = shakeCurve.Evaluate(trauma);

        shaking = true;

        for (; trauma > 0; trauma -= Time.smoothDeltaTime, timeTick += Time.smoothDeltaTime * (traumaSqr) * traumaMult)
        {
            Vector3 rotation = defaultRot;
            Vector3 position = defaultPos;

            traumaSqr = shakeCurve.Evaluate(trauma);

            rotation.z = rotationShake * traumaSqr * GetRandomNegToPos(seed, timeTick);

            position.x += positionShake * traumaSqr * GetRandom(seed + 1, timeTick) * -kickBackDir.x;
            position.y += positionShake * traumaSqr * GetRandom(seed + 2, timeTick) * -kickBackDir.y;

            transform.rotation = Quaternion.Euler(rotation);
            transform.position = position;

            yield return new WaitForEndOfFrame();
        }

        kickBackDir = Vector2.zero;

        transform.rotation = Quaternion.Euler(defaultRot);
        transform.position = defaultPos;

        shaking = false;
        trauma = 0f;

        yield break;
    }
}
