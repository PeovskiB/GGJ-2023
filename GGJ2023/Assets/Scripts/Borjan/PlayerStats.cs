using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public class ItemStats{
        public int additionalJumps = 0;
        public bool rootClearing = false;

        public void ObtainItem(int id){
            switch (id){
                //Double Jump
                case 0:
                    additionalJumps += 1;
                    break;
                //Acid    
                case 1:
                    rootClearing = true;
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
            
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = new Stats();
    }

}
