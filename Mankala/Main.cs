namespace Mankala;

public class main
{
    private static Controller _controller;
    
    /// <summary>
    /// classic entry point
    /// </summary>
    /// <param name="args">something</param>
    static void Main(string[] args)
    {
        _controller = new Controller(Player.P1);
    }
}