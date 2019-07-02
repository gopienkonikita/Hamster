using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Record : MonoBehaviour {


    public Text recordText;
    public Text recordText1;
    public Text recordText2;
    public Text recordText3;
    public Text recordText4;
    public Text recordText5;
    public Text recordText6;
    public Text recordText7;
    public Text recordText8;
    public Text recordText9;

    // Use this for initialization
    void Start ()
    {
        Input_text it = GetComponent<Input_text>();
        it.Load();
        #region
        recordText = GameObject.FindGameObjectWithTag("record").GetComponent<Text>();
        recordText1 = GameObject.FindGameObjectWithTag("record1").GetComponent<Text>();
        recordText2 = GameObject.FindGameObjectWithTag("record2").GetComponent<Text>();
        recordText3 = GameObject.FindGameObjectWithTag("record3").GetComponent<Text>();
        recordText4 = GameObject.FindGameObjectWithTag("record4").GetComponent<Text>();
        recordText5 = GameObject.FindGameObjectWithTag("record5").GetComponent<Text>();
        recordText6 = GameObject.FindGameObjectWithTag("record6").GetComponent<Text>();
        recordText7 = GameObject.FindGameObjectWithTag("record7").GetComponent<Text>();
        recordText8 = GameObject.FindGameObjectWithTag("record8").GetComponent<Text>();
        recordText9 = GameObject.FindGameObjectWithTag("record9").GetComponent<Text>();
        #endregion

        #region
        recordText.text = string.Format("1. {0} | {1}", Input_text.ListRecords[0].name, Input_text.ListRecords[0].score);
        recordText1.text = string.Format("2. {0} | {1}", Input_text.ListRecords[1].name, Input_text.ListRecords[1].score);
        recordText2.text = string.Format("3. {0} | {1}", Input_text.ListRecords[2].name, Input_text.ListRecords[2].score);
        recordText3.text = string.Format("4. {0} | {1}", Input_text.ListRecords[3].name, Input_text.ListRecords[3].score);
        recordText4.text = string.Format("5. {0} | {1}", Input_text.ListRecords[4].name, Input_text.ListRecords[4].score);
        recordText5.text = string.Format("6. {0} | {1}", Input_text.ListRecords[5].name, Input_text.ListRecords[5].score);
        recordText6.text = string.Format("7. {0} | {1}", Input_text.ListRecords[6].name, Input_text.ListRecords[6].score);
        recordText7.text = string.Format("8. {0} | {1}", Input_text.ListRecords[7].name, Input_text.ListRecords[7].score);
        recordText8.text = string.Format("9. {0} | {1}", Input_text.ListRecords[8].name, Input_text.ListRecords[8].score);
        recordText9.text = string.Format("10. {0} | {1}", Input_text.ListRecords[9].name, Input_text.ListRecords[9].score);
        #endregion
    }
}
