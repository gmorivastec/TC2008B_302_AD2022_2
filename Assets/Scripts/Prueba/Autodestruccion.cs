using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruccion : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        // PARA DESPUÉS, MUY IMPORTANTE
        // MEJOR USA UNA CORRUTINA
        // invoca un método con un delay
        Invoke("Autodestruirse", 2);
    }

    void Autodestruirse()
    {
        CarPoolManager.Instance.DesactivarObjeto(gameObject);
    }
}
