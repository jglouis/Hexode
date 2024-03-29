﻿using UnityEngine;
using Vectrosity;
using System.Collections;

public class ArrowManager : MonoBehaviour
{
    public float LineThickness = 12.0f;
    public float TextureScale = 4.0f;
    public float AnimationSpeed = 2.0f;
    public Color ArrowColor = Color.white;
    public Material LineMaterial;
    public Texture2D ArrowEndCap;

    Vector3 pos1 = Vector3.zero; 
    Vector3 pos2 = Vector3.zero;
    VectorLine arrow;

    void Start ()
    {
        arrow = new VectorLine ("Arrow", new Vector3[2], LineMaterial, LineThickness);

        // Set the end cap of the arrow.
        VectorLine.SetEndCap ("ArrowEndCap", EndCap.Front, LineMaterial, -1.0f, ArrowEndCap);
        arrow.endCap = "ArrowEndCap";

        // Set arrow style:
        arrow.textureScale = TextureScale;
        arrow.SetColor (ArrowColor);
    }

    void OnMouseDown ()
    {
        // Resets the positions to the center of the object.
        pos1 = transform.position;
        pos2 = pos1;
    }

    void OnMouseDrag ()
    {
        pos2 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.5f);
        pos2 = Camera.main.ScreenToWorldPoint (pos2);

       
    }

    void OnMouseUp ()
    {
        // When mouse is released, the arrow must point to the center of the hex.
        pos2 = HexGridManager.GetTransformCoordinates (HexGridManager.GetHexCoordinates (Camera.main.ScreenToWorldPoint (Input.mousePosition)));
    }
    
    void Update ()
    {

        if (Input.GetMouseButtonDown (0)) {
            // Check the hexagon clicked.
            Debug.Log (HexGridManager.GetHexCoordinates (Camera.main.ScreenToWorldPoint (Input.mousePosition)));
        }

        // Draw the Vectrosity arrow.
        arrow.points3.Clear ();
        arrow.points3.Add (pos2);
        arrow.points3.Add (pos1);
        arrow.Draw ();

        arrow.textureOffset = Time.time * AnimationSpeed % 1;
    }
}
