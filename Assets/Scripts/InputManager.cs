using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject placingPrefab;
    BuildManager bm;
    EnemyManager em;
    Dictionary<KeyCode, System.Action> keyToFunction = new Dictionary<KeyCode, System.Action>();
    public KeyCode build = KeyCode.B;
    
    void Start()
    {
        //register keys to functions
        bm = GetComponent<BuildManager>();
        keyToFunction.Add(build, bm.ToggleBuilding);
        em = GetComponent<EnemyManager>();
        keyToFunction.Add(KeyCode.Mouse1, RightClick);
    }

    // Update is called once per frame
    void Update()
    {
        //loop through registered keys, execute the function if pressed
        foreach ( KeyCode k in keyToFunction.Keys){
            if (Input.GetKeyDown(k)){
                keyToFunction[k]();
            }
        }
    }

    public void LeftClick(){
        

    }

    public void RightClick(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100)){
            em.setDestinations(hit.point);
        }
    }
}
