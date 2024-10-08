using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UI;

public class OutlineSelection : MonoBehaviour
{
    private float khoangCachHover;
    [SerializeField] float defaultKhoangCach = 5f;
    [SerializeField] ExampleCharacterCamera characterCamera;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private Camera camera;

    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>();
        if (camera == null)
        {
            Debug.LogError("No camera found in the scene.");
        }
    }
    void Update()
    {
        khoangCachHover = characterCamera.TargetDistance + defaultKhoangCach;
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            // Kiểm tra khoảng cách từ camera tới đối tượng trúng raycast
            float distance = Vector3.Distance(camera.transform.position, raycastHit.point);

            // Chỉ hiện Outline khi khoảng cách <= 10f
            if (distance <= khoangCachHover)
            {
                highlight = raycastHit.transform;
                if (highlight.CompareTag("Selectable") && highlight != selection)
                {
                    
                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                        //highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                        //highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                    }
                }
                else
                {
                    highlight = null;
                }
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                ActiveObject rotationActive;
                if (highlight.gameObject.TryGetComponent<ActiveObject>(out rotationActive))
                {
                    rotationActive.HandelAction();
                }
                
                //if (selection != null)
                //{
                //    selection.gameObject.GetComponent<Outline>().enabled = false;
                //}
                //selection = raycastHit.transform;
                //selection.gameObject.GetComponent<Outline>().enabled = true;
                //highlight = null;
            }
            else
            {
                //if (selection)
                //{
                //    selection.gameObject.GetComponent<Outline>().enabled = false;
                //    selection = null;
                //}
            }
        }
    }


}
