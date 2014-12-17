using System;

// Represents a player.
public class Player
{

}

// Represents the game state.
public class Game
{
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

    // Create a new 2 player game.
    public Game (Player p1, Player p2)
    {
        players = new Player[] {p1,p2};
    }

    // Start the game.
    public void Start ()
    {
        // Begins Round 1, Start phase. 
        currentRound = 1;
        currentPhase = Phase.Start;

        // Create an empty board.
        board = new HexBoard ();
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
    }
}
