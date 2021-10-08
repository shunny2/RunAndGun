using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 600;
    public GameObject bulletPrefab;
    public Transform shotSpawner;
    public Rigidbody2D bomb; // Prefab da bomba;
    public float damageTime = 1f;
    public bool canFire = true;

    private Animator anim;
    private Rigidbody2D rb2D;
    private bool facinRight = true;
    private bool jump;
    private bool onGround = false;
    private Transform groundCheck;
    private float hForce = 0;
    private bool isDead = false;
    private bool crouched;
    private bool lookingUp;
    private bool reloading;
    private float fireRate = 0.5f;
    private float nextFire;
    private bool tookDamage = false;

    private int bullets;
    private float reloadTime;
    private int health;
    private int maxHealth;
    private int bombs;

    GameManager gameManager;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>(); // Referenciando animator.
        gameManager = GameManager.gameManager;

        SetPlayerStatus();
        bombs = gameManager.bombs;
        health = maxHealth;

        UpdateBulletsUI();
        UpdateBombsUI();
        UpdateHealthUI();
    }

    void Update()
    {
        if(!isDead) {
            // Se alguma coisa colidir com a layer ground, ONGROUND vai ser verdadeiro.
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            if(onGround) {
                anim.SetBool("jump", false);
            }

            if(Input.GetButtonDown("Jump") && onGround && !reloading) {
                jump = true;
            }
            else if (Input.GetButtonUp("Jump")) {
                if(rb2D.velocity.y > 0) {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
                }
            }

            if(Input.GetButtonDown("Fire1") && Time.time > nextFire && bullets > 0 && !reloading && canFire) { // Verifica se o tempo atual e maior que o prox tiro.
                nextFire = Time.time + fireRate; // Só pode atirar depois de 0.5 segundos
                anim.SetTrigger("shoot");
                GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
                if(!facinRight && !lookingUp) { // Se estiver virado para a esquerda e nao esta olhando pra cima;
                    tempBullet.transform.eulerAngles = new Vector3(0,0,180);
                }else if(!facinRight && lookingUp) {
                    tempBullet.transform.eulerAngles = new Vector3(0,0,90);
                }
                if(crouched && !onGround) { // Se estiver agachado e nao estiver no chão.
                    tempBullet.transform.eulerAngles = new Vector3(0,0,-90);
                }

                bullets--;
                UpdateBulletsUI();
            }else if(Input.GetButtonDown("Fire1") && bullets <= 0 && onGround) {
                // Chama a coRotina para fazer o recarregamento da arma.
                StartCoroutine(Reloading());
            }
            /*
                GetButtonDown é chamado apenas uma vez quando é pressionado.
                GetButton é chamado várias vezes.
            */
            lookingUp = Input.GetButton("Up");
            crouched = Input.GetButton("Down");

            anim.SetBool("lookingUp", lookingUp); // Quando pressionado, é verdadeiro.
            anim.SetBool("crouched", crouched);

            if(Input.GetButtonDown("Reload") && onGround) {
                // Chama a coRotina para fazer o recarregamento da arma.
                StartCoroutine(Reloading());
            }

            if(Input.GetButtonDown("Fire2") && bombs > 0) {
                Rigidbody2D tempBomb = Instantiate(bomb, transform.position, transform.rotation);
                if(facinRight) {
                    // Adiciona uma força de 8 para direita e de 10 para cima.
                    tempBomb.AddForce(new Vector2(8,10), ForceMode2D.Impulse);
                }else if(!facinRight) {
                    tempBomb.AddForce(new Vector2(-8,10), ForceMode2D.Impulse);
                }

                bombs--;
                UpdateBombsUI();
            }

            if((crouched || lookingUp || reloading) && onGround) {
                // Travando a movimentação. Caso o personagem estiver agachado; olhando pra cima; ou recarregando.
                hForce = 0;
            }
        }
    }

    private void FixedUpdate() 
    {
        if(!isDead) {
            if(!crouched && !lookingUp && !reloading) {
                hForce = Input.GetAxisRaw("Horizontal");
            }

            anim.SetFloat("speed", Mathf.Abs(hForce)); // Mathf.Abs transforma o valor em positivo.

            rb2D.velocity = new Vector2(hForce * speed, rb2D.velocity.y);

            // Se andarmos para direita e ele estiver virado para esquerda a função Flip é chamada.
            if(hForce > 0 && !facinRight) {
                Flip();
            }
            else if(hForce < 0 && facinRight) {
                Flip();
            }

            if(jump) {
                anim.SetBool("jump", true);
                jump = false; // Trava para não pular mais de uma vez.
                rb2D.AddForce(Vector2.up * jumpForce); // Faz o pulo.
            }
        }
    }

    // Método para recarregar as balas.
    IEnumerator Reloading()
    {
        reloading = true;
        anim.SetBool("reloading", true);
        yield return new WaitForSeconds(reloadTime);
        bullets = gameManager.bullets;
        reloading = false;
        anim.SetBool("reloading", false);
        UpdateBulletsUI();
    }

    // Método para verificar o dano.
    IEnumerator TookDamage()
    {
        tookDamage = true;
        health--;
        UpdateHealthUI();
        if(health <= 0) {
            isDead = true;
            anim.SetTrigger("death");
            Invoke("ReloadScene", 2f);
        }else {
            // Player ignora tudo que for da layer inimigo. Evitando colisões.
            Physics2D.IgnoreLayerCollision(9,10);
            // Entra em um loop quando tomar dano. De modo que a sprite do player ativa e desativa. (Pisca)
            for(float i = 0; i < damageTime; i += 0.2f) {
                GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
            // Volta a se colidir denovo.
            Physics2D.IgnoreLayerCollision(9,10,false);
            tookDamage = false;
        }
    }

    // Método para recarregar a cena.
    void ReloadScene()
    {
        // Carrega a cena atual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Função que inverte a sprite.
    void Flip() 
    {
        facinRight = !facinRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1; // Faz a inversão da sprite.
        transform.localScale = scale; // Atualiza a escala do objeto.
    }

    public void SetPlayerStatus()
    {
        fireRate = gameManager.fireRate;
        bullets = gameManager.bullets;
        reloadTime = gameManager.realoadTime;
        maxHealth = gameManager.health;
    }

    public void SetHealthAndBombs(int life, int bomb)
    {
        health += life; // Atualiza a vida atual.
        if(health >= maxHealth) {
            health = maxHealth;
        }
        bombs += bomb;
        UpdateBombsUI();
        UpdateHealthUI();
    }

    void UpdateBulletsUI()
    {
        // Atualiza o numero de balas na interface.
        FindObjectOfType<UIManager>().UpdateBulletsUI(bullets); // Procura pelo objeto do tipo UIManager.
    }

    void UpdateBombsUI()
    {
        // Atualiza o numero de bombas na interface.
        FindObjectOfType<UIManager>().UpdateBombs(bombs);
        gameManager.bombs = bombs; // Atualiza o valor das bombas no gameManager.
    }

    void UpdateHealthUI()
    {
        // Atualiza o numero de vida na interface.
        FindObjectOfType<UIManager>().UpdateHealthUI(health);
    }

    void UpdateCoinsUI()
    {
        // Atualiza o numero de moedas na interface.
        FindObjectOfType<UIManager>().UpdateCoins();
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("Enemy") && !tookDamage) {
            // Chamando a CoRotina do dano do player.
            StartCoroutine(TookDamage());
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Enemy") && !tookDamage) {
            StartCoroutine(TookDamage());
        }else if(collision.gameObject.CompareTag("Coin")) {
            Destroy(collision.gameObject); // Destroi a moeda.
            gameManager.coins += 1; // Adiciona uma moeda.
            UpdateCoinsUI();
        }
    }
}
