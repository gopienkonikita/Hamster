using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) // проверка на столкновение бонусом
    {
        Move move = collider.GetComponent<Move>();
        if (move) // проверка на то что это герой
        {
            move.Bonus++;
            Destroy(gameObject);
        }
    }
}
