using System.Collections.Generic;
using UnityEngine;

public class PointAndClickManager : MonoBehaviour
{
  public delegate void SmartObjectClickedHandler(SmartObject smartObject);
  public static SmartObjectClickedHandler OnSmartObjectClicked;

  void Awake()
  {
    cameraOutline = FindObjectOfType<CameraOutline>().GetComponent<Camera>();
    SetCustomLayerMask();
  }

  void Update()
  {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        if (checkLayer(hit.transform.gameObject.layer))
        {
          enableLayerInCullingMask(hit.transform.root.gameObject.GetComponent<SmartObject>().MeshTransform.gameObject.layer);
          if (Input.GetMouseButtonDown(0))
          {
            if (OnSmartObjectClicked != null)
            {
              OnSmartObjectClicked(hit.transform.root.gameObject.GetComponent<SmartObject>());
            }
          }
        }
      }
      else
      {
        disableAllLayersInCullingMask();
      } 
  }

  private void enableLayerInCullingMask(int layer)
  {
    cameraOutline.cullingMask |= 1 << layer;
  }

  private void disableAllLayersInCullingMask()
  {
    cameraOutline.cullingMask = 0;
  }

  private void SetCustomLayerMask()
  {
    string[] layersString = new string[layers.Count];
    for (int i = 0; i < layers.Count; i++)
    {
      layersString[i] = LayerMask.LayerToName((int)Mathf.Log(layers[i].value, 2));
    }
    layerMask = LayerMask.GetMask(layersString);
  }

  private bool checkLayer(int layer)
  {
    return ((layerMask & (1 << layer)) > 0);
  }

  private Camera cameraOutline;
  private LayerMask layerMask;

  [SerializeField]
  private List<LayerMask> layers;
}
