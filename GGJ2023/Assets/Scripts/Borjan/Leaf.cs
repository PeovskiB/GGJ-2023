using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    
    private bool activated = false;


   private void OnTriggerStay2D(Collider2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if(playerHealth != null && !activated){
            activated = true;
            playerHealth.AddTwoMaxHP();
            Destroy(gameObject);
        }
            
    }

}
