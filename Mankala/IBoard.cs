using System.Dynamic;

namespace Mankala;

/// <summary>
/// board interface
/// </summary>
public interface IBoard
{
    /// <summary>
    /// move an amount of pits from one pit to another
    /// </summary>
    /// <param name="from">pit to move from</param>
    /// <param name="to">pit to move to</param>
    /// <param name="amount">amount of stones to move</param>
    void MoveAmount(APit from, APit to, int amount);

    /// <summary>
    /// does a move on the board
    /// </summary>
    /// <param name="p">player to make move for</param>
    /// <param name="move">move in question</param>
    /// <returns>next player, usefull for extra turns</returns>
    Player DoMove(Player p , int move);

    /// <summary>
    /// gets home pits of a given player
    /// </summary>
    /// <param name="p">player to get home pits from</param>
    /// <returns>home pits of player p</returns>
    List<APit> GetHomePits(Player p);

    /// <summary>
    /// get normal pits of a given player
    /// </summary>
    /// <param name="p">player to get pits from</param>
    /// <returns>pits of player p</returns>
    List<APit> GetPlayerPits(Player p);
    
    List<APit> PitList { get; set; }
    
}