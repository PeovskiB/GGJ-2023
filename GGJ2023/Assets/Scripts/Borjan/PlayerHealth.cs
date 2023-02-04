using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
   [SerializeField] private int maxHealth; 

   [SerializeField] private Image[] hearts; 
   [SerializeField] private Sprite fullHeart; 
   [SerializeField] private Sprite halfHeart; 
   [SerializeField] private Sprite emptyHeart; 

    private int health;
    public int Health{
        get{
            return health;
        }
        set{
            if(value > maxHealth){
                health = maxHealth;
            }else if(value < 0){
                health = 0;
                //AudioManager.instance.PlaySound("PlayerHurt");
            }else if(value < health){
                health = value;
                //AudioManager.instance.PlaySound("PlayerHurt");
            }
            else{
                health = value;
            }

            for(int i = 0; i < hearts.Length; i++){
                if(i < health/2){
                    hearts[i].sprite = fullHeart;
                }else if(i < health/2  + health%2){
                    hearts[i].sprite = halfHeart;
                }
                else{
                    hearts[i].sprite = emptyHeart;
                }

                if(i<maxHealth/2){
                    hearts[i].enabled = true;
                }else{
                    hearts[i].enabled = false;
                }
            }
           
        }
    }

    private void Start() {
        Health = maxHealth;
    }

    public void TakeDamage(int x){
        Health -= x;
        if(health <= 0)
           Death();
    }

    public void AddTwoMaxHP(){
        maxHealth += 2;
        Health += 2;
    }

    private void Death(){
        Destroy(gameObject);
    }

}
