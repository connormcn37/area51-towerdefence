using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 30f;
    private GameObject target;
    public float speed = 70f;
    public GameObject impactEffect;
    public void Seek (GameObject _target){
        target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null){
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget(){
        Enemy e = target.GetComponent<Enemy>();
        e.TakeDamage(damage);
        GameObject effect = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect,2f);
        Destroy(gameObject);
    }
}
