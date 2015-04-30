using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System.Collections.Generic;
public class SessionsActivity : MonoBehaviour {

    LitJson.JsonData sessionData;


    public GameObject switchBox;
    public GameObject fieldBox;
    public UIRoot root;
    List<GameObject> switchBoxList = new List<GameObject>();
    List<GameObject> fieldBoxList = new List<GameObject>();
    int elementCount = 0;
	// Use this for initialization
	void Start () {
        var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        FileInfo fi = new FileInfo(Path.Combine(documents, "sessions.txt"));
        FileStream sessionFile = fi.Open(FileMode.OpenOrCreate);
        using (StreamReader sr = new StreamReader(sessionFile))
        {
            string sessionJson = sr.ReadToEnd();
            if (string.IsNullOrEmpty(sessionJson))
            {
                sessionJson = JsonMapper.ToJson(new Sessions());
            }
            sessionData = LitJson.JsonMapper.ToObject(sessionJson);
        }

        sessionFile.Close();

        sessionFile = fi.Open(FileMode.OpenOrCreate);
        using (StreamWriter sw = new StreamWriter(sessionFile))
        {
            sw.WriteLine(sessionData.ToJson());
        }
        sessionFile.Close();

        foreach (string key in ((IDictionary)sessionData).Keys)
        {
            if (sessionData[key].IsInt)
            {
                CreateNumberEditSession(key, (int)sessionData[key]);
            }
            if (sessionData[key].IsBoolean)
            {
                CreateBoolSwitchSession(key, (bool)sessionData[key]);
            }
        }

	}
    void CreateNumberEditSession(string title, int defaultValue)
    {
        
        //to do add item
        GameObject field = GameObject.Instantiate(fieldBox) as GameObject;
        field.transform.parent = root.transform;
        field.transform.localScale = Vector3.one;
        fieldBoxList.Add(field);
        field.transform.localPosition = new Vector3(0, elementCount * -34 + 100, 0);

        field.transform.GetChild(0).gameObject.GetComponent<UIInput>().value = defaultValue.ToString();
        field.transform.GetChild(1).gameObject.GetComponent<UILabel>().text = title;
        //to do gui change
        /*et.TextChanged += (sender, e) =>
        {
            string change = "";
            foreach (var item in e.Text)
            {
                change += item;
            }
            if (change != "")
            {
                sessionData[title] = int.Parse(change);
            }
            else
            {
                sessionData[title] = 0;
            }
            Save();
        };*/

        elementCount++;
    }
    public void NumberEditSessionChanged()
    {
        
    }



    void CreateBoolSwitchSession(string title, bool defaultValue)
    {
 
        GameObject switchObject = GameObject.Instantiate(switchBox) as GameObject;
        switchObject.transform.parent = root.transform;
        switchObject.transform.localScale = Vector3.one;
        switchBoxList.Add(switchObject);
        switchObject.transform.localPosition = new Vector3(0, elementCount * -34 + 100, 0);

        switchObject.transform.GetChild(0).gameObject.GetComponent<UIToggle>().value = defaultValue;
        switchObject.transform.GetChild(1).gameObject.GetComponent<UILabel>().text = title;
        //         Switch sw = new Switch(this);
        //         sw.Checked = defaultValue;
        //         sw.Text = title;
        //         sw.CheckedChange += (sender, e) =>
        //         {
        //             sessionData[title] = e.IsChecked;
        //             Save();
        //         };
        //         principalview.AddView(sw);

        elementCount++;
    }
    void Save()
    {
        var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        FileInfo fi = new FileInfo(Path.Combine(documents, "sessions.txt"));
        FileStream sessionFile = fi.Open(FileMode.OpenOrCreate);
        using (StreamWriter sw = new StreamWriter(sessionFile))
        {
            sw.WriteLine(sessionData.ToJson());
        }
        sessionFile.Close();
    }
    public LitJson.JsonData GetSessionJsonData()
    {
        return sessionData;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
