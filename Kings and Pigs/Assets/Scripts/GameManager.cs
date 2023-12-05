using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //static é uma variavel unica de classe
    private static int vida = 3;
    [SerializeField] private int vidaInicial = 3;

    //variavel dos coraçoes
    [SerializeField] private Image[] coracoes;

    // Start is called before the first frame update
    void Start()
    {
        AjustaVida();
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

    public void AjustaVida()
    {
        //rodar pelo meu vetor
        for (var i=0; i < coracoes.Length; i++)
        {
            //checando se o valor atual é maior do que a vida atual
            if (i < vida)
            {
                //eu tenho mais vidas do que o valor do i
                //Se i for 0 , quer dizer que a vida é ao menos 1
                //se i for 1 , quer dizer que a vida é ao menos 2
                //se i for 2 , quer dizer que a vida é ao menos 3
                coracoes[i].enabled = true;
            }
            else
            {
                coracoes[i].enabled = false;
            }
        }
    }
}
