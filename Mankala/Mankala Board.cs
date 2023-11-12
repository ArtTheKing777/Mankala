namespace Mankala;

public class MankalaBoard : IBoard
{
    private int numPits;
    
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="pits">starting pits</param>
    /// <param name="amountInPits">starting amount in pits</param>
    public MankalaBoard(int pits = 12,int amountInPits = 4)
    {
        numPits = pits;
        List<APit> tempMankalaList = new List<APit>();
        APit? previous = null;
        for (int i = 0; i < pits+2; i++)//+1 is actually +2(for homepits) -1(for indexes)
        {
            APit current;
            if (i == pits / 2)
            {
                current = new HomePit(0, i, Player.P1);
            }
            else if (i == pits+1)
            {
                current = new HomePit(0, i, Player.P2);
                tempMankalaList[0].Connect(current);
            }
            else if (i > pits / 2)
            {
                current = new Pit(amountInPits, i, Player.P2);
                int opposite = numPits - i;
                tempMankalaList[opposite].ConnectCross(current);
            } 
            else current = new Pit(amountInPits, i, Player.P1); 
            
            current.Connect(previous);
            tempMankalaList.Add(current);
            previous = current;
        }
        PitList = tempMankalaList;
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
    public Player DoMove(Player p,int moveIndex)
    {
        APit homePit = GetHomePits(p)[0];
        APit start = PitList[moveIndex];
        APit current = PitList[moveIndex];
        int count = start.Stones;
        Player playerturnchange = p == Player.P1?Player.P2:Player.P1;
        while (count > 0)
        {
            current = current.Next;
            if (current.Type == PitType.HomePit && current.Player == p && count == 1)
            {
                playerturnchange = p;
                MoveAmount(start,homePit , 1);
            }
            else if (current.Stones == 0 && current.Player == p && current.Type == PitType.Pit)
            {
                MoveAmount(current.Opposite,homePit,current.Opposite.Stones);
                MoveAmount(start,homePit , 1);
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
    public List<APit> GetPitsForType(Player p,PitType pt)
    {
        List<APit> pits = new List<APit>();
        
        foreach (APit pit in PitList)
        {
            if (pit.Type == pt && pit.Player == p) { pits.Add(pit); }
        }
        return pits;
    }

    /// <summary>
    /// gets home pits of a given player
    /// </summary>
    /// <param name="p">player to get home pits from</param>
    /// <returns>home pits of player p</returns>
    public List<APit> GetHomePits(Player p) { return GetPitsForType(p, PitType.HomePit); }

    /// <summary>
    /// get normal pits of a given player
    /// </summary>
    /// <param name="p">player to get pits from</param>
    /// <returns>pits of player p</returns>
    public List<APit> GetPlayerPits(Player p){ return GetPitsForType(p, PitType.Pit); }

    /// <summary>
    /// pit list
    /// </summary>
    public List<APit> PitList { get; set; }
}