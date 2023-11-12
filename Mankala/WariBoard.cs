namespace Mankala;

public class WariBoard : IBoard
{
    private int numpits;
    private HomePit p1HomePit = new HomePit(0, (-1), Player.P1); //wari has its homepits not as part of the board circuit, so they are just kept as loose objects
    private HomePit P2HomePit = new HomePit(0, (-1), Player.P2);
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="pits">starting pits</param>
    /// <param name="amountInPits">starting amount in pits</param>
    public WariBoard(int pits = 12, int amountInPits = 4)
    {
        numpits = pits;
        List<APit> tempWariList = new List<APit>();
        APit? previous = null;
        for (int i = 0; i < pits; i++)//in this case just -1 for indexes, because home pits are not part of board circuit
        {
            APit current;
            if (i == pits-1)
            {
                current = new Pit(amountInPits, i, Player.P2);
                tempWariList[0].Connect(current);
            }
            else if (i > (pits - 2) / 2)
            {
                current = new Pit(amountInPits, i, Player.P2);
            }
            else current = new Pit(amountInPits, i, Player.P1);
            
            current.Connect(previous);
            tempWariList.Add(current);
            previous = current;
        }

        PitList = tempWariList;
        PitList.Add(p1HomePit);
        PitList.Add(P2HomePit);
    }
    
    /// <summary>
    /// move an amount of pits from one pit to another
    /// </summary>
    /// <param name="from">pit to move from</param>
    /// <param name="to">pit to move to</param>
    /// <param name="amount">amount of stones to move</param>
    public void MoveAmount(APit from, APit to, int amount)
    {
        to.Stones += amount;
        from.Stones -= amount;
    }

    /// <summary>
    /// does a move on the board
    /// </summary>
    /// <param name="p">player to make move for</param>
    /// <param name="move">move in question</param>
    /// <returns>next player, usefull for extra turns</returns>
    public Player DoMove(Player p, int moveIndex)
    {
        APit homePit = GetHomePits(p)[0];
        APit start = PitList[moveIndex];
        APit current = PitList[moveIndex];
        int count = start.Stones;
        Player playerturnchange = p == Player.P1 ? Player.P2 : Player.P1;
        while (count > 0)
        {
            current = current.Next;
            if ((current.Stones == 1 || current.Stones == 2) && count == 1 && GetPlayerPits(playerturnchange).Contains(current))
            {
                MoveAmount(current, homePit, current.Stones);
            }
            else
            {
                MoveAmount(start, current, 1);
            }

            count--;
        }
        
        return playerturnchange;
    }

    /// <summary>
    /// helper function for get homepits/playerpits
    /// </summary>
    /// <param name="p"></param>
    /// <param name="pt"></param>
    /// <returns></returns>
    public List<APit> GetPitsForType(Player p, PitType pt)
    {
        List<APit> pits = new List<APit>();
        foreach (APit pit in PitList)
        {
            if (pit.Type == pt && pit.Player == p) {pits.Add(pit);}
        }
        return pits;
    }

    /// <summary>
    /// gets home pits of a given player
    /// </summary>
    /// <param name="p">player to get home pits from</param>
    /// <returns>home pits of player p</returns>
    public List<APit> GetHomePits(Player p) { return GetPitsForType(p, PitType.HomePit);}

    /// <summary>
    /// get normal pits of a given player
    /// </summary>
    /// <param name="p">player to get pits from</param>
    /// <returns>pits of player p</returns>
    public List<APit> GetPlayerPits(Player p) { return GetPitsForType(p, PitType.Pit);}
    
    /// <summary>
    /// when creating this please add the home pits at the end of the list, then the parsing will take care of the rest.
    /// </summary>
    public List<APit> PitList { get; set; }
    
}