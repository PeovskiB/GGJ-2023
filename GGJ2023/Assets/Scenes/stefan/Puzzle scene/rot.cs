using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rot : MonoBehaviour
{
    public float rotationSpeed = 90f;

    public GameObject puzzlepiece;

    public int chanceofflipping = 0;
    private bool isRotating = false;
    private Quaternion targetRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating){
            isRotating = true;
            targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f, rotationSpeed));
            StartCoroutine(RotatePuzzle());
        }
    }

    IEnumerator RotatePuzzle()
    {
        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isRotating = false;
    }
}
