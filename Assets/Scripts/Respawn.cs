using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public GameObject respawn;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Move move = collider.GetComponent<Move>();

        if (collider.tag == "Player") // проверка тега объекта 
        {
            collider.transform.position = respawn.transform.position;
            move.Lives--;
            move.source.clip = move.clips[3];
            move.source.Play();
        }
    } // задание точки возрождения
}
