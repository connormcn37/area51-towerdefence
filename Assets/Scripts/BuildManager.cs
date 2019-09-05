using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance = null;
    public GameObject standardTurretPrefab;
    public GameObject prefabToBuild;
    private GameObject buildPreview;
    private bool buildingNow = false;
    private float mouseWheelRotation = 0f;
    void Awake(){
        if (instance != null){
            Debug.LogError("BuildManager already exists?");
            return;
        }
        instance = this;
    }
    
    void Start(){
        prefabToBuild = standardTurretPrefab;
    }

    void Update(){
        if (buildingNow){
            if (buildPreview == null){
                InitPreview();
            }
            MovePrefabToGrid();
            RotatePreview();
            ReleaseIfClicked();
        }
    }
    public void InitPreview(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100)){
            Vector3 pos = GetComponent<GridManager>().GetNearestPointOnGrid(hit.point);
            buildPreview = (GameObject) Instantiate(prefabToBuild, pos, prefabToBuild.transform.rotation);
            //TODO:
            //should make it transparent or something to show user it's a preview, 
            //disable collisions with enemies so you can't push them around, 
            //and disable any scripts so you can't just have a flying turret
        }
    }
    public void MovePrefabToGrid(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100)){
            Vector3 pos = GetComponent<GridManager>().GetNearestPointOnGrid(hit.point);
            buildPreview.transform.position = pos;
        }
    }

    public void RotatePreview(){
        if (!buildPreview){
            return;
        }
        mouseWheelRotation += Input.mouseScrollDelta.y;
        buildPreview.transform.Rotate(0f, mouseWheelRotation * 10f, 0f, Space.World);
        mouseWheelRotation = 0f;      
    }
    
    public void ReleaseIfClicked(){
        if (Input.GetMouseButtonDown(0)) {
            //TODO: should reset preview effects too
            buildPreview = null; 
            ToggleBuilding();
        }
    }
    public GameObject GetPrefabToBuild(){
        return prefabToBuild;
    }

    public void SetPrefabToBuild(GameObject prefab){
        Debug.Log("setting prefab to "+prefab);
        prefabToBuild = prefab;
    }

    public void ToggleBuilding(){
        buildingNow = !buildingNow;
    }

    public void CancelBuilding(){
        //currently not working
        if (buildPreview != null){
            Destroy(buildPreview);
        }
        buildingNow = false;
    }

}