﻿using System.Collections.Generic;
using UnityEngine;

public class PointAndClickManager : MonoBehaviour
{
  public delegate void SmartObjectClickedHandler (SmartObject smartObject);
  public static SmartObjectClickedHandler OnSmartObjectClicked = delegate { };

  void Awake ()
  {
    cameraOutline = FindObjectOfType<CameraOutline> ().GetComponent<Camera> ();
    SetCustomLayerMask ();
  }

  private void OnDisable ()
  {
    disableAllLayersInCullingMask ();
  }

  void Update ()
  {
    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast (ray, out hit) && checkLayer (hit.transform.gameObject.layer))
    {
      SmartObject smartObject = hit.transform.gameObject.GetComponent<SmartObject> ();
      if (smartObject != lastSmartObject)
      {
        disableAllLayersInCullingMask();
      }
      lastSmartObject = smartObject;
      if (ActionsManager.Instance.IsSmartObjectInteractableOnCurrentStage (smartObject))
      {
        enableLayerInCullingMask (smartObject.MeshTransform.gameObject.layer);
        if (Input.GetMouseButtonDown (0))
        {
          OnSmartObjectClicked (smartObject);
        }
      }
      else
      {
        disableAllLayersInCullingMask ();
      }
    }
    else
    {
      disableAllLayersInCullingMask ();
    }
  }

  private void enableLayerInCullingMask (int layer)
  {
    cameraOutline.cullingMask |= 1 << layer;
  }

  private void disableAllLayersInCullingMask ()
  {
    if (cameraOutline != null)
    {
      cameraOutline.cullingMask = 0;
    }
  }

  private void SetCustomLayerMask ()
  {
    string[] layersString = new string[layers.Count];
    for (int i = 0; i < layers.Count; i++)
    {
      layersString[i] = LayerMask.LayerToName ((int) Mathf.Log (layers[i].value, 2));
    }
    layerMask = LayerMask.GetMask (layersString);
  }

  private bool checkLayer (int layer)
  {
    //return ((layerMask & (1 << layer)) > 0);
    return LayerMask.LayerToName(layer).Equals(Constants.Layers.SmartObjects);
  }

  private Camera cameraOutline;
  private LayerMask layerMask;
  private SmartObject lastSmartObject;

  [SerializeField]
  private List<LayerMask> layers;
}
