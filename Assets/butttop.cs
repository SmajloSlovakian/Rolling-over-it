using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class butttop : MonoBehaviour
{
    public string mylvl;
    public void Gotolead(){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<kamera>().follow=GameObject.Find("LeaderboardPos").GetComponent<Transform>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<kamera>().size=10;
        GameObject.Find("LeadConstructOnline").GetComponent<ConstructLeader>().Construct(mylvl);
        GameObject.Find("LeadConstructLocal").GetComponent<ConstructLeader>().Deconstruct();
        GameObject.Find("LeadConstructLocal").GetComponent<ConstructLeader>().ConstructWithData(PlayerPrefs.GetString("NewScores"));
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("Butthome"));
    }
}
