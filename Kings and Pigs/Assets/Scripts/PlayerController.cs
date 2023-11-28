using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velh = 5f;
    [SerializeField] private float velv = 7f;
    [SerializeField] private int totalPulos = 1;
    [SerializeField]private int qtdPulos = 1;
    private Rigidbody2D meuRB;
    private Animator meuAnim;
    // Start is called before the first frame update
    void Start()
    {
        //pegando o meu RB
        meuRB = GetComponent<Rigidbody2D>();
        //pegando o animator
        meuAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();
        Pulando();
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
            meuAnim.SetBool("OnGround", false);
        }
    }
    //colisao
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checando se esta colidindo com o chao
        if (collision.gameObject.CompareTag("Ground"))
        {
            //se tocou no chao entao vai resetar os pulos
            qtdPulos = totalPulos;
        }
    }
}
