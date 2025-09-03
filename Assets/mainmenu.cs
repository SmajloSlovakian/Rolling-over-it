using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Reflection;
using System;

public class mainmenu : MonoBehaviour
{
    Slider frameslide;
    void Awake()
    {
        if(PlayerPrefs.GetInt("FirstLaunch")<4){
            PlayerPrefs.SetInt("Levels",-1);
        }
        if(PlayerPrefs.GetInt("FirstLaunch")<5){
            PlayerPrefs.SetInt("Antialiasing",3);
        }
        if(PlayerPrefs.GetInt("FirstLaunch")<6){
            PlayerPrefs.SetInt("Resolution",0);
            PlayerPrefs.SetInt("Fullscreen",1);
            PlayerPrefs.SetInt("Refreshrate",0);
            if(Application.platform==RuntimePlatform.Android)PlayerPrefs.SetInt("TouchControls",1);
            PlayerPrefs.SetInt("Rumble",1);
        }
        if(PlayerPrefs.GetInt("FirstLaunch")<7){
            PlayerPrefs.SetInt("Checkpoint",0);
        }
        if(PlayerPrefs.GetInt("FirstLaunch")<8){
            //PlayerPrefs.SetString("Scores","");
        }
        if(PlayerPrefs.GetInt("FirstLaunch")<9){
            PlayerPrefs.SetString("Nickname","");
        }
        if(PlayerPrefs.GetInt("FirstLaunch")<11){
            PlayerPrefs.SetString("NewScores","");
        }
        if(PlayerPrefs.GetInt("FirstLaunch")<12){
            PlayerPrefs.SetInt("AlwaysCutscenes",0);
        }
        PlayerPrefs.SetInt("FirstLaunch",12);
        GameObject.Find("Antialidrop").GetComponent<TMP_Dropdown>().value=PlayerPrefs.GetInt("Antialiasing");
        GameObject.Find("Fulltick").GetComponent<Toggle>().isOn=PlayerPrefs.GetInt("Fullscreen")==1;
        GameObject.Find("Touchtick").GetComponent<Toggle>().isOn=PlayerPrefs.GetInt("TouchControls")==1;
        GameObject.Find("Rumbletick").GetComponent<Toggle>().isOn=PlayerPrefs.GetInt("Rumble")==1;
        GameObject.Find("Chpointick").GetComponent<Toggle>().isOn=PlayerPrefs.GetInt("Checkpoint")==1;
        GameObject.Find("Cutick").GetComponent<Toggle>().isOn=PlayerPrefs.GetInt("AlwaysCutscenes")==1;
        GameObject.Find("Nickentry").GetComponent<TMP_InputField>().text=PlayerPrefs.GetString("Nickname");
        Destroy(GameObject.Find("Cutscened"));
        TMP_Dropdown resdrop=GameObject.Find("Resdrop").GetComponent<TMP_Dropdown>();
        frameslide=GameObject.Find("Slideframe").GetComponent<Slider>();

        List<string> li= new List<string>();
        foreach (Resolution i in Screen.resolutions){
             li.Add("Resolution: "+i.width+"x"+i.height);}
        li.Reverse();
        resdrop.AddOptions(li);
        resdrop.value=PlayerPrefs.GetInt("Resolution");

        frameslide.maxValue=((float)Screen.currentResolution.refreshRateRatio.value);
        frameslide.value=PlayerPrefs.GetInt("Refreshrate");
        if(PlayerPrefs.GetString("Nickname")!="")UploadData();
        /*//if(PlayerPrefs.GetString("Nickname")!="")UploadData();
        //GUIUtility.systemCopyBuffer=PlayerPrefs.GetString("Scores");
        PlayerPrefs.SetString("Scores",GUIUtility.systemCopyBuffer);
        print(PlayerPrefs.GetString("Scores"));*/
    }
    /*void Update(){
        if(Input.GetKeyDown(KeyCode.LeftControl))PlayerPrefs.SetString("Scores",GUIUtility.systemCopyBuffer);
        if(Input.GetKeyDown(KeyCode.LeftShift))UploadData();
    }*/
    public async void UploadData(){
        string save="";
        foreach(string i in PlayerPrefs.GetString("NewScores").Split(";")){
            if(i.Length==0)break;
            string[] o=i.Split(":");
            if(save.Length>0)save+=";";
            if(o.Length>=4){
                save+=i;
            }else{
                print(o.Length);
                WWWForm form=new WWWForm();
                form.AddField("entry.1665291068",i+":"+PlayerPrefs.GetString("Nickname"));
                UnityWebRequest w=UnityWebRequest.Post("https://docs.google.com/forms/u/0/d/e/1FAIpQLSdOkN51RnScAQ4CsIC9AJL3s6MteAijkdI2d53Vo2wp55AiIw/formResponse",form); //https://docs.google.com/forms/u/0/d/e/1FAIpQLScCFHEG-qVqiqdbKpdh_ZWbS1cgbs7pd2pOIaOIlZtKYbJEXg/formResponse
                w.SendWebRequest();
                while(w.result==UnityWebRequest.Result.InProgress){await System.Threading.Tasks.Task.Yield();}
                if(w.result==UnityWebRequest.Result.Success)save+=i+":"+PlayerPrefs.GetString("Nickname");
                else save+=i;
                print(w.result+" "+w.responseCode);
            }
            await System.Threading.Tasks.Task.Yield();
        }
        PlayerPrefs.SetString("NewScores",save);
    }
    public void close(){
        Application.Quit();
    }
    public void removeuserdata(){
        PlayerPrefs.SetInt("Levels",-1);
        PlayerPrefs.SetString("NewScores","");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void changeAA(int a){
        PlayerPrefs.SetInt("Antialiasing",a);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<kamera>().SetAntialiasing(a);
    }
    public void changeRES(int a){
        PlayerPrefs.SetInt("Resolution",a);
        if(a==0){
            Screen.SetResolution(Screen.currentResolution.width,Screen.currentResolution.height,Screen.fullScreen);
        }else{
            a=Screen.resolutions.Length-a;
            Screen.SetResolution(Screen.resolutions[a].width,Screen.resolutions[a].height,Screen.fullScreen);
        }
    }
    public void changeFUL(bool a){
        Screen.fullScreen=a;
        if(a)PlayerPrefs.SetInt("Fullscreen",1);
        else PlayerPrefs.SetInt("Fullscreen",0);
    }
    public void changeTCH(bool a){
        if(a)PlayerPrefs.SetInt("TouchControls",1);
        else PlayerPrefs.SetInt("TouchControls",0);
    }
    public void changeRUM(bool a){
        if(a)PlayerPrefs.SetInt("Rumble",1);
        else PlayerPrefs.SetInt("Rumble",0);
    }
    public void changeFRA(float a){
        if(a==0){
            Application.targetFrameRate=-1;
            frameslide.GetComponentInChildren<TMP_Text>().text="FPS: Auto";
        }else{
            Application.targetFrameRate=((int)a);
            frameslide.GetComponentInChildren<TMP_Text>().text="FPS: "+a;
        }
        PlayerPrefs.SetInt("Refreshrate",(int)a);
    }
    public void changeCHE(bool a){
        if(a)PlayerPrefs.SetInt("Checkpoint",1);
        else PlayerPrefs.SetInt("Checkpoint",0);
    }
    public void changeNICK(string a){
        if(a.LastIndexOf(";")>=0||a.LastIndexOf(":")>=0){
            print("NIENIENIENI");
            GameObject.Find("Nickentry").GetComponent<TMP_InputField>().text="";
            return;
        }
        PlayerPrefs.SetString("Nickname",a);
        if(a!="")UploadData();
    }
    public void changeMETR(Transform follow){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<kamera>().ChangeFollow(follow);
    }
    public void changeMESE(GameObject select){
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(select);
    }
    public void changeMESI(float kamsize){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<kamera>().ChangeSize(kamsize);
    }
    public void changeCUT(bool a){
        if(a)PlayerPrefs.SetInt("AlwaysCutscenes",1);
        else PlayerPrefs.SetInt("AlwaysCutscenes",0);
    }
}