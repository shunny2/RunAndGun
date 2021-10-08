using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int health;
    public int bombs;

    void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>(); // Pega o componente do player.

        if(player != null) {
            player.SetHealthAndBombs(health, bombs);
            Destroy(gameObject);
        }
    }
}
