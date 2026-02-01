using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Este archivo es el game controller pero del jugador
 */
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float segundosEsperaRespawn;
    public GameObject gameOverUI;

    private Vector2 checkpointPos;
    public PlayerController player;

    

    void Awake()
    {
        Instance = this;
        BGM.BGMMenu.CambiarMusica(0);
        player = GameObject.FindGameObjectWithTag("Jugador").GetComponent<PlayerController>();
    }

    void Start()
    {
        checkpointPos = player.transform.position;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
        Debug.Log("Checkpoint actualizado a: " + pos);
    }

    public void Respawneo()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(segundosEsperaRespawn);

        player.transform.position = checkpointPos;
        player.ResetearVelocidad();
        if (player.vida == 0)
        {
            PlayerDied();
        }

}

    public void PlayerDied()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}


