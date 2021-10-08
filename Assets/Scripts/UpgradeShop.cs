using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    public Text healthText;
    public Text damageText;
    public Text fireRateText;
    public Text bulletsText;
    public Text reloadTimeText;
    public Text upgradeCostText;

    GameManager gameManager;
    Player player;

    void Start()
    {
        gameManager = GameManager.gameManager;
        player = FindObjectOfType<Player>();
        UpdateUI();
    }

    void UpdateUI()
    {
        // Atualiza todos os textos que estão na tela e os valores do GameManager.
        healthText.text = "Health: " + gameManager.health;
        damageText.text = "Damage: " + gameManager.damage;
        fireRateText.text = "Fire Rate: " + gameManager.fireRate;
        bulletsText.text = "Bullets: " + gameManager.bullets;
        reloadTimeText.text = "Reload Time: " + gameManager.realoadTime;
        upgradeCostText.text = "Upgrade Cost: " + gameManager.upgradeCost;
    }

    void SetCoins(int coin)
    {
        gameManager.coins -= coin;
        FindObjectOfType<UIManager>().UpdateCoins();
    }

    public void SetHealth()
    {
        // Verifica se as moedas são maior ou igual o custo do upgrade.
        if(gameManager.coins >= gameManager.upgradeCost) {
            gameManager.health++;
            FindObjectOfType<UIManager>().UpdateHealthBar(); // Atualiza a barra de vida na tela.
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5); // Aumenta o custo do upgrade em 20%.
            UpdateUI();
        }
    }

    public void SetDamage()
    {
        // Verifica se as moedas são maior ou igual o custo do upgrade.
        if(gameManager.coins >= gameManager.upgradeCost) {
            gameManager.damage++;
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();
        }
    }

    public void SetFireRate()
    {
        // Verifica se as moedas são maior ou igual o custo do upgrade.
        if(gameManager.coins >= gameManager.upgradeCost) {
            gameManager.fireRate -= 0.1f;

            if(gameManager.fireRate <= 0) {
                gameManager.fireRate = 0;
            }

            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();
        }
    }

    public void SetBullets()
    {
        // Verifica se as moedas são maior ou igual o custo do upgrade.
        if(gameManager.coins >= gameManager.upgradeCost) {
            gameManager.bullets++;
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();
        }
    }

    public void SetReloadTime()
    {
        // Verifica se as moedas são maior ou igual o custo do upgrade.
        if(gameManager.coins >= gameManager.upgradeCost) {
            gameManager.realoadTime -= 0.1f;

            if(gameManager.realoadTime <= 0) {
                gameManager.realoadTime = 0;
            }

            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();
        }
    }
}
