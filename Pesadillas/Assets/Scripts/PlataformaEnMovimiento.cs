using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaEnMovimiento : MonoBehaviour
{
    public GameObject ObjetoAmover;
    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocidad;

    private Vector3 MoverHacia;
    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObjetoAmover.transform.position = Vector3.MoveTowards(ObjetoAmover.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if(ObjetoAmover.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
        }

         if(ObjetoAmover.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
        }
        
    }

    // IMPORTANTE: Para que el personaje no se resbale 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hola"); 
        if (collision.gameObject.CompareTag("Jugador")) // O "Player"
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            collision.transform.SetParent(null);
        }
    }
}
