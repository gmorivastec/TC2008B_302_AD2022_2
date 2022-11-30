using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    [SerializeField]
    private Carro[] _carros;

    [SerializeField]
    private float _escalaTiempo = 1;
    private GameObject[] _carrosGO;
    private Vector3[] _direcciones;

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

        /*
        if(Input.GetKeyDown(KeyCode.R)){

            // simulando un update en datos
            for(int i = 0; i < _carros.Length; i++){
                _carros[i].x = Random.Range(0f, 10f);
                _carros[i].z = Random.Range(0f, 10f);
            }

            PosicionarCarros();
        }*/

        // actualizar posición basado en intervalos regulares
        // RECUERDA QUE MOVEMOS LOS GAME OBJECTS
        if(_direcciones != null && _direcciones.Length > 0){
            for(int i = 0; i < _carrosGO.Length; i++){

                // reorientar
                _carrosGO[i].transform.forward = _direcciones[i].normalized;

                // aplicar desplazamiento
                _carrosGO[i].transform.Translate(
                    _direcciones[i] * Time.deltaTime * _escalaTiempo, 
                    Space.World
                );
            }
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
            _direcciones = new Vector3[_carros.Length];

            PosicionarCarros();

            for(int j = 0; j < _carros.Length; j++){

                // en cada paso calcular vector dirección para cada carro
                if(i < datos.steps.Length - 1){
                    _direcciones[j] = new Vector3(
                        datos.steps[i + 1].carros[j].x - datos.steps[i].carros[j].x,
                        0,
                        datos.steps[i + 1].carros[j].z - datos.steps[i].carros[j].z 
                    );
                } else {
                    _direcciones[j] = Vector3.zero;
                }
            }
            

            yield return new WaitForSeconds(1 / _escalaTiempo);
        }
    }
}
