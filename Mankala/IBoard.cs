using System.Dynamic;

namespace Mankala;

public interface IBoard
{
    void MoveAmount(APit from, APit to, int amount);

    Player DoMove(Player p , int move);

    List<APit> GetHomePits(Player p);

    List<APit> GetPlayerPits(Player p);
    
    List<APit> PitList { get; set; }
    
}