using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reload : MonoBehaviour
{
    vlajka vlaj;
    void Start(){
        vlaj=GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>();
    }
    void Update(){
        if(Input.GetAxis("Reset")>0.5)vlaj.notransload(SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponentInParent<controller>().tag=="Player")
        if(PlayerPrefs.GetInt("Checkpoint")==0){
            vlaj.load(SceneManager.GetActiveScene().name);
        }else{
            GameObject.FindGameObjectWithTag("Player").GetComponent<controller>().revcheckp();
        }
    }
}
