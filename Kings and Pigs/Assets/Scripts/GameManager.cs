using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //static é uma variavel unica de classe
    private static int vida = 3;
    [SerializeField] private int vidaInicial = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //criando o metodo para ir para outra cena
    public void MudaCena(string destino)
    {
        SceneManager.LoadScene(destino);
    }
    //get vida
    public int GetVida()
    {
        return vida;
    }
    public void SetVida(int novaVida)
    {
        vida = novaVida;
    }
    public void GameOver()
    {
        //resetando a vida
        vida = vidaInicial;
        //indo para a cena inicial
        SceneManager.LoadScene("Cena 1");
    }
}
