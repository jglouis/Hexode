using UnityEngine;
using System;

// A delegate type for hooking up change phase notifications.
public delegate void ChangedPhaseHandler (object sender,RoundAndPhaseEventArgs e);

public class RoundAndPhaseEventArgs:EventArgs
{
    public int Round;
    public Game.Phase Phase;
    public RoundAndPhaseEventArgs (int round, Game.Phase phase)
    {
        this.Round = round;
        this.Phase = phase;
    } 
}

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

    // Event that is fired each time a new phase begins.
    public event ChangedPhaseHandler ChangedPhase;

    // Invoke the event.
    void onChangedPhase (RoundAndPhaseEventArgs e)
    {
        if (ChangedPhase != null)
            ChangedPhase (this, e);
    }

    // UI
    public void OnClick ()
    {
        NextPhase ();
    }




    // Some tests...

    Game game;


    void Start ()
    {
        Debug.Log ("Starting a game...");
       
        Player p1 = new Player ();
        Player p2 = new Player ();
        game = new Game (p1, p2);
        game.Start ();

        // Create a ship.
        SpaceShip ship = new SpaceShip (Weight.Light, 3, 1);
    }

    void NextPhase ()
    {
        game.NextPhase ();
        onChangedPhase (new RoundAndPhaseEventArgs (game.CurrentRound, game.CurrentPhase));
    }
}
