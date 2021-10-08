using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShop : MonoBehaviour
{
    public GameObject shopPanel;

    void OnTriggerEnter2D(Collider2D collider) 
    {
        Player player = collider.GetComponent<Player>();

        if(player != null) {
            player.canFire = false;
            shopPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        Player player = collider.GetComponent<Player>();
        
        if(player != null) {
            player.canFire = true;
            shopPanel.SetActive(false);
        }
    }
}
