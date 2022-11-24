using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoServer : MonoBehaviour
{

    public static PseudoServer Instance {
        get;
        private set;
    } 

    public string JSON {
        get;
        private set;
    }
    private IEnumerator ienumerator;
    private Coroutine coroutine;

    void Awake() 
    {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ienumerator = Server();
        coroutine = StartCoroutine(ienumerator);
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.A)){
            StopAllCoroutines();
        }
        if(Input.GetKeyDown(KeyCode.B)){
            StopCoroutine(ienumerator);
        }
        if(Input.GetKeyDown(KeyCode.C)){
            StopCoroutine(coroutine);
        }


    }

    // CORRUTINAS
    // mecanismo que tiene unity para lidiar con concurrencia
    // es pseudo concurrente - NO hay un hilo pero da la impresión de 
    // ejecutarse de manera simultánea

    // MÁS NOTAS DE CORRUTINAS:
    // las corrutinas están relacionadas al componente
    IEnumerator Server() {

        while(true){
            ListaCarros listaCarros = new ListaCarros();
            /*
            listaCarros.carros = new Carro[10];

            for(int i = 0; i < listaCarros.carros.Length; i++){
                listaCarros.carros[i] = new Carro();
                listaCarros.carros[i].id = i;
                listaCarros.carros[i].x = Random.Range(0f, 10f);
                listaCarros.carros[i].y = Random.Range(0f, 10f);
                listaCarros.carros[i].z = Random.Range(0f, 10f);
            }

            JSON = JsonUtility.ToJson(listaCarros);
            // print(JSON);
            */
            yield return new WaitForSeconds(0.5f);
            
        }
    }
}
