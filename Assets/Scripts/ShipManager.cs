using UnityEngine;
using System;

// Component responsible for displaying ships.
public class ShipManager : MonoBehaviour
{
    public SpriteRenderer ShipSprite;

//    void Start ()
//    {
//        // Put a ship sprite at (0,0).
//        var s = CreateSpaceShip (Vector2.zero);
//
//        // Move the sprite 
//        MoveSpaceShip (s, new Vector2 (2, 2), 0.8f);
//
//    }

    // Observe the GameManager for a turn to be completed.
    void Start ()
    {
        GameManager.Instance.ChangedPhase += new ChangedPhaseHandler (UpdateBoard);
    }

    void UpdateBoard (object sender, RoundAndPhaseEventArgs e)
    {
        int round = e.Round;
        Game.Phase phase = e.Phase;
        Debug.Log (round + " " + phase);
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
