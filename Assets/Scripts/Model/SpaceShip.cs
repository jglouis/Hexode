using System;
using UnityEngine;

public enum Weight
{
    Light,
    Medium,
    Heavy
}

// Represents a ship. Holds the ships state.
public class SpaceShip
{
    public Weight Ton;
    public int Maneuvrability;
    public int Acceleration;
    
    public Vector2 Orientation;
    public Vector2 Movement;
    public Vector2 Impulsion;

    // Create a SpaceShip.
    // Acceleration and manoeuvrability should be set between 1 and 6.
    public SpaceShip (Weight ton, int maneuvrability, int acceleration)
    {
        Ton = ton;
        Maneuvrability = maneuvrability;
        Acceleration = acceleration;
    }
}
