using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveForeground : MonoBehaviour
{

    Transform playerTransform;
    [SerializeField] GameObject foreGround;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.y > -65)
            foreGround.SetActive(true);
        else
            foreGround.SetActive(false);
    }
}
