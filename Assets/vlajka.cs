using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class vlajka : MonoBehaviour
{
    public string scéna;
    public Sprite texturEN;
    public Sprite texturDIS;
    public float timeToTransition;
    float transTime;
    public int transtype=-1; //0=nothing, 1=fadein, -1=fadeout
    SpriteRenderer fadeobject;
    string loadscene;
    public int početlamp=0;
    public int preflevels;
    public GameObject sketchobj;
    public List<string> cutscenetexts;
    public List<float> cutscenetimes;
    void OnTriggerEnter2D(Collider2D other) {
        if(početlamp<=0&&other.GetComponentInParent<controller>().tag=="Player"){
            if(PlayerPrefs.GetInt("Checkpoint")==0){
                if(GameObject.Find("Cutscened")!=null)Destroy(GameObject.Find("Cutscened"));
                if(PlayerPrefs.GetInt("Levels")<preflevels){
                    load();
                    PlayerPrefs.SetInt("Levels",preflevels);
                }else{
                    GameObject.Find("Pauza").GetComponent<zapauzuj>().pauzuj(true);
                    GameObject.Find("Win submenu").GetComponentInChildren<TMP_Text>().text+=GameObject.Find("Timer").GetComponent<časovač>().Finish();
                }
            }else{
                GameObject.FindGameObjectWithTag("Player").GetComponent<controller>().revcheckp();
            }
        }
    }
    public void notransload(string scene="Default"){
        GameObject sh=GameObject.Find("RunShowData");
        if(sh!=null)if(sh.GetComponent<showrunner>().running)sh.GetComponent<showrunner>().StopShow();
        if(scene=="Default"){
            SceneManager.LoadScene(scéna);
        }else if(scene=="Continue"){
            if(PlayerPrefs.GetInt("Levels")>=8)SceneManager.LoadScene("Testovací");
            else SceneManager.LoadScene(PlayerPrefs.GetInt("Levels")+2);
        }else{
            SceneManager.LoadScene(scene);
            //loadscena(scene);
        }
    }
    async void loadscena(string scene){
        Scene s=SceneManager.GetActiveScene();
        var o=SceneManager.LoadSceneAsync(scene,LoadSceneMode.Additive);
        while(!o.isDone&&transTime>=timeToTransition)await System.Threading.Tasks.Task.Yield();
        SceneManager.UnloadSceneAsync(s,UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }
    public void load(string scene="Default"){
        loadscene=scene;
        //notransload(scene);
        transtype=1;
    }
    void Start(){
        fadeobject=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<kamera>().fadeobject;
        fadeobject.gameObject.SetActive(true);
        fadeobject.enabled=true;
        fadeobject.color=new Color(0,0,0,1);
        transTime=timeToTransition;

        if(GameObject.Find("Cutscened")==null
        &&cutscenetexts.Count!=0
        &&(PlayerPrefs.GetInt("Levels")<preflevels||PlayerPrefs.GetInt("AlwaysCutscenes")==1)
        ){
            transtype=0;
            skeč o=Instantiate(sketchobj).GetComponentInChildren<skeč>();
            DontDestroyOnLoad(o.GetComponentInParent<Canvas>().gameObject);
            o.titles=cutscenetexts;
            o.sec=cutscenetimes;
            o.returnto=SceneManager.GetActiveScene().name;
            notransload("cutscene");
        }
        else{
            početlamp=GameObject.FindGameObjectsWithTag("unlitlantern").Length;
            if(početlamp>0){
                gameObject.GetComponent<SpriteRenderer>().sprite=texturDIS;
            }
            GameObject g=GameObject.Find("RunShowData");
            if(g!=null)g.GetComponent<showrunner>().StartShow();
        }
    }
    private void Update() {
        transTime+=transtype*Time.deltaTime;
        if(transTime>timeToTransition){
            notransload(loadscene);
        }else if(transTime<0){
            transtype=0;
            transTime=0;
            fadeobject.enabled=false;
        }else{
            fadeobject.enabled=true;
        }
        if(0<=transTime && transTime/timeToTransition<=1){
            fadeobject.color=new Color(0,0,0,transTime/timeToTransition);
        }else{
            fadeobject.color=new Color(0,0,0,1);
        }
        if(Input.GetKeyDown(KeyCode.Return)&&Input.GetKey(KeyCode.LeftShift)){
            SceneManager.LoadScene(scéna);
        }
    }
    void FixedUpdate(){
        if(početlamp<=0&&PlayerPrefs.GetInt("Checkpoint")==0)
            gameObject.GetComponent<SpriteRenderer>().sprite=texturEN;
    }
}