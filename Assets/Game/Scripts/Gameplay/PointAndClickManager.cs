using UnityEngine;

public class PointAndClickManager : MonoBehaviour
{
  public delegate void SmartObjectClickedHandler(SmartObject smartObject);
  public static SmartObjectClickedHandler OnSmartObjectClicked;

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer(Constants.Layers.SmartObjects))
        {
          OnSmartObjectClicked(hit.transform.gameObject.GetComponent<SmartObject>());
        } 
      }
    }
  }
}
