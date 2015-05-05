using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System.Collections.Generic;
public class SessionsActivity : MonoBehaviour {

    LitJson.JsonData sessionData;

    public Sessions sessions;

    public UIInput 游戏开始手牌数量;
    public UIInput 发牌数;
    public UIInput 出牌数量最大值;
    public UIInput 出牌数量最小值;
    public UIToggle 牌堆2是否激活;
    public UIToggle 混合牌堆1跟2;
    public UIToggle 循环发牌;

    bool init = false;
	// Use this for initialization
	void Start () {

        try
        {
            FileStream stream = File.Open(System.IO.Path.Combine(Application.streamingAssetsPath, "session.txt"), FileMode.Open, FileAccess.Read);


            using (StreamReader sr = new StreamReader(stream))
            {
                string sessionJson = sr.ReadToEnd();
                if (string.IsNullOrEmpty(sessionJson))
                {
                    sessionJson = JsonMapper.ToJson(new Sessions());
                }
                sessionData = LitJson.JsonMapper.ToObject(sessionJson);

                sessions = JsonMapper.ToObject<Sessions>(sessionData.ToJson());

                游戏开始手牌数量.value = sessions.游戏开始手牌数量.ToString();
                发牌数.value = sessions.发牌数.ToString();
                出牌数量最大值.value = sessions.出牌数量最大值.ToString();
                出牌数量最小值.value = sessions.出牌数量最小值.ToString();
                牌堆2是否激活.value = sessions.牌堆2是否激活;
                混合牌堆1跟2.value = sessions.混合牌堆1跟2;
                循环发牌.value = sessions.循环发牌;
            }
            init = true;
        }
        catch (System.Exception ex)
        {

            Debug.Log(ex);


            sessionData = JsonMapper.ToObject(JsonMapper.ToJson(sessions));

            Save();

        }
       

       

	}
    public void OnChange游戏开始手牌数量(string field)
    {
        Debug.Log("OnChange游戏开始手牌数量" + field);
        sessions.游戏开始手牌数量 = int.Parse(field);
        Save();
    }
    public void OnChange发牌数(string field)
    {
        Debug.Log("OnChange发牌数" + field);
        sessions.发牌数 = int.Parse(field);
        Save();
    }
    public void OnChange出牌数量最大值(string field)
    {
        Debug.Log("OnChange出牌数量最大值" + field);
        sessions.出牌数量最大值 = int.Parse(field);
        Save();
    }
    public void OnChange出牌数量最小值(string field)
    {
        Debug.Log("OnChange出牌数量最小值" + field);
        sessions.出牌数量最小值 = int.Parse(field);
        Save();
    }
    public void OnChange牌堆2是否激活(bool field)
    {
        Debug.Log("OnChange牌堆2是否激活" + field);
        sessions.牌堆2是否激活 = field;
        Save();
    }
    public void OnChange混合牌堆1跟2(bool field)
    {
        Debug.Log("OnChange混合牌堆1跟2" + field);
        sessions.混合牌堆1跟2 = field;
        Save();
    }
    public void OnChange循环发牌(bool field)
    {
        Debug.Log("OnChange循环发牌" + field);
        sessions.循环发牌 = field;
        Save();
    }
    void Save()
    {
        //var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        if (init)
        {
            FileInfo fi = new FileInfo(System.IO.Path.Combine(Application.streamingAssetsPath, "session.txt"));
            FileStream sessionFile = fi.Open(FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(sessionFile))
            {
                sw.WriteLine(sessionData.ToJson());
            }
            sessionFile.Close();
        }
        
    }
    public LitJson.JsonData GetSessionJsonData()
    {
        return sessionData;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
