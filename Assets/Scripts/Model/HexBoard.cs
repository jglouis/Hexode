using System;
using UnityEngine;
using System.Collections.Generic;

// Represents the hexagonal board.
// A HexBoard has the responsability for handling the Ships it contains.
public class HexBoard
{
    // Dictionary containing all the ships.
    Dictionary<SpaceShip, Vector2> ships;

    public HexBoard ()
    {
        // Initialize the ships Dictionary.
        ships = new Dictionary<SpaceShip, Vector2> ();
    }
}
