using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeman : Enemy
{
    public float walkDistance;

    private bool walk;
    private bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        anim.SetBool("Walk", walk);
        anim.SetBool("Attack", attack);

        if(Mathf.Abs(targetDistance) < walkDistance) {
            walk = true;
        }

        if(Mathf.Abs(targetDistance) < attackDistance) {
            attack = true;
            walk = false;
        }
    }

    private void FixedUpdate() 
    {
        if(walk && !attack) {
            if(targetDistance < 0) { // Direita.
                rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
                if(!facinRight) {
                    Flip();
                }
            }else { // Esquerda.
                rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
                if(facinRight) {
                    Flip();
                }
            }
        }
    }

    public void ResetAttack()
    {
        attack = false; // Para o loop de ataque no inimigo.
    }
}
