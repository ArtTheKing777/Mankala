namespace Mankala;

public class MankalaBoard : IBoard
{
    private int numPits;
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

    public void MoveAmount(APit from, APit to, int amount)
    {
        to.Stones += amount;
        from.Stones -= amount;
    }

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

    public List<APit> GetPitsForType(Player p,PitType pt)
    {
        List<APit> pits = new List<APit>();
        
        foreach (APit pit in PitList)
        {
            if (pit.Type == pt && pit.Player == p) { pits.Add(pit); }
        }
        return pits;
    }

    public List<APit> GetHomePits(Player p) { return GetPitsForType(p, PitType.HomePit); }

    public List<APit> GetPlayerPits(Player p){ return GetPitsForType(p, PitType.Pit); }

    public List<APit> PitList { get; set; }
}