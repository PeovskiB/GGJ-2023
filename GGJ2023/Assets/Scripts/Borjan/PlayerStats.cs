using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public class ItemStats{
        public int additionalJumps = 0;
        public bool rootClearing = false;
        public int additionalMaxMana;
        public float additionalManaRegen;

        public void ObtainItem(int id){
            switch (id){
                //Double Jump
                case 0:
                    additionalJumps += 1;
                    break;
                //Root Clearing  
                case 1:
                    rootClearing = true;
                    break;  
                //Max Mana
                case 2:
                    additionalMaxMana += 50;
                    SpellSystem.instance.UpdateManaStats();
                    break;
                //Mana Regen
                case 3:
                    additionalManaRegen += 30f;
                    SpellSystem.instance.UpdateManaStats();
                    break;    
                default:
                    Debug.LogWarning("Error: invalid item id");
                    break;
            }
        
        }
    }

    public class Stats{
        ItemStats itemStats = new ItemStats();

        int baseJumps = 1;
        int baseMaxMana = 100;
        float baseManaRegen = 20f;
        
        public void ObtainItem(int id){
            itemStats.ObtainItem(id);
        }
        
        //Return stat values
        public int GetNumberOfJumps(){
            return baseJumps + itemStats.additionalJumps;
        }

        public bool GetCanDestroyRoots(){
            return itemStats.rootClearing;
        }

        public int GetMaxMana(){
            return baseMaxMana + itemStats.additionalMaxMana;
        }

        public float GetManaRegen(){
            return baseManaRegen + itemStats.additionalManaRegen;
        }

    }

    public static PlayerStats instance;

    public Stats stats;

    private void Awake() {
        if(instance == null){
            instance = this;
        }  
        else{
            Destroy(gameObject);
        }
        stats = new Stats();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
