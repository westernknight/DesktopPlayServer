using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CardsInfoActivity : MonoBehaviour {

   
    public UIScrollView scrollView;
    public UIScrollView scrollView2;
    public GameObject cardPrefab;
    [HideInInspector]
    public List<UIToggle> pile1State = new List<UIToggle>();
	// Use this for initialization
	void Start () {

        FileStream stream = File.Open(System.IO.Path.Combine(Application.streamingAssetsPath, "a.xml"), FileMode.Open, FileAccess.Read);

        CardsConfig cc = new CardsConfig(stream);
        stream.Close();

        List<string> pile1 = cc.GetPile(0);
        for (int i = 0; i < pile1.Count; i++)
        {
            GameObject go = GameObject.Instantiate(cardPrefab) as GameObject;

            go.transform.parent = scrollView.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.localPosition = new Vector3(160, 135-i*35, 0);
            UIToggle[] toggle = go.GetComponentsInChildren<UIToggle>();
            UILabel[] label = go.GetComponentsInChildren<UILabel>();
            label[0].text = pile1[i];
            pile1State.Add(toggle[0]);
        }
        List<string> pile2 = cc.GetPile(1);
        for (int i = 0; i < pile2.Count; i++)
        {
            GameObject go = GameObject.Instantiate(cardPrefab) as GameObject;

            go.transform.parent = scrollView2.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.localPosition = new Vector3(160, 135 - i * 35, 0);
            UIToggle[] toggle = go.GetComponentsInChildren<UIToggle>();
            UILabel[] label = go.GetComponentsInChildren<UILabel>();
            label[0].text = pile2[i];
            pile1State.Add(toggle[0]);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
