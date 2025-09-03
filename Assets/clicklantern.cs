using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clicklantern : MonoBehaviour
{
    public GameObject lantern;
    public void enlighten() {
        Instantiate(lantern,transform.position,transform.rotation);
        Destroy(gameObject);
        if(PlayerPrefs.GetInt("Levels")>=7){
            GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>().load("Testovací");
        }
    }
}
