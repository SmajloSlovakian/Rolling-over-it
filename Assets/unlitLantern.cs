using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlitLantern : MonoBehaviour
{
    public GameObject lantern;
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponentInParent<controller>().tag=="Player"){
            GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>().početlamp--;
            Instantiate(lantern,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
