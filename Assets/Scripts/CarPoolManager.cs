using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPoolManager : MonoBehaviour
{

    // LO PRIMERO
    // lo vamos a hacer un pseudo singleton 
    // voy a usar una propiedad
    // mecanismo que divide el acceso de lectura / escritura a una variable
    // la variable puede ser anónima (como aquí)
    public static CarPoolManager Instance {
        get;
        private set;
    }

    // la variable puede estar definida
    private float _dummy;
    public float Dummy{
        get{
            return _dummy;
        }
        set{
            _dummy = value;
        }
    }

    [SerializeField]
    private GameObject _objetoOriginal;
    
    [SerializeField]
    private int _tamanioDePool;
    
    private Queue<GameObject> _pool;

    // sucede una vez al inicio, siempre corre no importa 
    // si el objeto está deshabilitado

    // TODOS Los awakes de los objetos ya creados corren al principio
    // PERO no sabemos cuál corre antes que los demás
    void Awake() {
        //print("AWAKE");

        // mecanismo de singleton correctivo
        // osea, si ya existe pelas
        if(Instance != null){
            // significa que ya fue asignada
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //print("START");

        // vamos a crear el pool
        _pool = new Queue<GameObject>();
        for(int i = 0; i < _tamanioDePool; i++){

            GameObject nuevoObjeto = Instantiate<GameObject>(_objetoOriginal);
            _pool.Enqueue(nuevoObjeto);
            nuevoObjeto.SetActive(false);
        }
    }

    // Start is called before the first frame update
    // corre para todos después de terminar TODOS los awakes
    void Start()
    {
        

    }

    // Update is called once per frame
    // frame?
    // corre en intervalos irregulares 
    void Update()
    {
        //print("UPDATE");
    }

    // corre también 1 vez por frame
    // se ejecuta al terminar TODOS los updates 
    // de TODOS los componentes de TODOS los objetos
    void LateUpdate(){
        //print("LATE UPDATE");
    }

    // corre en intervalos regulares especificados por engine
    // tiene que ser menos frames que update
    void FixedUpdate(){
        //print("FIXED UPDATE");
    }

    public GameObject ActivarObjeto(Vector3 posicion){

        print(posicion);
        // revisar si queue tiene objetos disponibles
        if(_pool == null || _pool.Count == 0){
            Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
            return null;
        }

        GameObject objetoActivado = _pool.Dequeue();
        objetoActivado.SetActive(true);
        objetoActivado.transform.position = posicion;
        return objetoActivado;
    }

    public void DesactivarObjeto(GameObject objetoADesactivar){
        objetoADesactivar.SetActive(false);
        _pool.Enqueue(objetoADesactivar);
    }
}
