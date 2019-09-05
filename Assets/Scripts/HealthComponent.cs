using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage){
        health-=damage;
        if (health <= 0){
            GameObject.Destroy(gameObject);
            return;
        }
    }
}
