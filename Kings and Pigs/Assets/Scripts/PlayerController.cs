using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velh = 5f;
    [SerializeField] private float velv = 7f;
    [SerializeField] private int totalPulos = 1;
    [SerializeField]private int qtdPulos = 1;
    [SerializeField] private int vida = 3;
    private float delayDano = 0f;
    //elementos do raycast
    [SerializeField] private LayerMask layerLevel;
    private BoxCollider2D boxColl;
    private Rigidbody2D meuRB;
    private Animator meuAnim;
    // Start is called before the first frame update
    void Start()
    {
        //pegando o meu RB
        meuRB = GetComponent<Rigidbody2D>();
        //pegando o animator
        meuAnim = GetComponent<Animator>();
        //pegando o box collider
        boxColl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();
        Pulando();
        Invencibilidade();
    }

    private void Invencibilidade()
    {
        //diminuindo o dalay dano
        if (delayDano > 0f)
        {
            delayDano -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        meuAnim.SetBool("OnGround", IsGrounded());
        //se tocou no chao , reseta os pulos
        if (IsGrounded())
        {
            qtdPulos = totalPulos;
        }
    }
    private void Movimentacao()
    {
        //pegando meu input
        var movimento = Input.GetAxis("Horizontal") * velh;
        //passando a velocidade para o Rigid Body
        meuRB.velocity = new Vector2(movimento, meuRB.velocity.y);

        //ajustando a escala dele para olhar para onde vai
        if (movimento != 0)
        {
            //fazendo a escala ser ajsutada dependendo da velocidade
            //se a velocidade for positiva a escala dele é 1
            //se a velocidade for negativa a escala dele é -1
            transform.localScale = new Vector3(Mathf.Sign(movimento), 1f, 1f);
        }

        meuAnim.SetBool("Movendo", movimento != 0);

    }

    private void Pulando()
    {
        //pegando o input de pulo
        var pulo = Input.GetButtonDown("Jump");
        //definindo o parametro do velv com base na vcelocidade y do rb
        meuAnim.SetFloat("Velv", meuRB.velocity.y);

        //checando se pulou e se tem pulos suficiente
        if (pulo && qtdPulos > 0)
        {
            //se pulou , altera a velocidade do eixo Y do rigidbody
            meuRB.velocity = new Vector2(meuRB.velocity.x, velv);
            //diminuindo a quantidade de pulos
            qtdPulos --;
            //avisando que nao esta no chao
            //meuAnim.SetBool("OnGround", false);
        }
    }
    //colisao
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checando se esta colidindo com o chao
        if (collision.gameObject.CompareTag("Ground"))
        {
            //se tocou no chao entao vai resetar os pulos
            //qtdPulos = totalPulos;
            //avisando que tocou no chao
            //meuAnim.SetBool("OnGround", true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checando se colidiu com o colisor do inimigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //checando se o Y é maior que o Y do inimigo
            if(transform.position.y > collision.transform.position.y)
            {
                //ganhando impulso
                meuRB.velocity = new Vector2(meuRB.velocity.x, velv);

                //pegando o animator do pai do colidor do pig E ativar o trigger
                collision.GetComponentInParent<Animator>().SetTrigger("Dano");
                //collision.GetComponentInParent<EnemyController>
            }
            else
            {
                //perdendo vida se o delay dano menor ou igual a 0
                if(delayDano <= 0f)
                {
                    vida--;

                    //reseta o dalay dano
                    delayDano = 2f;
                }
                
            }
        }
    }
    //saiu da colisao
    private void OnCollisionExit2D(Collision2D collision)
    {
        //checando se parou de tocar no chao
        if (collision.gameObject.CompareTag("Ground"))
        {
            //parou de tocar no chao , entao tocar animacao de pulo mesmo se nao tiver tocado espaço
            meuAnim.SetBool("OnGround", false);
        }
    }

    //raycast de colisaono chao
    private bool IsGrounded()
    {
        //criando o raycast             //pegando os limites do colisor
        bool chao = Physics2D.Raycast(boxColl.bounds.center, Vector2.down, 0.5f, layerLevel);

        Color cor;
        if(chao) 
        {
            cor = Color.red;
        }
        else
        {
            cor = Color.green;
        }

        //debug da linha
        Debug.DrawRay(boxColl.bounds.center, Vector2.down, cor);

        return chao;

    }
}
