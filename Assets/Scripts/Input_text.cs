using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Input_text : MonoBehaviour
{
    public GameObject panel;
    public GameObject canvas;
    public InputField field;

    public string line;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //проверка на нажатие клавиши Space
        {
            panel.SetActive(true);
            canvas.SetActive(false);
        }
    } // вывод панели очков при нажатии клавиши space

    public void Load_Menu() // загрузка меню
    {
        Save();
        SceneManager.LoadScene("Menu");
    }
    
    public void EndEdit() // сохраняет вводимый текст если поле не активно
    {
        line = field.text;
        Debug.Log(line);

        ListRecords.Add(new Tabl_rec(line, Move.mon));

    }

    public static List<Tabl_rec> ListRecords = new List<Tabl_rec>();

    [Serializable]
    public struct Tabl_rec // создает структуру таблицы рекордов
    {
        public string name;
        public int score;


        public Tabl_rec(string name_1, int score_1)
        {
            name = name_1;
            score = score_1;
        }
    }

    public void Save()
    {
        if (!Directory.Exists(Application.dataPath + "/saves")) // проверка на существование дирректории
            Directory.CreateDirectory(Application.dataPath + "/saves");
        FileStream fs = new FileStream(Application.dataPath + "/saves/save_rec.sv", FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, ListRecords);
        Debug.Log("------Save------");
        foreach (var item in ListRecords)
            Debug.Log(string.Format("name = {0}, score = {1}", item.name, item.score));
        Debug.Log("------Save complete------");
        fs.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/saves/save_rec.sv")) // проверка на файл
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/save_rec.sv", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                ListRecords = (List<Tabl_rec>)formatter.Deserialize(fs);
                Debug.Log("------Load------");
                foreach (var item in ListRecords)
                    Debug.Log(string.Format("name = {0}, score = {1}", item.name, item.score));
                Debug.Log("------Load complete------");
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
            //Application.Quit();
        }
    }
}
