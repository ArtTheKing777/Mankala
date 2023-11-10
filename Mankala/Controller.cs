namespace Mankala;

public class Controller
{
    private Player _currentplayer;
    private View _view;
    private Model _model;
    
    public Controller(Player currentplayer)
    {
        _currentplayer = currentplayer;
        _view = new View();
        _model = new Model();
        StartProgramIO();
    }

    private void StartProgramIO()
    {
        _view.WriteLine("welcome to this program where you can play mankala like games.");
        _view.WriteLine("do you want to start or quit the program? press 1 for start and press 2 for quit");
        
        string a = Console.ReadLine();
        if(a==null) StartProgramIO();
        else if(a == "1") MakeGame();
        else if(a == "2"){}//stop program
        else {_view.WriteLine("try enter 1 or 2"); StartProgramIO();}
    }
    
    private void MakeGame()
    {
        string a = SelectGameTypeIO();
        _model.SetMode(a);
        StartGameIO();
    }

    private void StartGameIO()
    {
        _view.WriteLine("Press 1 to start game or press 2 to go to the game select");
        
        string a = Console.ReadLine();
        if (a == null){_view.WriteLine("not an option, enter 1 or 2");StartGameIO();}
        else if (a == "1") CurrentGameLoop(); 
        else if (a == "2") StartProgramIO();
        else {Console.WriteLine("try enter 1 or 2"); StartGameIO();}
    }

    private void CurrentGameLoop()
    {
        DoMove(_currentplayer);
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

    private void DoMove(Player player)
    {
        List<int> m = _model.GetMoves(player);
        int move = DoMoveIO(player, m);
        Player p = _model.DoMove(player, move);
        if (player == p) _model.DoMove(player, DoMoveIO(player, _model.GetMoves(player))); //_model.DoMove returns the next player, so if that is the same player, they get another turn.
        else _currentplayer = p; //else switch the current player;
    }

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

         string a = Console.ReadLine();
         if (a == "")
         {
             _view.WriteLine("please enter something");
             DoMove(player);
         }
        
         if (Convert.ToInt32(a) == null) //check if int
         {
             _view.WriteLine("please enter a number");
             DoMove(player);
         }

         int j = Convert.ToInt32(a);

         return moves[j-1];
    }

    private string SelectGameTypeIO()
    {
        _view.WriteLine("select one of the game types by pressing the number before it.");
        _view.WriteLine("GameTypes:");
        WriteGameTypesIO();

        string a = Console.ReadLine();
        if (a == null)
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

    private void WriteGameTypesIO()
    {
        _view.WriteLine("1. manakala");
        _view.WriteLine("2. wari");
        _view.WriteLine("3. newvariant");
    }
}