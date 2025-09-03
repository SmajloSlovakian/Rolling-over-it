using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class continuebutt : MonoBehaviour
{
	public void Click(){
        GameObject.FindGameObjectWithTag("vlajka").GetComponent<vlajka>().load();
    }
}
