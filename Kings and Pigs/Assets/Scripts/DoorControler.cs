using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
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
}
