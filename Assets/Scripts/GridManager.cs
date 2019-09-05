using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float size = 1f;
    public int xmax = 40;
    public int zmax = 40;
    public GameObject[] objects;
    public Color highlightColor = new Color(200f,200f,0f);
    public Color defaultColor = new Color(100f,100f,100f);
    void Awake()
    {
        xmax = Mathf.FloorToInt(transform.localScale.x);
        zmax = Mathf.FloorToInt(transform.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetNearestPointOnGrid(Vector3 pos){
        pos -= transform.position;

        int xCount = Mathf.RoundToInt(pos.x / size);
        int yCount = Mathf.RoundToInt(pos.y / size);
        int zCount = Mathf.RoundToInt(pos.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    public void OnDrawGizmos(){
        
        for (int xpos = 0; xpos < xmax; xpos++)
        {
            for (int zpos = 0; zpos < zmax; zpos++)
            {
                Vector3 point = GetNearestPointOnGrid(new Vector3(xpos, 0f, zpos));
                Gizmos.DrawSphere(point,0.2f);
            }
        }
    }
}
