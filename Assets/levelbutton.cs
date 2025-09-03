using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class levelbutton : MonoBehaviour
{
    public int levelnumber;
    public GameObject scoretxt;
    public GameObject buttleader;
    bool holding=false;
    float haveholdfor=0;
    bool stopload=false;
    void Start(){
        if(PlayerPrefs.GetInt("Levels")<levelnumber){
            gameObject.SetActive(false);
        }
        if(scoretxt!=null)
        foreach(var i in PlayerPrefs.GetString("NewScores").Split(";")){
            if(i.Split(":")[0]=="lvl"+levelnumber){
                GameObject txt=GameObject.Instantiate<GameObject>(scoretxt,transform.position+new Vector3(0,1,-3),transform.rotation,transform);
                txt.GetComponent<TMP_Text>().text+=SecToMS(i.Split(":")[2].Length/3f/50f);
                txt.transform.localScale=new Vector3(1,1,1);
                GameObject leadb=GameObject.Instantiate<GameObject>(buttleader,transform.position+new Vector3(2.5f,0,-3),transform.rotation,transform);
                leadb.GetComponent<butttop>().mylvl="lvl"+levelnumber;
                break;
            }
        }
    }

    public void Click(bool a){
        holding=a;
        if(!a)haveholdfor=0;
    }

    void Update(){
        if(holding){
            haveholdfor+=Time.deltaTime;
        }
        if(haveholdfor>0.5)GetComponentInChildren<butttop>().Gotolead();
        if(stopload&&GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>().transtype==1)GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>().transtype=-1;
        if(Input.GetAxis("Leaderboard")>0.5&&GameObject.Find("EventSystem").GetComponent<EventSystem>().currentSelectedGameObject==gameObject)GetComponentInChildren<butttop>().Gotolead();
    }

    string SecToMS(float sec){
        string ret="";
        if(Mathf.Floor(sec/3600)!=0)ret+=Mathf.Floor(sec/3600)+"hrs ";
        if(Mathf.Floor(sec/60)!=0)ret+=Mathf.Floor(sec/60)+"min ";
        if(sec!=0)ret+=Mathf.Floor((sec%60)*100)/100+"sec";
        return ret;
    }
}
