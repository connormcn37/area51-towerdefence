using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = (GameObject[]) GameObject.FindGameObjectsWithTag("enemy"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDestinations(Vector3 dest){
        Debug.Log("set dest " +dest);
        foreach(GameObject e in enemies){
            Enemy agent = e.GetComponent<Enemy>();
            agent.PushDestination(dest);
        }
    }
}
