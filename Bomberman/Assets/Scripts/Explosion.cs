using UnityEngine;

public class Explosion : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.Dead();
        }
    }

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}