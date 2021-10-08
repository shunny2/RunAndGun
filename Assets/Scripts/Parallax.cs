using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform target;
    public float paralaxSpeed; // Velocidade que vai seguir o alvo(camera).

    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = target.position; // Pega a posição atual do alvo(camera).
    }

    // Update is called once per frame
    void Update()
    {
        // Faz o background seguir a camera.
        transform.Translate((target.position.x - previousPosition.x) / paralaxSpeed, (target.position.y - previousPosition.y) / paralaxSpeed, 0);
        previousPosition = target.position; // Atualiza a posição anterior do alvo.
    }
}
