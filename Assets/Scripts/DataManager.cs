using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    [SerializeField]
    private Carro[] _carros;
    private GameObject[] _carrosGO;

    // Start is called before the first frame update
    void Start()
    {
        _carrosGO = new GameObject[_carros.Length];

        for(int i = 0; i < _carros.Length; i++)
        {
            _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(Vector3.zero);
        }

        PosicionarCarros();
    }

    private void PosicionarCarros() 
    {
        for(int i = 0; i < _carros.Length; i++)
        {
            _carrosGO[i].transform.position = new Vector3(
                    _carros[i].x,
                    0,
                    _carros[i].z
                );
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){

            // simulando un update en datos
            for(int i = 0; i < _carros.Length; i++){
                _carros[i].x = Random.Range(0f, 10f);
                _carros[i].z = Random.Range(0f, 10f);
            }

            PosicionarCarros();
        }
    }

    public void EscucharRequestSinArgumentos() {

        print("HUBO UN REQUEST MUY INTERESANTE!");
    }

    public void EscucharRequestConArgumentos(ListaCarros datos){
        print("DATOS: " + datos);

        // actualizar arreglo _carros de esta clase con 
        // los carros que recibo de "datos"

        // invocar PosicionarCarros()

        StartCoroutine(ConsumirSteps(datos));
    }


    private IEnumerator ConsumirSteps(ListaCarros datos) {

        for(int i = 0; i < datos.steps.Length; i++){

            _carros = datos.steps[i].carros;
            PosicionarCarros();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
