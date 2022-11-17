using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera[] _camaras;
    
    [SerializeField]
    private int _camaraActual;


    void Start() {

        HabilitarCamara(_camaraActual);
    }

    private void HabilitarCamara(int camaraAHabilitar) {

        for(int i = 0; i < _camaras.Length; i++){

            if(i == camaraAHabilitar){
                _camaras[i].gameObject.SetActive(true);
            } else {
                _camaras[i].gameObject.SetActive(false);
            }
        }
    }

    public void SiguienteCamara() {

        _camaraActual++;

        // reiniciar indice a 0
        _camaraActual %= _camaras.Length;

        HabilitarCamara(_camaraActual);
    }
}
