namespace Mankala;

public class Controller
{
    private Player _currentPlayer;
    private View _view;
    private Model _model;
    
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="currentPlayer">the first player</param>
    public Controller(Player currentPlayer)
    {
        _currentPlayer = currentPlayer;
        _view = new View();
        _model = new Model();
        StartProgramIO();
    }

    /// <summary>
    /// input/output function to start the program
    /// </summary>
    private void StartProgramIO()
    {
        _view.WriteLine("welcome to this program where you can play mankala like games.");
        _view.WriteLine("do you want to start or quit the program? press 1 for start and press 2 for quit");
        
        string a = Console.ReadLine() ?? string.Empty;
        if(a == "1") MakeGame();
        else if(a == "2"){}//stop program
        else {_view.WriteLine("try enter 1 or 2"); StartProgramIO();}
    }
    
    /// <summary>
    /// creates a new game, of type given by SelectGameTypeIO
    /// </summary>
    private void MakeGame()
    {
        string a = SelectGameTypeIO();
        _model.SetMode(a);
        StartGameIO();
    }

    /// <summary>
    /// input/output function to start a game
    /// </summary>
    private void StartGameIO()
    {
        _view.WriteLine("Press 1 to start game or press 2 to go to the game select");
        
        string a = Console.ReadLine() ?? string.Empty;
        if (a == "1") CurrentGameLoop(); 
        else if (a == "2") StartProgramIO();
        else {Console.WriteLine("try enter 1 or 2"); StartGameIO();}
    }
    
    /// <summary>
    /// game loop
    /// </summary>
    private void CurrentGameLoop()
    {
        DoMove(_currentPlayer);
        if(!_model.EndGame()){CurrentGameLoop();}
        else
        {
            switch (_model.DetermineWinner())
            {
                case 0:
                    _view.WriteLine("there is a Draw");
                    StartGameIO();
                    break;
                case 1:
                    _view.WriteLine("player 1 has won");
                    StartGameIO();
                    break;
                case 2:
                    _view.WriteLine("player 2 has won");
                    StartGameIO();
                    break;
            }
        }
        
    }
    
    /// <summary>
    /// asks for a move via DoMoveIO, and then does it on a board.
    /// </summary>
    /// <param name="player">player who will make the move</param>
    private void DoMove(Player player)
    {
        List<int> m = _model.GetMoves(player);
        int move = DoMoveIO(player, m);
        if (move == -1)
        {
            _currentPlayer = _currentPlayer == Player.P1?Player.P2:Player.P1;
            return;
        }
        _currentPlayer = _model.DoMove(player, move);
    }

    /// <summary>
    /// input/output function for DoMove, gets the moves from DoMove, and asks the user which one they want to play
    /// then passes that back to DoMove
    /// </summary>
    /// <param name="player">player to make move for</param>
    /// <param name="moves">list of move options</param>
    /// <returns></returns>
    private int DoMoveIO(Player player, List<int> moves)
    {
        if (moves.Count == 0) return -1; //no moves
         _view.PrintBoard(player,_model.board);
         string pl = "";
         if (player == Player.P1) pl = "player 1"; else pl = "player 2";
         _view.WriteLine("Choose your move by pressing the corresponding number "+pl+":");
         
         for (int i=0; i < moves.Count; i++)
         {
             _view.WriteLine((i+1) + "." + "move pit " + (moves[i]+1) );
         }

         string a = Console.ReadLine() ?? string.Empty;
         if (a == "")
         {
             _view.WriteLine("please enter something");
             DoMove(player);
         }
        
         int j;
         if (int.TryParse(a,out j)) //check if int
         {
             _view.WriteLine("please enter a number");
             DoMove(player);
         }
         
         return moves[j-1];
    }
    
    /// <summary>
    /// input/output function to select a game type
    /// </summary>
    /// <returns>a game type to play, can be either mankala, wari, or NewVariant</returns>
    private string SelectGameTypeIO()
    {
        _view.WriteLine("select one of the game types by pressing the number before it.");
        _view.WriteLine("GameTypes:");
        WriteGameTypesIO();

        string a = Console.ReadLine() ?? string.Empty;
        if (a == "")
        {
            _view.WriteLine("please enter something");
            return SelectGameTypeIO();
        }
        else if (Int32.Parse(a) == null)
        {
            _view.WriteLine("please enter a valid number");
            return SelectGameTypeIO();
        }
        int input = int.Parse(a);
        switch (input)
        {
            case 1:
                return "mankala";
            case 2:
                return "wari";
            case 3:
                return "newvariant";
            default:
                _view.WriteLine("enter a valid number");
                return SelectGameTypeIO();
        }
    }
    
    /// <summary>
    /// quick helper function for compactness
    /// </summary>
    private void WriteGameTypesIO()
    {
        _view.WriteLine("1. manakala");
        _view.WriteLine("2. wari");
        _view.WriteLine("3. newvariant");
    }
}