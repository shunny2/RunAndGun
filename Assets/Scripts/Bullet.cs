using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    private int damage = 1;
    public float destroyTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime); // Destrói a bala depois de 1.5 segundos.
        damage = GameManager.gameManager.damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        Enemy otherEnemy = collider.GetComponent<Enemy>(); // Verifica se o objeto que foi colidido tenha o componente Enemy.
        Boss boss = collider.GetComponent<Boss>();

        if(otherEnemy != null) {
            otherEnemy.TookDamage(damage);
        }

        if(boss != null) {
            boss.TookDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
