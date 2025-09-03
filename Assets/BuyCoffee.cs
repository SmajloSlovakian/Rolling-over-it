using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCoffee : MonoBehaviour
{
    public void BuyIt(){
        Application.OpenURL("https://smajlo.itch.io/donate/");
    }
}
