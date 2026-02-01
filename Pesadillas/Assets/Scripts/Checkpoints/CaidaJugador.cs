using UnityEngine;

public class CaidaJugador : MonoBehaviour
{
    public int dano = 1;
    /*
    void OnCollisionEnter2D(Collider2D collision)
    {
        GameController.Instance.Respawneo();
        if (collision.gameObject.CompareTag("Jugador"))
        {

            GameController.Instance.Respawneo();
            /*
             PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                //player.PerderVida(dano, collision.GetContact(0).normal);
                player.PerderVidaRespawn(dano);
                player.vida += 1;
                
            }
           
        }
    }
*/ 
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Jugador"))
        {
            PlayerController player = otherCollider.gameObject.GetComponent<PlayerController>();
            player.PerderVidaRespawn(dano);
            GameController.Instance.Respawneo();
            /*
             // PlayerController player = otherCollider.gameObject.GetComponent<PlayerController>();
             if (player != null)
             {
                // player.PerderVida(dano, otherCollider.GetContact(0).normal);
                 player.PerderVidaRespawn(dano);
                // player.vida += 1;
                 GameController.Instance.Respawneo();
             }
             */
        }
    }
}
