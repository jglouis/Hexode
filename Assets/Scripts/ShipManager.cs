using UnityEngine;
using System;
using System.Collections.Generic;

// Component responsible for displaying ships.
public class ShipManager : MonoBehaviour
{
    public SpriteRenderer ShipSprite;
    public float MoveDuration = 1.0f;

//    void Start ()
//    {
//        // Put a ship sprite at (0,0).
//        var s = CreateSpaceShip (Vector2.zero);
//
//        // Move the sprite 
//        MoveSpaceShip (s, new Vector2 (2, 2), 0.8f);
//
//    }

    HexBoard board;
    Dictionary<SpaceShip, GameObject> shipsToGameObject = new Dictionary<SpaceShip, GameObject> (); // Keeps track of every ship and its corresponding gameobject.

    // Observe the Game for a turn to be completed.
    void Start ()
    {
        GameManager.Instance.Game.ChangedPhase += new ChangedPhaseHandler (UpdateBoard);
        board = GameManager.Instance.Game.Board;
    }

    void UpdateBoard (object sender, RoundAndPhaseEventArgs e)
    {
        int round = e.Round;
        Game.Phase phase = e.Phase;
        Debug.Log (round + " " + phase);

        // Look up the board for each ship.
        foreach (SpaceShip ship in board.Ships.Keys) {
            // Check if the ship exists in the dictionary.
            if (shipsToGameObject.ContainsKey (ship)) {
                // Move the ship to its new position.
                MoveSpaceShip (shipsToGameObject [ship], board.Ships [ship], MoveDuration);
            } else {
                // Create the ship.
                shipsToGameObject.Add (ship, CreateSpaceShip (board.Ships [ship]));
            }
        }
    }

    // Instantiate a space ship at hex coord (u,v).
    GameObject CreateSpaceShip (Vector2 uv)
    {
        var sprite = Instantiate (ShipSprite) as SpriteRenderer;
        // Make the ship a child of the ShipManager gameobject.
        sprite.name = "ship " + uv;
        sprite.transform.parent = transform;

        sprite.transform.position = HexGridManager.GetTransformCoordinates (uv);

        return sprite.gameObject;
    }

    // Move a space ship to destination (in hexagonal coordinate) in given time duration (in second).
    void MoveSpaceShip (GameObject spaceShip, Vector2 destination, float duration)
    {
        var dest = HexGridManager.GetTransformCoordinates (destination);
        LeanTween.move (spaceShip, dest, duration).setEase (LeanTweenType.easeInQuad);
    }
}
