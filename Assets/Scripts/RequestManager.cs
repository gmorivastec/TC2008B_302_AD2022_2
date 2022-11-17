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

    IEnumerator HacerRequest() {

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

                for(int i = 0; i < listaCarros.carros.Length; i++){

                    print(
                        listaCarros.carros[i].x + ", " + 
                        listaCarros.carros[i].y + ", " + 
                        listaCarros.carros[i].z 
                    );
                }

                _requestRecibidaSinArgumentos?.Invoke();
                _requestConArgumentos?.Invoke(listaCarros);
            }
            yield return new WaitForSeconds(_esperaEntreRequests);
        }
    }
}
