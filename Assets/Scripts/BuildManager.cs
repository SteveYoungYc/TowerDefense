using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public GameObject turretPrefab;

    public GameObject missileSiloPrefab;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //turretPrefab = Resources.Load<GameObject>("Prefabs/Turret");
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (EventSystem.current.IsPointerOverGameObject() == false)
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map"));
            if (isCollider && hit.collider.gameObject.CompareTag("Floor"))
            {
                if (MoneyManager.Affordable())
                {
                    Instantiate(turretPrefab, hit.point, Quaternion.identity);
                    MoneyManager.BuildATurret();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //if (EventSystem.current.IsPointerOverGameObject() == false)
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map"));
            if (isCollider && hit.collider.gameObject.CompareTag("Floor"))
            {
                Instantiate(missileSiloPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
