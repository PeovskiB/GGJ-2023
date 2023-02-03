using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float hearts = 3;

    private Movement mvm;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        mvm = GetComponent<Movement>();
    }

    public void Hurt()
    {
        hearts--;

        if (hearts <= 0)
            Die();
    }

    private void Die()
    {
        print("Died");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Entrance"))
            CameraController.SetStage(collision.GetComponentInParent<Stage>());
        if (collision.CompareTag("Key"))
        {
            collision.transform.position = transform.position;
            collision.transform.SetParent(transform, true);
        }
    }
}
