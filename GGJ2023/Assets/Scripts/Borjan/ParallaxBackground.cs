using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    [SerializeField] private Vector2 parallaxEfectMultilier;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEfectMultilier.x, deltaMovement.y * parallaxEfectMultilier.y);
        lastCameraPosition = cameraTransform.position;
    }
}
