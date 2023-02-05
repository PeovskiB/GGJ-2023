using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class WormController : MonoBehaviour
{
    public float rotSpd, movSpd;
    public List<Transform> targetList;
    private int tid = 0;
    private Transform target;
    private bool resetNow = false;

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

    private void Start()
    {
        SetTarget(targetList[0]);
    }
    // Update is called once per frame
    void LateUpdate()
    {
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
