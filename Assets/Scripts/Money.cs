using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

    public Text moneyText;

    private void Start()
    {
        moneyText = GameObject.FindGameObjectWithTag("coins").GetComponent<Text>();
       // moneyText.text = "0";
    } // задание текста монет


    private void OnTriggerEnter2D(Collider2D collider) // подсчет монет 
    {
        Move move = collider.GetComponent<Move>();
        if (move) // проверка на героя
        {
            move.Coins++;
            Move.mon = move.Coins;
            moneyText.text = move.Coins.ToString();
            Destroy(gameObject);
        }
    }
}
