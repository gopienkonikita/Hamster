using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour {

    private Transform[] hearts = new Transform[5];

    private Move move;


    private void Awake() // запуск при движении объекта
    {
        move = FindObjectOfType<Move>();

        for (int i = 0; i < hearts.Length; i++) // запуск цикла по массиву сердец
        {
            hearts[i] = transform.GetChild(i);
            //Debug.Log(hearts[i]);
        }
    }

    public void Refresh() // добавляет сердца герою если их не больше 5
    {
        for (int i = 0; i < hearts.Length; i++) // цикл по массиву сердец
        {
            if (i < move.Lives) hearts[i].gameObject.SetActive(true); // проверка на коллисество сердец
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
