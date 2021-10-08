using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text bulletsText;
    public Text coinsText;
    public Text bombsText;
    public Text healthText;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
        UpdateCoins();
    }

    public void UpdateBulletsUI(int bullets)
    {
        bulletsText.text = bullets.ToString();
    }

    public void UpdateHealthUI(int health)
    {
        healthText.text = health.ToString();
        healthBar.value = health; // Atualizando a barra de vida.
    }

    public void UpdateCoins()
    {
        coinsText.text = GameManager.gameManager.coins.ToString();
    }

    public void UpdateBombs(int bombs)
    {
        bombsText.text = bombs.ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = GameManager.gameManager.health;
    }
}
