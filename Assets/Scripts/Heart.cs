using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) // проверка на столкновение героя с сердцем
    {
        Move move = collider.GetComponent<Move>();
        if (move) // проверка на главного героя
        {
            move.Lives++;
            Move.hp = move.Lives;
            move.source.clip = move.clips[1];
            move.source.Play();
            Destroy(gameObject);
        }
    }
}
