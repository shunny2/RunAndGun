using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour // Game Manager é responsavel por manter os dados do jogo.
{
    public int health = 5;
    public int damage = 1;
    public float fireRate = 0.5f;
    public float realoadTime = 1f;
    public int bullets = 6;
    public int coins;
    public int bombs = 2;
    public int upgradeCost = 20;

    public static GameManager gameManager;

    void Awake()
    {
        if(gameManager == null) {
            gameManager = this; // Transforma o objeto atual no game manager.
        }else {
            Destroy(gameObject); // Destroi os objetos das cenas anteriores e mantem o primeiro.
        }

        DontDestroyOnLoad(gameObject); // Faz a persistencia entre as cenas.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
