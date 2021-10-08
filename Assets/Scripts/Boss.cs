using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Rigidbody2D bullet;
    public Transform[] shotSpawners;
    public float minYForce, maxYForce;
    public float fireRateMin, fireRateMax;

    public GameObject enemy;
    public Transform enemySpawn;
    public float minEnemyTime, maxEnemyTime;

    public int health;

    public GameObject laser;
    public Transform laserSpawn;
    public float minLaserTime, maxLaserTime;

    private bool isDead = false;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateBoss()
    {
        GetComponent<PolygonCollider2D>().enabled = true;
        Invoke("Fire", Random.Range(fireRateMin, fireRateMax));
        Invoke("InstantiateEnemies", Random.Range(minEnemyTime, maxEnemyTime));
        Invoke("FireLaser", Random.Range(minLaserTime, maxLaserTime));
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void InstantiateEnemies()
    {
        if(!isDead) {
            // Instanciando os inimigos do boss.
            Instantiate(enemy, enemySpawn.position, enemySpawn.rotation);
            Invoke("InstantiateEnemies", Random.Range(minEnemyTime, maxEnemyTime));
        }
    }

    void Fire() 
    {
        if(!isDead) {
            Rigidbody2D tempBullet = Instantiate(bullet, shotSpawners[Random.Range(0, shotSpawners.Length)].position, Quaternion.identity); // Quaternion.identity significa sem rotação.
            tempBullet.AddForce(new Vector2(0, Random.Range(minYForce, maxYForce)), ForceMode2D.Impulse);
            // Chamando a própria função.
            Invoke("Fire", Random.Range(fireRateMin, fireRateMax));
        }
    }

    void FireLaser()
    {
        if(!isDead) {
            // Instanciando o laser do boss.
            Instantiate(laser, laserSpawn.position, laserSpawn.rotation);
            Invoke("FireLaser", Random.Range(minLaserTime, maxLaserTime));
        }
    }

    public void TookDamage(int damage)
    {
        health -= damage; // Diminui a vida de acordo com o dano do player
        if(health <= 0) {
            isDead = true;

            // Procura por todos objetos do tipo enemy em cena e coloca dentro do vetor.
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach(Enemy enemy in enemies) { // Para cada inimigo...
                enemy.gameObject.SetActive(false); // Desativando
            }
            // Procura por todos objetos do tipo bullet em cena e coloca dentro do vetor.
            Bullet[] bullets = FindObjectsOfType<Bullet>();
            foreach(Bullet bullet in bullets) { // Para cada inimigo..
                bullet.gameObject.SetActive(false); // Desativando
            }

            Invoke("LoadScene", 2f);

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
