using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : MonoBehaviour
{
    public int len;
    private LineRenderer line;
    public Vector3[] poses, segV;
    public float spd;
    public Transform targetDir, targetPos;
    public float targetDist, trailSpeed;

    public float wglSpd, wglMag;
    public Transform wgl;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = len;
        poses = new Vector3[len];
        segV = new Vector3[len];
    }

    void Update()
    {
        wgl.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wglSpd) * wglMag);
        poses[0] = targetPos.position;

        for (int i = 1; i < poses.Length; i++)
        {
            poses[i] = Vector3.SmoothDamp(poses[i], poses[i - 1] + targetDir.right * -1 * targetDist, ref segV[i], spd * Time.deltaTime / trailSpeed);
        }

        line.SetPositions(poses);
    }
}
