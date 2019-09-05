using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject target;

    [Header("Attributes")]
    public float health = 20f;
    public float range = 15f;
    public float rotateSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public Transform firepoint;
    public Transform partToRotate;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget () {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance < range){
            target = nearestEnemy;
        } else {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);

        if (fireCountdown <= 0f){
            Shoot();
            fireCountdown = 1f/fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot(){
        GameObject bulletob = (GameObject) Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Bullet bullet = bulletob.GetComponent<Bullet>();
        if (bullet != null) {
            bullet.Seek(target);
        }
        
    }

    /*public void TakeDamage(float damage){
        health -= damage;
        if (health < 0){
            Destroy(gameObject);
        }
    }*/

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
