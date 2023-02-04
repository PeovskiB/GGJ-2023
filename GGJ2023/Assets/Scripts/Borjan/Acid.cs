using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Acid : MonoBehaviour
{

    Vector2 direction = Vector2.down;
    [SerializeField] float speed = 4f;

    int damage = 2;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + direction * speed *Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        transform.position = newPosition;

        foreach(RaycastHit2D hit in hits){
            PlayerHealth playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth != null){
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
            if(hit.collider.gameObject.layer == 6){
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(int x){
        damage = x;
    }

}
