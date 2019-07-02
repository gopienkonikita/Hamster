using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public virtual void ReceiveDamage()
    {
        Die();
    } // уничтожаем объект

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
