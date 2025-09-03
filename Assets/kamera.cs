using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class kamera : MonoBehaviour
{
    public Transform follow;
    public float size=13;
    Transform cam;
    public float offsetY;
    public float zrychlovac;
    bool iscam;
    public SpriteRenderer fadeobject;
    Rigidbody2D rb;
    void Start(){
        if(follow==null){
            follow=GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Transform>()[1];
        }
        cam=transform;
        iscam=cam.GetComponent<Camera>()!=null;
        rb=GetComponent<Rigidbody2D>();
        if(iscam)SetAntialiasing(PlayerPrefs.GetInt("Antialiasing"));
    }
    public void ChangeFollow(Transform trans){
        follow=trans;
    }
    public void ChangeSize(float a){
        size=a;
    }
    void Update(){
        if(rb.simulated){}//rb.AddForce((follow.position+Vector3.up*offsetY-cam.position).normalized*zrychlovac*Time.deltaTime*10,ForceMode2D.Force);
        else cam.transform.position=new Vector3((cam.position.x-follow.position.x)/Mathf.Pow(zrychlovac,Time.deltaTime)+follow.position.x,(cam.position.y-follow.position.y-offsetY)/Mathf.Pow(zrychlovac,Time.deltaTime)+follow.position.y+offsetY,cam.position.z);
        if(iscam)cam.GetComponent<Camera>().orthographicSize=(cam.GetComponent<Camera>().orthographicSize-size)/Mathf.Pow(zrychlovac,Time.deltaTime)+size;
    }
    void FixedUpdate(){
        if(rb.simulated)rb.AddForce((follow.position+Vector3.up*offsetY-cam.position).normalized*zrychlovac*Time.fixedDeltaTime*10,ForceMode2D.Force);
    }
    public void SetAntialiasing(int mode){
        UniversalAdditionalCameraData uacm=GetComponent<UniversalAdditionalCameraData>();
        if(mode==0){
            uacm.antialiasing=AntialiasingMode.None;
        }
        if(mode==1){
            uacm.antialiasing=AntialiasingMode.SubpixelMorphologicalAntiAliasing;
            uacm.antialiasingQuality=AntialiasingQuality.Low;
        }
        if(mode==2){
            uacm.antialiasing=AntialiasingMode.SubpixelMorphologicalAntiAliasing;
            uacm.antialiasingQuality=AntialiasingQuality.Medium;
        }
        if(mode==3){
            uacm.antialiasing=AntialiasingMode.SubpixelMorphologicalAntiAliasing;
            uacm.antialiasingQuality=AntialiasingQuality.High;
        }
    }
}
