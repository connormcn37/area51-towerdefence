using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public float health = 20f;
    public float speed = 5f;
    public float attackRadius = 2f;
    public float attackSpeed = 2f;
    public float attackDamage = 5f;
    private float attackTime = 0f;
    public GameObject area51;
    [SerializeField]
    public Stack<Vector3> dests;

    public NavMeshAgent agent;
    public bool errored = false;
    public float errorCooldown = 2;
    public float timeLeft = 0f;
    public GameObject target = null;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        area51 = GameObject.FindGameObjectWithTag("Base");
        dests = new Stack<Vector3>();
        PushDestination(area51.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, area51.transform.position) < 3){
            //reached the target
            Debug.Log("Reached area51");
            Destroy(gameObject);
            return;
        }
        if (target){
            AttackTarget();
        } else if (agent.pathStatus != NavMeshPathStatus.PathComplete){
            //have an incomplete/unreachable path, but no target
            GetTarget();
            if (!target){
                Debug.Log("GetTarget failed"); 
                if(agent.destination != dests.Peek()){
                    agent.SetDestination(dests.Peek());
                }
            }
        } else if (
            dests.Count > 1
            && Vector3.Distance(transform.position, dests.Peek()) < 2
        ){
            //no target, at destination, but we have queued places to be
            dests.Pop();
            agent.SetDestination(dests.Peek());
        } else if (
            dests.Count > 0 
            && agent.destination != dests.Peek()
        ){
            agent.SetDestination(dests.Peek());
        }
    }
    public void AttackTarget(){
        if (attackTime > 1/attackSpeed){
            if (Vector3.Distance(transform.position, target.transform.position) < attackRadius){
                target.GetComponent<HealthComponent>().TakeDamage(attackDamage);
                attackTime = 0f;
                Debug.Log("attacking");
            } else {
                //transform.Translate(Vector3.Normalize(target.transform.position - transform.position) * speed * Time.deltaTime);
            }
        } else {
            attackTime += Time.deltaTime;
        }
    }
    public void GetTarget(){
        Debug.Log("GetTarget");

        GameObject[] list = GameObject.FindGameObjectsWithTag("Building");
        GameObject closest = null;
        float dist = Mathf.Infinity;
        foreach (GameObject g in list){
            Vector3 dirToTarget = (g.transform.position - transform.position);
            float distToTarget = dirToTarget.sqrMagnitude;
            if (distToTarget < dist){
                closest = g;
                dist = distToTarget;
            }
        }
        target = closest;
        dests.Push(target.transform.position);
        agent.SetDestination(target.transform.position);
    }

    public void TakeDamage(float damage){
        health -= damage;
        if (health < 0f){
            //should add +1 kill to game state
            GameObject.FindWithTag("Manager").GetComponent<GameManager>().killCount+=1;
            Destroy(gameObject);
            return;
        }
    }

    public void PushDestination(Vector3 destination){
        dests.Push(destination);
    }
}
