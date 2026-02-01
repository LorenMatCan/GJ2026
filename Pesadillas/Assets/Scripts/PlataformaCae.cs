using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCae : MonoBehaviour
{
    [SerializeField] private float tiempoEspera;
    [SerializeField] private float tiempoReaparece;
    private Rigidbody2D rBody;
    private Vector3 posIni;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        posIni = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            Invoke("Cae", tiempoEspera);
            Invoke("Reaparece", tiempoReaparece);

        }
    }
    private void Cae ()
    {
        rBody.isKinematic = false;
    }
     private void Reaparece ()
    {
        rBody.velocity = Vector3.zero;
        rBody.isKinematic = true;
        transform.position = posIni;

    }
}
