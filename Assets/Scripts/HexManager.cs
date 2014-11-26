using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameObjectList = System.Collections.Generic.List<UnityEngine.GameObject>;


// HexManager handles the hexagonal grid of hexagon sprites
public class HexManager : MonoBehaviour {
	
	public float Radius = 1.0f;
	public float HexProportion = 0.9f;
	
	public GameObject HexagonPrefab;
	public int Size = 5; //size of the grid
	private static Dictionary<Vector2, GameObject> hexagons;
	private static Rect rectBoardBoundaries = new Rect(0,0,0,0);
	
	// Use this for initialization
	void Start () {		
		
		// Create a dictionnary that will store each hex prefab instance
		// according to its position in hexagonal coordinates (u,v)
		// u is the x axe and v is the "north-west" axe
		// central hexagon is of (0,0) coordinates
		hexagons = new Dictionary<Vector2, GameObject>();
		
		for(int u = -(Size-1); u < Size; u++)
		for(int v = -(Size-1); v < Size; v++){
			
			Vector2 hexCoord = new Vector2(u,v);

			Debug.Log(new Vector2(u,v));
			
			CreateHex(hexCoord);
			
		}		
	}
	
	
	
	//C reate an hex prefab, place it on the board, in the dictionary and infomr the hex of its coordinates
	public void CreateHex(Vector2 hexCoord){													
		
		// place the hex 
		Vector3 hex_pos = this.GetTransformCoordinates(hexCoord);
		GameObject hexagon = Instantiate(HexagonPrefab,hex_pos,HexagonPrefab.transform.rotation) as GameObject;
		
		// change the name of the hexagon, adding hexagonal coordinates, to make the game scene easier to debug
		hexagon.name += " (" + hexCoord[0] + "," + hexCoord[1] + ")";
		
		// make the hexagon a child of HexGrid
		hexagon.transform.parent = transform;
		
		// adjust rect boundaries (for screen scrolling)
		rectBoardBoundaries.xMax = Mathf.Max(rectBoardBoundaries.xMax, hex_pos.x);
		rectBoardBoundaries.xMin = Mathf.Min(rectBoardBoundaries.xMin, hex_pos.x);
		rectBoardBoundaries.yMax = Mathf.Max(rectBoardBoundaries.yMax, hex_pos.y);
		rectBoardBoundaries.yMin = Mathf.Min(rectBoardBoundaries.yMin, hex_pos.y);
		
		// store the hex
		hexagons[hexCoord] = hexagon;
//		Hex hexScript = (Hex) hexagon.GetComponentInChildren(typeof(Hex));
//		
//		// set a reference to the hex manager
//		hexScript.Hexmanager = this;
//		
//		// inform the hex prefab of its own hex coordinates
//		hexScript.HexCoord = hexCoord;
		
		
	}
	
	// Create empty hexes adjacents to hex (if not already created)
	public void CreateEmptyAdjacentHexes(Vector2 centerHexCoord){
		
		foreach(Vector2 adjHexCoord in HexUtil.GetAdjacentCoords(centerHexCoord)){
			
			// if the hex already exists, then do nothing
			if(hexagons.ContainsKey(adjHexCoord))
				continue;
			
			CreateHex(adjHexCoord);
			
		}		
	}	
	
	// return the transform coordinates, given the hexagonal coordinates (u,v) + height (h)
	private Vector3 GetTransformCoordinates(Vector2 hexCoord){
		
		float u = hexCoord[0]; 
		float v = hexCoord[1];
		float h = 0;
		
		float x = Mathf.Sqrt(3.0f) * Radius * u - Mathf.Sqrt(3.0f) * Radius / 2.0f * v;
		float y = 3 * Radius * v / 2.0f;
		return new Vector3(x, y, h);
	}


	
	// property that computes the board boundaries as rectangle coodinates
	public static Rect RectBoardBoundaries{		
		get{			
			return rectBoardBoundaries;			
		}		
	}
	
	public static Dictionary<Vector2, GameObject> Hexagons {
		get {
			return hexagons;
		}
	}
}
