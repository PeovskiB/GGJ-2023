using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    
    [SerializeField] private int damage = 2;

   private void OnTriggerStay2D(Collider2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if(playerHealth != null)
            playerHealth.TakeDamage(damage);
    }

}
