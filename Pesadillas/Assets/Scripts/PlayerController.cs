using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>PlaverController</c> Controla las habilidades del jugador al igual que las máscaras.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velocidad;
    //Sirve para determinar si estamos en el aire o en una plataforma y asi poder hacer el salto
    [SerializeField] private Transform revisaHayPisoAbajo;
    [SerializeField] private LayerMask suelo;
    [SerializeField] private float poderSalto;
    //El tiempo coyote nos permite hacer que el jugador salte aunque tecnicamente no este en el suelo
    [SerializeField] private float tiempoCoyote;
    private float contadorTiempoCoyote;
    //El buffer de salto sirve para preparar el siguiente aunque no hayamos llegado al suelo
    [SerializeField] private float bufferSaltoTiempo;
    private float contadorBufferSalto;
    //Doble salto 
    private bool dobleSalto;
    //Sirve para saber si el sprite esta viendo hacia la derecha o izuierda 
    //Inicia en verdadero
    private bool viendoDerecha = true;
    private float xInput;
    //Lo necesario para las máscaras 
    [SerializeField] private GameObject[] Fases;
    //El estado actual de nuestro protagonista 0 es normal, 1 es luz, 2 es invisible
    private int state = 0;
    [SerializeField] private float duracionLuz;
    [SerializeField] private float recargaLuz;
    [SerializeField] private float duracionInvisible;
    [SerializeField] private float recargaInvisible;
    private bool estadoLuz = true, estadoInvisible = true, estadoMascara = true;
    public Text MensajeLuz, MensajeInvisible, mensajeVida;
    private float tempVelocidad, tempPoderSalto;
    //Necesario para la vida 
    [SerializeField] private int vida;

    void Awake()
    {
        tempPoderSalto = poderSalto;
        tempVelocidad = velocidad;     
    }

    void Update()
    {
        //Movimiento básico de izquierda a derecha 
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * velocidad, rb.velocity.y);

        //Solo aceleramos cuando recibamos un input por partr del usuario
        if (xInput > 0.0f)
            transform.localScale = Vector3.one;

        #region Salto

        if (Input.GetButtonDown("Jump"))
        {
            contadorBufferSalto = bufferSaltoTiempo;
            if (dobleSalto | EstaEnElSuelo() || (contadorTiempoCoyote > 0f && contadorBufferSalto > 0f))
            {
                rb.velocity = new Vector2(rb.velocity.x, poderSalto);
                dobleSalto = !dobleSalto;
                contadorBufferSalto = 0f;
            }

        }
        else
        {
            contadorBufferSalto -= Time.deltaTime;
        }

        //Mientras mas tiempo se mantenga presinado mas salta
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            contadorTiempoCoyote = 0f;
        }

        //Restablecemos la variable para permitir en un futuro el doble salto
        if (EstaEnElSuelo() && !Input.GetButtonDown("Jump"))
            dobleSalto = !dobleSalto;

        //Implementación de Coyote time
        if (EstaEnElSuelo())
        {
            contadorTiempoCoyote = tiempoCoyote;
        }
        else
        {
            contadorTiempoCoyote -= Time.deltaTime;
        }
        #endregion
        Flip();

        //Necesarias para usar las máscaras 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mascaraLuz();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mascaraInvisible();
        }
    }


    /// <summary>
    /// Crea una esfera en la parte inferior del personaje.Nos permite saber si estamos en el aire o en una plataforma
    /// </summary>    
    private bool EstaEnElSuelo()
    {
        return Physics2D.OverlapCircle(revisaHayPisoAbajo.position, 0.2f, suelo);
    }

    /// <summary>
    /// Voltea el asset del personaje dependiendo de en que dirección esta viendo 
    /// </summary>  
    private void Flip()
    {
        if (viendoDerecha && xInput < 0f || !viendoDerecha && xInput > 0f)
        {
            //"volteamos" los valores de acuerdo a la nueva dirección del jugador 
            viendoDerecha = !viendoDerecha;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    #region Máscaras/Habilidades.

    private void ActivarEstado()
    {
        for (int i = 0; i < Fases.Length; i++)
        {
            if (i == state)
            {
                Fases[i].SetActive(true);
            }
            else
            {
                Fases[i].SetActive(false);
            }
        }
    }

    private void mascaraLuz()
    {
        if (estadoLuz && estadoMascara)
        {
            estadoMascara = false;
            estadoLuz = false;
            state = 1;
            ActivarEstado();
            StartCoroutine(UsarLuz());
        }
    }
    private void mascaraInvisible()
    {
        if (estadoInvisible && estadoMascara)
        {
            estadoMascara = false;
            estadoInvisible = false;
            state = 2;
            ActivarEstado();
            StartCoroutine(UsarInvisible());
        }
    }

    private void efectosInvisibilidad()
    {
        tempVelocidad = velocidad;
        tempPoderSalto = poderSalto;
        velocidad = velocidad / 2;
        poderSalto = poderSalto / 2;
    }

    private void regresarAFaseNormal()
    {
        velocidad = tempVelocidad;
        poderSalto = tempPoderSalto;
        estadoMascara = true;
        state = 0;
        ActivarEstado();
    }



    private IEnumerator UsarLuz()
    {
        MensajeLuz.text = "Se esta usando luz";
        yield return new WaitForSeconds(duracionLuz);
        regresarAFaseNormal();
        MensajeLuz.text = "Recargando";
        yield return new WaitForSeconds(recargaLuz);
        MensajeLuz.text = "Luz";
        estadoLuz = true;
    }

    private IEnumerator UsarInvisible()
    {
        MensajeInvisible.text = "Se esta usando Invisible";
        efectosInvisibilidad();
        yield return new WaitForSeconds(duracionInvisible);
        regresarAFaseNormal();
        MensajeInvisible.text = "Recargando";
        ActivarEstado();
        yield return new WaitForSeconds(recargaInvisible);
        MensajeLuz.text = "Invisible";
        estadoInvisible = true;
    }
    #endregion

    #region Interacción con el mundo 
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "PlataformaMovil")
        {
            transform.parent = collision.transform;
        }

        if (collision.gameObject.tag == "Enemigo")
        {
            PerderVida(1);
            rb.AddForce(rb.velocity * 0.5f, ForceMode2D.Impulse);

        }
    }

    private void OncollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovil")
        {
            transform.parent = null;
        }
    }
    #endregion

    #region  Todo lo relacionado a la vida

    private void PerderVida(int dano)
    {
        if (vida > 0)
        {
            vida = vida - dano;
            mensajeVida.text = vida.ToString() ;         
        }
        else
        {
            Morir();
        }

    }
    private void Morir()
    {
        mensajeVida.text = "Ha muerto";
        Destroy(gameObject); 
           
     }
    #endregion

}

