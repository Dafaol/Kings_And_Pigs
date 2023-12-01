using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    [SerializeField] private string destino;
    private Animator meuAnim;
    // Start is called before the first frame update
    void Start()
    {
        meuAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Abrindo()
    {
        meuAnim.SetTrigger("Abrindo");
    }
    //indo para o destino
    public void IndoParaDestino()
    {
        //acessando o game manager
        FindObjectOfType<GameManager>().MudaCena(destino);
    }
}
