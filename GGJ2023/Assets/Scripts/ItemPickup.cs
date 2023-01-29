using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    [SerializeField] private Item item;
    private bool isPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !isPickedUp){
            isPickedUp = true;
            ItemSystem.instance.PickUpNewItem(item);
            Destroy(gameObject);
        }
    }

    public void SetItem(Item it){
        item = it;
    }
}
