using UnityEngine;
using System;

// This script will be the master script concerning the game rules.
public class GameManager : MonoBehaviour
{
    // Singleton
    static GameManager instance;
  
    //get the singleton
    public static GameManager Instance {
        get {
            if (instance == null) {
                // This is where the magic happens.
                // FindObjectOfType(...) returns the first GameMaster object in the scene.
                instance = FindObjectOfType (typeof(GameManager)) as GameManager;
            }
      
            // If it is still null, create a new instance.
            if (instance == null) {
                GameObject obj = new GameObject ("Game Master");
                obj.AddComponent (typeof(NetworkView));              
                instance = obj.AddComponent (typeof(GameManager)) as GameManager;
                obj.networkView.observed = obj.GetComponent<GameManager> ();
                Debug.Log ("Could not locate an GameManager object. Singleton created on fly.");
            }
      
            return instance;
        }
    }

    // UI
    public void OnClick ()
    {
        game.NextPhase ();
    }

    Game game;

    public Game Game {
        get {
            if (game == null) {
                game = new Game ();
            }
            return game;
        }
    }

    void Start ()
    {
        Debug.Log ("Starting a game...");
       
        Player p1 = new Player ();
        Player p2 = new Player ();
        game = Game;
        game.Start ();

        // Create a ship.
        SpaceShip ship = new SpaceShip (Weight.Light, 3, 1);

        // Add it to the board.
        game.Board.Add (ship, Vector2.zero);
    }

}
