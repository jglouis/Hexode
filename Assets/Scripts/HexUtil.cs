using UnityEngine;

public static class HexUtil
{
	
	// Distance between two hexes
	// dU = U2-U1
	// dV = V2-V1
	// D6 = (|dU|+|dV|+|dU-dV|)/2
	public static int DistanceBetween(Vector2 hexCoord1, Vector2 hexCoord2){
		int dU = (int) (hexCoord1[0] - hexCoord2[0]);
		int dV = (int) (hexCoord1[1] - hexCoord2[1]);
		return (
			Mathf.Abs(dU) +
			Mathf.Abs(dV) +
			Mathf.Abs(dU-dV)			
			)/2;
	}
	
	// true if the to coordiantes are adjacent
	public static bool IsAdjacent(Vector2 hexCoord1, Vector2 hexCoord2){
		
		if(DistanceBetween(hexCoord1, hexCoord2) == 1)
			return true;
		return false;	
	}
	
	// return an array with all the adjacents hexagonal coordinates
	public static Vector2[] GetAdjacentCoords(Vector2 hexCoord){
		
		Vector2[] adjHexCoords = new Vector2[6];
		Vector2[] adjToOrigin = {
			new Vector2(1,0),
			new Vector2(1,1),
			new Vector2(0,1),
			new Vector2(-1,0),
			new Vector2(-1,-1),
			new Vector2(0,-1)
		};
		
		for(int i = 0; i < 6; i++)
			adjHexCoords[i] = hexCoord + adjToOrigin[i];
		
		return adjHexCoords;
	}
}
