namespace Mankala;

public class GameManager
{
    private Game game;
    private string[] gameTypes = { "mankala","custom mankala" };
    
    static void Main(string[] args)
    {
        GameManager g = new GameManager();
        g.StartProgramIO();
    }
    
    public void StartProgramIO()
    {
        Console.WriteLine("welcome to this program where you can play mankala like games.");
        Console.WriteLine("do you want to start or quit the program? press 1 for start and press 2 for quit");
        
        string a = Console.ReadLine();
        if(a==null) StartProgramIO();
        else if(a == "1") MakeGame();
        else if(a == "2"){}//stop program
        else {Console.WriteLine("try enter 1 or 2"); StartProgramIO();}
    }
    
    private void MakeGame()
    {
        string a = SelectGameTypeIO();
        if (a == "mankala")
        {
            game = new mankala(this);
            game.StartGameIO();
        }
        else if (a == "custom mankala")
        {
           CustomMankalaIO();
        }
    }

    private void CustomMankalaIO()
    {
        Console.WriteLine("please enter the amount of pits each player will have");
        string b = Console.ReadLine();
        if (b == null)
        {
            Console.WriteLine("please enter something");
            CustomMankalaIO();
        }

        if (Convert.ToInt32(b) == null)
        {
            Console.WriteLine("please enter a number");
            CustomMankalaIO();
        }
        int B = Convert.ToInt32(b);
        
        Console.WriteLine("please enter the amount of stones each pit will have");
        string a = Console.ReadLine();
        if (a == null)
        {
            Console.WriteLine("please enter something");
            CustomMankalaIO();
        }

        if (Convert.ToInt32(a) == null)
        {
            Console.WriteLine("please enter a number");
            CustomMankalaIO();
        }

        int A = Convert.ToInt32(a);

        game = new mankala(this, B, A);
        game.StartGameIO();
    }

    private string SelectGameTypeIO()
    {
        Console.WriteLine("select one of the game types by pressing the number before it.");
        Console.WriteLine("GameTypes:");
        for (int i = 0; i < gameTypes.Length; i++)
        {
            Console.WriteLine((i+1) + ". " + gameTypes[i]); 
        }
        
        string a = Console.ReadLine();
        if (a == null)
        {
            Console.WriteLine("please enter something");
            MakeGame();
        }
        
        if (Convert.ToInt32(a) == null) //check if int
        {
            Console.WriteLine("please enter a number");
            MakeGame();
        }

        int j = Convert.ToInt32(a);

        return gameTypes[j-1];
    }
}