﻿namespace Mankala;

public abstract class Game
{
    private GameManager gameManager;
    protected Board board;
    protected CurrentGameState currentGameState;
    
    protected int _numberOfPits;
    protected int _startingStones;

    protected Game(GameManager g)
    {
        gameManager = g;
        //default board can overwrite
        _numberOfPits = 2;
        _startingStones = 1;
        board = new Board(_numberOfPits, _startingStones, new[] { 1 }, new[] { 1 });
        currentGameState = CurrentGameState.TurnP1;
    }
    //I am using a switch here because i can, but it might look better if I just use if statements
    private void CurrentGameLoop()
    {
        //change the Gamestate but allow for overwrite in move
        if (currentGameState == CurrentGameState.TurnP1) DoMove(Player.P1); else DoMove(Player.P2);

        if (!EndGame())
        {
            CurrentGameLoop();
        }
        else
        {
            currentGameState = DetermineWinner();
            
            if (currentGameState == CurrentGameState.P1Win)Console.WriteLine("PLAYER 1 WINS!!");
            else if (currentGameState == CurrentGameState.P2Win)Console.WriteLine("PLAYER 2 WINS!!");
            else Console.WriteLine("DRAW");
            
            StartGameIO();
        }
    }

    protected CurrentGameState SwitchPlayerTurn()
    {
        if (CurrentGameState.TurnP1 == currentGameState) return CurrentGameState.TurnP2;
        else return CurrentGameState.TurnP1;
    }

    //assume that moves can only be made on the pits of the player
    protected int DoMoveIO(int[] moves, Player player)
    {
        if (moves.Length == 0) return -1;//there are no moves possible so 
        
        printBoard();
        Console.WriteLine("Choose your move by pressing the number before:");
        
        for (int i=0; i < moves.Length; i++)
        {
            Console.WriteLine((i+1) + "." + "move pit " + moves[i] );
        }

        string a = Console.ReadLine();
        if (a == null)
        {
            Console.WriteLine("please enter something");
            DoMove(player);
        }
        
        if (Convert.ToInt32(a) == null) //check if int
        {
            Console.WriteLine("please enter a number");
            DoMove(player);
        }

        int j = Convert.ToInt32(a);

        return moves[j-1];
    }
    public void StartGameIO()
    {
        Console.WriteLine("Press 1 to start game or press 2 to go to the game select");
        
        string a = Console.ReadLine();
        if (a == null){Console.WriteLine("not an option, enter 1 or 2");StartGameIO();}
        else if (a == "1") CurrentGameLoop(); 
        else if (a == "2") gameManager.StartProgramIO();
        else {Console.WriteLine("try enter 1 or 2"); StartGameIO();}
    }

    //default will be just count value of the things in the mankalas and highest number wins
    protected virtual CurrentGameState DetermineWinner()
    {
        int p1Count = board.GetPlayerMankalas(Player.P1).Sum();
        int p2Count = board.GetPlayerMankalas(Player.P2).Sum();
        if (p1Count > p2Count) return CurrentGameState.P1Win;
        if (p1Count < p2Count) return CurrentGameState.P2Win;
        return CurrentGameState.Draw;
    }
    
    private void MoveStonesToMankala()
    {
        int[] P1Stones = board.GetPitsOfPlayer(Player.P1);
        for (int i = 0; i < _numberOfPits; i++)
        {
            board.MoveAmount(i, _numberOfPits,P1Stones[i]);
        }
        int[] P2Stones = board.GetPitsOfPlayer(Player.P2);
        for (int i = 7; i < (2*_numberOfPits)+1; i++) 
        { 
            board.MoveAmount(i, (_numberOfPits), P2Stones[i]);
        }
    }

    //if no moves for both player end game (default)
    protected virtual bool EndGame()
    {
        return GetMoves(Player.P1).Length + GetMoves(Player.P2).Length == 0;
    }

    //if not implemented in sub classes just give empty to indicate no moves possible (default)
    protected virtual int[] GetMoves(Player p)
    {
        return Array.Empty<int>();
    }

    //default is get move based on current player and do nothing
    protected virtual void DoMove(Player player)
    {
        int[] m;
        if (CurrentGameState.TurnP1 == currentGameState) m = GetMoves(Player.P1);
        else m = GetMoves(Player.P2);
       
        if(m.Length == 0)return;

        int chosenM = DoMoveIO(m, player);
        //do the move based on the rules
    }
    
    //this is virtual because you might want it to look diffrent based on the version your playing
    protected virtual void printBoard()
    {
        Console.WriteLine("player1 pits: "+board.GetPitsOfPlayer(Player.P1));
        Console.WriteLine("player1 mankala's: "+board.GetPlayerMankalas(Player.P1));
        Console.WriteLine("player2 pits: "+board.GetPitsOfPlayer(Player.P2));
        Console.WriteLine("player2 mankala's: "+board.GetPlayerMankalas(Player.P2));
    }
    
}