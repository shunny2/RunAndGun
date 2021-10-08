using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("Player")) {
            // Procura pelo objeto boss e chama a função ativar boss.
            FindObjectOfType<Boss>().ActivateBoss();
            // Trava a posição min da camera no eixo X.
            FindObjectOfType<UnityStandardAssets._2D.CameraFollow>().minXAndY = new Vector2(117.5f, 0);
            gameObject.SetActive(false); // Desativa o gatilho(parede) pro boss.
        }
    }
}
