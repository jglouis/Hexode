using System;

// Represents a player.
public class Player
{

}

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

// Represents the game state.
public class Game
{
    // Event that is fired each time a new phase begins.
    public event ChangedPhaseHandler ChangedPhase;
    
    // Invoke the event.
    void onChangedPhase (RoundAndPhaseEventArgs e)
    {
        if (ChangedPhase != null)
            ChangedPhase (this, e);
    }

    // Represents the phase during a round.
    public enum Phase
    {
        Start,
        AssignOrders,
        ComputeVectorsLight,
        ComputeNewPositions,
        ComputeVectorsMedium,
        ResolveCombat,
        ComputeVectorsHeavy,
        End
    }

    Player[] players;
    int currentRound; // the current round number
    Phase currentPhase;

    HexBoard board;

    public HexBoard Board {
        get {
            if (board == null) {
                // Create an empty board.
                board = new HexBoard ();
            }
            return board;
        }
    }

    // Start the game.
    public void Start ()
    {
        // Begins Round 1, Start phase. 
        currentRound = 1;
        currentPhase = Phase.Start;

    }

    // Proceed to the next phase.
    // Proceed to the next round if the round is finished (End Phase).
    public void NextPhase ()
    {
        if (currentPhase == Phase.End) {
            currentPhase = Phase.Start;
            currentRound++;
        } else
            currentPhase++;
        
        onChangedPhase (new RoundAndPhaseEventArgs (currentRound, currentPhase));
    }
}
