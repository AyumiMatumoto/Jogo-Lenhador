using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private SpriteRenderer sprite;
    private Animator anim;
    private string ladoAtual = "E"; // E==Esq / D==Dir

    // Use this for initialization
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.gameOver) { 
            sprite.flipX = false;
        anim.Play("Die");
        }
    }

    void TrocaLado(string novaPosicao)
    {
        if(ladoAtual != novaPosicao)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            sprite.flipX = !sprite.flipX;
            ladoAtual = novaPosicao;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.gameOver = true;
        GameManager.instance.SalvaPontuacao();
    }

    public void Toque(string ladoToque)
    {
        if (!GameManager.instance.gameOver)
        {
            TrocaLado(ladoToque);
            anim.Play("Cut");
        }
    }
}
