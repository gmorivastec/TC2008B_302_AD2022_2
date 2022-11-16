using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPoolTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){

            // crear nuevo carrito en posicion random
            CarPoolManager.Instance.ActivarObjeto(
                new Vector3(
                    Random.Range(0f, 10f),
                    Random.Range(0f, 10f),
                    Random.Range(0f, 10f)
                )
            );
        }
    }
}
