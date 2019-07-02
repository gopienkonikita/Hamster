using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_pause : MonoBehaviour
{
    public GameObject panel;
    public GameObject text;
    public GameObject player;
    private bool pause = false;
    public static bool load_level = false;


    public Text coinText;

    private float timer;

    [System.Serializable]
    public class Saver
    {
        public int lvl;
        public float x;
        public float y;
        public float z;
        public int health;
        public int coins;
        public int bonus_jump;
    } // серелизуемый класс для сохранения локации

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // проверка на нажатие клавиши ESC
        {
            if (!pause) // проверка на нахождение в паузе
            {
                pause = true;
                panel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pause = false;
                panel.SetActive(false);
                Time.timeScale = 1;
            }
        }
        timer += 1 * Time.deltaTime;
        if (timer > 40) // проверка времени таймера
        {
            text.SetActive(true);
        }
        if (timer > 60)
        {
            text.SetActive(false);
        }
    } // проверка на нажатие клавиш и вывод сообщений

    public void Save() // сохранение уровля в файл
    {
        Saver saves = new Saver();
        Move move = player.GetComponent<Move>();

        saves.x = player.transform.position.x;
        saves.y = player.transform.position.y;
        saves.z = player.transform.position.z;

        //saves.health =  Move.hp;
        saves.health = move.live;
        saves.coins = move.coin;
        saves.bonus_jump = move.bonus;

        saves.lvl = move.save;

        if (!Directory.Exists(Application.dataPath + "/saves")) // проверка на директирию
            Directory.CreateDirectory(Application.dataPath + "/saves");
        FileStream fs = new FileStream(Application.dataPath + "/saves/save.sv", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, saves);
        fs.Close();
    }


    public void Load_lvl()
    {
        
        if (File.Exists(Application.dataPath + "/saves/save.sv")) // проверка на присутствие файла
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/save.sv", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                Saver saves = (Saver)formatter.Deserialize(fs);

                if(saves.lvl ==1) // проверка на значение переменной для правильной загрузки уровня
                {
                    load_level = true;
                    SceneManager.LoadScene(4);
                }
                if (saves.lvl == 2)
                {
                    load_level = true;
                    SceneManager.LoadScene(5);
                }
                if (saves.lvl == 3)
                {
                    load_level = true;
                    SceneManager.LoadScene(6);
                }
                if (saves.lvl == 4)
                {
                    load_level = true;
                    SceneManager.LoadScene(7);
                }
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

    public void Exit()
    {
        Application.Quit();
    } // выход из приложения
}
