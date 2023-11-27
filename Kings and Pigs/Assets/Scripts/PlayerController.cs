using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velh = 5f;
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
        
}
