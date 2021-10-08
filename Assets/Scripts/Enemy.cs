using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float attackDistance;
    public GameObject coin;
    public GameObject deathAnimation;

    protected Animator anim;
    protected bool facinRight = true;
    protected Transform target;
    protected float targetDistance;
    protected Rigidbody2D rb2D;
    protected SpriteRenderer sprite;

    void Awake()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update() // Sobreescreve o update para os que vão herdar
    {
        targetDistance = transform.position.x - target.position.x; // Posição do objeto menos a posição do player

    }

    protected void Flip() 
    {
        facinRight = !facinRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TookDamage(int damage)
    {
        health -= damage; // Diminui a vida de acordo com o dano do player
        if(health <= 0) {
            Instantiate(coin, transform.position, transform.rotation); // Instanciando a moeda
            Instantiate(deathAnimation, transform.position, transform.rotation); // Instanciando a animação

            gameObject.SetActive(false); // Desativando o inimigo
        }else {
            StartCoroutine(TookDamageCoRoutine());
        }
    }

    IEnumerator TookDamageCoRoutine() // Criando a CoRotina para mudar a cor da sprite após o dano.
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
