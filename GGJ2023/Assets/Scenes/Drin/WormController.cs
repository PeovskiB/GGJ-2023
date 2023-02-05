using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class WormController : MonoBehaviour
{
    public float rotSpd, movSpd;
    public List<Transform> targetList;
    private int tid = 0;
    private Transform target;
    private bool resetNow = false;
    private bool start = false;
    public float timer = 0f;
    public static WormController instance;
    public TextMeshProUGUI countdown;


    public void SetTarget(Transform goal)
    {
        target = goal;

    }
    public void ResetTarget()
    {
        tid++;
        if (tid >= targetList.Count) tid = 0;
        SetTarget(targetList[tid]);
    }

    public void TriggerBoss()
    {
        start = true;
    }

    private void Start()
    {
        SetTarget(targetList[0]);
        if (instance == null) instance = this;
    }
    public static void Stop()
    {
        instance.start = false;
        Tentacles.instance.stop = true;
    }
    void TriggerWin()
    {
        GameState.WinScreen();
    }
    public float timeLimit;
    void LateUpdate()
    {
        if (!start) return;
        timer += Time.deltaTime;
        if (timer < timeLimit)
            countdown.text = (timeLimit - timer).ToString("##") + "s";
        if (timer >= timeLimit)
        {
            countdown.text = "Victory!";
            TriggerWin();
        }

        Vector2 dir = target.position - transform.position;
        float agl = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(agl, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpd * Time.smoothDeltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target.position, movSpd * Time.smoothDeltaTime);

        if (Vector2.Distance(transform.position, target.position) < 1f)
        {
            if (!resetNow)
            {
                ResetTarget();
                resetNow = true;
            }
        }
        else resetNow = false;
    }

}
