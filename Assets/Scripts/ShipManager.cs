using UnityEngine;
using System.Collections.Generic;

// Component responsible for displaying ships
public class ShipManager : MonoBehaviour
{
    public SpriteRenderer ShipSprite;

    // List of all the ship sprites
    List<SpriteRenderer> shipSprites = new List<SpriteRenderer> ();

    void Start ()
    {
        // Put a ship sprite in each case
        foreach (var k in HexGridManager.Hexagons.Keys) {
            CreateSpaceShip (k);
        }
    }

    // Instantiate a space ship at hex coord (u,v)
    void CreateSpaceShip (Vector2 uv)
    {
        var sprite = Instantiate (ShipSprite) as SpriteRenderer;
        // Make the ship a child of the ShipManager gameobject
        sprite.name = "ship " + uv;
        sprite.transform.parent = transform;

        sprite.transform.position = HexGridManager.GetTransformCoordinates (uv);


    }
}
