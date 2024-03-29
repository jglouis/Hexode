﻿using UnityEngine;
using System.Collections.Generic;

// HexGridManager handles the hexagonal grid of hexagon sprites.
public class HexGridManager : MonoBehaviour
{
    
    public float Radius = 1.0f;
    public float HexProportion = 0.9f;
    public SpriteRenderer HexagonPrefab;
    public int Size = 5; //size of the grid
    static float r = 1.0f;
    static Dictionary<Vector2, SpriteRenderer> hexagons;
    static Rect rectBoardBoundaries = new Rect (0, 0, 0, 0);
    
    // Use this for initialization.
    void Start ()
    {
        // Assign static r
        r = Radius;

        // Create a dictionnary that will store each hex prefab instance
        // according to its position in hexagonal coordinates (u,v)
        // u is the x axe and v is the "north-west" axe
        // central hexagon is of (0,0) coordinates.
        hexagons = new Dictionary<Vector2, SpriteRenderer> ();
        
        for (int u = -(Size-1); u < Size; u++)
            for (int v = -(Size-1); v < Size; v++) {            
                Vector2 hexCoord = new Vector2 (u, v);            
                CreateHex (hexCoord);            
            }
    }

    // Create an hex prefab, place it on the board, in the dictionary and infomr the hex of its coordinates.
    public void CreateHex (Vector2 hexCoord)
    {                                                    

        // Place the hex.
        Vector3 hex_pos = this.GetTransformCoordinates (hexCoord);
        SpriteRenderer hexagon = Instantiate (HexagonPrefab, hex_pos, HexagonPrefab.transform.rotation) as SpriteRenderer;

        // Change the name of the hexagon, adding hexagonal coordinates, to make the game scene easier to debug.
        hexagon.name += " (" + hexCoord [0] + "," + hexCoord [1] + ")";

        // Make the hexagon a child of HexGrid.
        hexagon.transform.parent = transform;

        // Adjust rect boundaries (for screen scrolling).
        rectBoardBoundaries.xMax = Mathf.Max (rectBoardBoundaries.xMax, hex_pos.x);
        rectBoardBoundaries.xMin = Mathf.Min (rectBoardBoundaries.xMin, hex_pos.x);
        rectBoardBoundaries.yMax = Mathf.Max (rectBoardBoundaries.yMax, hex_pos.y);
        rectBoardBoundaries.yMin = Mathf.Min (rectBoardBoundaries.yMin, hex_pos.y);

        // Store the hex.
        hexagons [hexCoord] = hexagon;   
    }

    // Create empty hexes adjacents to hex (if not already created).
    public void CreateEmptyAdjacentHexes (Vector2 centerHexCoord)
    {

        foreach (Vector2 adjHexCoord in HexUtil.GetAdjacentCoords(centerHexCoord)) {
        
            // If the hex already exists, then do nothing.
            if (hexagons.ContainsKey (adjHexCoord))
                continue;
        
            CreateHex (adjHexCoord);         
        }       
    }   

    // GetHexCoordinates returns the hex coordinates, given transform coordinates.
    public static Vector2 GetHexCoordinates (Vector3 transformCoordinates)
    {
        float x = transformCoordinates.x;
        float y = transformCoordinates.y;

        int u = Mathf.RoundToInt ((x * Mathf.Sqrt (3.0f) / 3.0f - y / 3.0f) / r);
        int v = Mathf.RoundToInt (y * 2.0f / 3.0f / r);

        return new Vector2 (u, v);
    }

    // GetTransformCoordinates returns the transform coordinates, given the hexagonal coordinates (u,v) + height (h).
    public static Vector3 GetTransformCoordinates (Vector2 hexCoord)
    {

        float u = hexCoord [0]; 
        float v = hexCoord [1];

        float x = Mathf.Sqrt (3.0f) * r * (u + v / 2.0f);
        float y = 3 * r * v / 2.0f;
        return new Vector3 (x, y, 0);
    }

    // Property that computes the board boundaries as rectangle coodinates.
    public static Rect RectBoardBoundaries {    
        get {            
            return rectBoardBoundaries;         
        }       
    }

    public static Dictionary<Vector2, SpriteRenderer> Hexagons {
        get {
            return hexagons;
        }
    }
}
