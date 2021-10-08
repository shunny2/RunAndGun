using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberfly : Enemy // Herdando da classe Enemy.
{

    void Start()
    {
        
    }

    protected override void Update() // Sobreescrevendo o método Update.
    {
        base.Update();  // Chamando a função Update do pai. (Enemy)

        if(Mathf.Abs(targetDistance) < attackDistance) { // Transformando a distancia em um valor absoluto para que o Bomb siga corretamente.
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime); // Função MoveTowards pega a posição atual e faz o movimento ate o alvo.
        }
    }
}
