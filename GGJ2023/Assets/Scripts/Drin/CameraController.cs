using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;
    private Vector3 vel;
    private float baseSize;
    public AnimationCurve shakeCurve;
    private Camera cam;
    public float smoothTime;
    public float fovShake;
    public float traumaMult = 1f;
    private float trauma;
    public float rotationShake;
    public float positionShake;
    private bool shaking = false;
    public Vector2 kickBackDir;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
        baseSize = cam.orthographicSize;
    }
    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 goal = (Vector2)target.position;

        goal.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, goal, ref vel, smoothTime * Time.smoothDeltaTime);
    }
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
        Vector3 defaultPos = Vector3.zero;
        Vector3 defaultRot = Vector3.zero;
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

            cam.transform.localRotation = Quaternion.Euler(rotation);
            cam.transform.localPosition = position;

            cam.orthographicSize = baseSize + Random.Range(-fovShake, fovShake);
            yield return new WaitForEndOfFrame();
        }

        kickBackDir = Vector2.zero;

        shaking = false;
        trauma = 0f;

        Reset();

        yield break;
    }

    private void Reset()
    {
        cam.transform.localRotation = Quaternion.identity;
        cam.transform.localPosition = Vector3.zero;
        cam.orthographicSize = baseSize;
    }
}
