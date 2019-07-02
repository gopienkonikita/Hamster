using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_level : MonoBehaviour {

    [SerializeField] private int level = 0;
    public GameObject lvl_1;
    public GameObject lvl_2;
    public GameObject lvl_3;
    public GameObject lvl_4;
    public GameObject text;

    public int next_lvl=0;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Save();
        Move move = collision.GetComponent<Move>();
        var player = GameObject.FindWithTag("dieBoss");

        if (move && player == null)
        {
            Move.mon = move.coin;
            Move.hp = move.live;
            SceneManager.LoadScene(level);
        }
    }

    public void Next()
    {
        Move.mon = 0;////////////////////////////////
        Move.hp = 5;
        SceneManager.LoadScene(level);
    } // переход к следующему уровню

    public void Exit()
    {
        Application.Quit();
    } // выход из приложения

    [System.Serializable]
    public class Saver
    {
        public int level;

    } // серелизуемый класс для сохранения локации

    public void Save() // сохранение уровля в файл
    {
        Saver saves = new Saver();

        saves.level = next_lvl;

        if (!Directory.Exists(Application.dataPath + "/saves"))
            Directory.CreateDirectory(Application.dataPath + "/saves");
        FileStream fs = new FileStream(Application.dataPath + "/saves/save_level.sv", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, saves);
        fs.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/saves/save_level.sv"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/save_level.sv", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                Saver saves = (Saver)formatter.Deserialize(fs);
                next_lvl = saves.level;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        else
        {
            //вывести сообщение от том что фала нет
            //Application.Quit();
        }
    }

    public void Choise()
    {
        text.SetActive(true);
        Load();
            lvl_1.SetActive(true);
        if (next_lvl >= 1)
            lvl_2.SetActive(true);
        if (next_lvl >= 2)
            lvl_3.SetActive(true);
        if (next_lvl >= 3)
            lvl_4.SetActive(true);
    } // функция отображения кнопок с уровнями в главном меню

}
