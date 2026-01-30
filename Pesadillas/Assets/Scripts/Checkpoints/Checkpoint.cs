using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Jugador"))
        {
            Debug.Log("Entro el check");
            GameController.Instance.UpdateCheckpoint(transform.position);
           
        }
    }
}
