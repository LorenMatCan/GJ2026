using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonMusica : MonoBehaviour
{
  public static BotonMusica btnM; 
  private void Awake()
    {
      if (btnM != null && btnM  != this)
        {
            Destroy(this.gameObject);
            return;
        }
        btnM  = this; 
        DontDestroyOnLoad(this.gameObject); 
    }

}
