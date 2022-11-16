using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class RequestConArgumentos : UnityEvent<ListaCarros> {}
public class RequestManager : MonoBehaviour
{

    [SerializeField]
    private UnityEvent _requestRecibidaSinArgumentos;

    [SerializeField]
    private RequestConArgumentos _requestConArgumentos;
    
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
            string jsonSource = PseudoServer.Instance.JSON;
            
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
            yield return new WaitForSeconds(1);
        }
    }
}
