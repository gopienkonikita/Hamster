using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_prologue : MonoBehaviour {

    [SerializeField] private int level = 0;

    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(level);
        }
    } // переход между прологами

}
