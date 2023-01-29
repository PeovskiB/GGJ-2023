using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roots : MonoBehaviour

{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && PlayerStats.instance.stats.GetCanDestroyRoots()){
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

}
