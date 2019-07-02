using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>(); // проверка на юнита

        if (unit && unit is Move)
        {
            unit.ReceiveDamage();
        }
    } // проверка на столкновение героя с препядствиями
}
