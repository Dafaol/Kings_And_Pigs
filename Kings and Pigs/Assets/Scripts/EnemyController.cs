
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //meu RB
    private Rigidbody2D meuRB;
    //meu anim
    private Animator meuAnim;
    //elementos do raycast
    private BoxCollider2D boxCol;
    [SerializeField] LayerMask layerLevel;

    //boxcollider do colisor
    [SerializeField]private BoxCollider2D colisor;

    [SerializeField] private float velh = 2f;
    [SerializeField] private float espera = 2f;
    private bool morte = false;
    // Start is called before the first frame update
    void Start()
    {
        //pegando o rb
        meuRB = GetComponent<Rigidbody2D>();
        //pegando o animator
        meuAnim = GetComponent<Animator>();
        //pegando o box collider
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //só pode mover se nao estiver morto
        if (!morte)
        {
            Movendo();
        }
    }

    public void Morrendo()
    {
        
        morte = true;
        //tirando a velocidade
        meuRB.velocity = Vector2.zero;
        //destruindo depois de um tempo
        Destroy(gameObject, 2f);
        //desativando o colisor
        colisor.enabled = false;
    }
    private void Movendo()
    {
        //checando se esta batendo na parede
        if(BatendoParede())
        {
            meuRB.velocity = new Vector2(meuRB.velocity.x * -1f, meuRB.velocity.y);
        }
        //olhando para onde esta indo, se a velocidade for diferente de zero
        if (meuRB.velocity.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(meuRB.velocity.x) * -1, 1f, 1f);
        }

        //só se move se o tempo acabou
        if (espera <= 0f)
        {
            //movendo ele
            int dir = Random.Range(-1, 2);
            //multiplicando a velocidade pela direçao
            meuRB.velocity = new Vector2(velh * dir, meuRB.velocity.y);
            
            
            //resentando a espera
            espera = Random.Range(2f, 10f);
        }
        //se a espera for maior do que zero , entao diminui
        else
        {
            espera -= Time.deltaTime;
        }

        //checando se esta se movendo e informando ao animator
        meuAnim.SetBool("Movendo", meuRB.velocity.x != 0);
        
    }
    private bool BatendoParede()
    {
        //criando o raycast pra ver se tem parede na frente
        var dir = new Vector2(Mathf.Sign(meuRB.velocity.x), 0f);
        bool parede = Physics2D.Raycast(boxCol.bounds.center, dir,1f,layerLevel);


        Color cor;
        if (parede)
        {
            cor = Color.red;
        }
        else
        {
            cor = Color.green;
        }

        //debug da linha
        Debug.DrawRay(boxCol.bounds.center, dir, cor);


        return parede;
    }
}
