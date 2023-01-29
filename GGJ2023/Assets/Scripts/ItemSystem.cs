using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSystem : MonoBehaviour
{

    public static ItemSystem instance;

    private List<Item> ItemsOwned = new List<Item>();

    [SerializeField] int maxItemCount;
    //Ui for the items
    [SerializeField] private Image[] ItemImages;


    private void Awake() {
        if(instance == null){
            instance = this;
        }    
        else{
            Destroy(gameObject);
        }
    }


    private void Start() {
        UpdateItemUI();
    }

    public void PickUpNewItem(Item item){
        PlayerStats.instance.stats.ObtainItem(item.id);
        ItemsOwned.Add(item);
        UpdateItemUI();
    }
    
    void UpdateItemUI(){
        for(int i = 0; i < maxItemCount; i++){              
                if(i < ItemsOwned.Count){
                    ItemImages[i].enabled = true;
                    ItemImages[i].sprite = ItemsOwned[i].icon;
                    //Debug.Log("ItemUIUpdated");
                }else{
                    ItemImages[i].enabled = false;
                }
        }
    }

    public bool CheckIfItemOwned(int ind){
        for(int i = 0; i < ItemsOwned.Count; i++){
            if(ItemsOwned[i].id == ind)
                return true;
        }
        return false;
    }

    public bool ItemLimitIsReached(){
        if(ItemsOwned.Count == maxItemCount)
            return true;
        else
            return false;
    }


}
