using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


[System.Serializable]
public class RequestConArgumentos : UnityEvent<ListaCarros> {}
public class RequestManager : MonoBehaviour
{

    [SerializeField]
    private UnityEvent _requestRecibidaSinArgumentos;

    [SerializeField]
    private RequestConArgumentos _requestConArgumentos;

    [SerializeField]
    private string _url = "http://127.0.0.1:5000/";

    [SerializeField]
    private float _esperaEntreRequests = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HacerRequest());

        // _requestRecibidaSinArgumentos += funcion();
    }

    IEnumerator HacerRequestRecurrente() {

        while(true){

            // hacer request al "server" 
            // esto va a cambiar mañana
            // string jsonSource = PseudoServer.Instance.JSON;

            UnityWebRequest www = UnityWebRequest.Get(_url);

            yield return www.SendWebRequest();

            string jsonSource = null;

            //revisar si no hubo broncas
            if(www.result != UnityWebRequest.Result.Success){
                print("ERROR EN REQUEST!");
            } else {
                jsonSource = www.downloadHandler.text;
            }
            
            if(jsonSource != null){

                ListaCarros listaCarros = JsonUtility.FromJson<ListaCarros>(jsonSource);
                print(listaCarros);
/*
                for(int i = 0; i < listaCarros.carros.Length; i++){

                    print(
                        listaCarros.carros[i].x + ", " + 
                        listaCarros.carros[i].y + ", " + 
                        listaCarros.carros[i].z 
                    );
                }
*/
                _requestRecibidaSinArgumentos?.Invoke();
                _requestConArgumentos?.Invoke(listaCarros);
            }
            yield return new WaitForSeconds(_esperaEntreRequests);
        }
    }

    public IEnumerator HacerRequest() {

        // hacer request 1 vez al backend 
        // obtenemos datos 

        // ESTO VA A CAMBIAR PARA SER UN REQUEST COMO EL QUE ESTÁ ARRIBA

        // 1. hacer request
        // 2. recibir respuesta en texto
        // 3. parsear a objeto
        // 4. invocar método de evento para avisar que objeto está parseado
        yield return new WaitForSeconds(1);

        ListaCarros dummy = new ListaCarros();
        dummy.steps = new Step[10];

        for(int i = 0; i < dummy.steps.Length; i++){

            dummy.steps[i] = new Step();
            
            // carros
            dummy.steps[i].carros = new Carro[10];
            for(int j = 0; j < dummy.steps[i].carros.Length; j++){
                dummy.steps[i].carros[j] = new Carro();
                dummy.steps[i].carros[j].x = -6 + j * 1.5f;
                dummy.steps[i].carros[j].z = i * -1f;
            }

            // semáforos
            dummy.steps[i].semaforos = new Semaforo[4];
            for(int j = 0; j < dummy.steps[i].semaforos.Length; j++){

                dummy.steps[i].semaforos[j] = new Semaforo();
                dummy.steps[i].semaforos[j].color = 0;
            }
        }

        print(JsonUtility.ToJson(dummy));

        _requestConArgumentos?.Invoke(dummy);

    /*
{"steps":[{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]},{"carros":[{"id":0,"x":-6.0,"z":0.0},{"id":0,"x":-4.5,"z":1.0},{"id":0,"x":-3.0,"z":2.0},{"id":0,"x":-1.5,"z":3.0},{"id":0,"x":0.0,"z":4.0},{"id":0,"x":1.5,"z":5.0},{"id":0,"x":3.0,"z":6.0},{"id":0,"x":4.5,"z":7.0},{"id":0,"x":6.0,"z":8.0},{"id":0,"x":7.5,"z":9.0}],"semaforos":[{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0},{"id":0,"color":0}]}]}

    */
    }
}
