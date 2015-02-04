using UnityEngine;
using Vectrosity;
using System.Collections;

public class ArrowManager : MonoBehaviour
{
    public Material LineMaterial;

    Vector3 pos1 = Vector3.zero; 
    Vector3 pos2 = Vector3.zero;
    VectorLine arrow;

    void Start ()
    {
        arrow = new VectorLine ("Arrow", new Vector3[0], LineMaterial, 6.0f);
    }
    
    void Update ()
    {        
        if (Input.GetMouseButtonDown (0)) {
            pos1 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.5f);
            pos1 = Camera.main.ScreenToWorldPoint (pos1); 
            pos2 = pos1;
            
        }
        
        if (Input.GetMouseButton (0)) {
            pos2 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.5f);
            pos2 = Camera.main.ScreenToWorldPoint (pos2); 
            
        }

        // Draw the Vectrosity arrow.
        arrow.points3.Clear ();
        arrow.points3.Add (pos1);
        arrow.points3.Add (pos2);
        arrow.Draw ();
    }

}
