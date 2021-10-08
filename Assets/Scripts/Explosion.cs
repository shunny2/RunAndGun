using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 3;

    void OnTriggerEnter2D(Collider2D collider) 
    {
        Enemy otherEnemy = collider.GetComponent<Enemy>(); // Verifica se o objeto que foi colidido tenha o componente Enemy.
        Boss boss =  collider.GetComponent<Boss>();
        Debug.Log(otherEnemy);
        Debug.Log(boss);
        
        if(otherEnemy != null) {
            otherEnemy.TookDamage(damage);
        }

        if(boss != null) {
            boss.TookDamage(damage);
        }

        Destroy(gameObject);
    }
}
