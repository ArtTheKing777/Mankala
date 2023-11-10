namespace Mankala;

public class MankalaBoard : IBoard
{
    private int numPits;
    public MankalaBoard(int pits = 12,int amountInPits = 4)
    {
        numPits = pits;
        List<APit> tempMankalaList = new List<APit>();
        APit? previous = null;
        for (int i = 0; i < pits+1; i++)//+1 is actually +2(for homepits) -1(for indexes)
        {
            APit current;
            if (i == pits / 2 - 1)
            {
                current = new HomePit(0, i, Player.P1);
            }
            else if (i == pits)
            {
                current = new HomePit(0, i, Player.P1);
                tempMankalaList[0].Connect(current);
            }
            else if (i > pits / 2)
            {
                current = new Pit(amountInPits, i, Player.P1);
                int opposite = i - ((numPits - i) * 2);
                tempMankalaList[opposite].ConnectCross(current);
            } 
            else current = new Pit(amountInPits, i, Player.P1); 
            
            current.Connect(previous);
            tempMankalaList.Add(current);
            previous = current;
        }
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
        Player playerturnchange = p == Player.P1?Player.P1:Player.P2;
        while (count > 0)
        {
            current = current.Next;
            if (current.Type == PitType.HomePit && current.Player == p)
            {
                playerturnchange = p;
            }
            else if (current.Stones == 0 && current.Player == p)
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
        List<APit> homePits = new List<APit>();
        
        foreach (APit pit in PitList)
        {
            if (pit.Type == pt && pit.Player == p) { homePits.Add(pit); }
        }
        return homePits;
    }

    public List<APit> GetHomePits(Player p) { return GetPitsForType(p, PitType.HomePit); }

    public List<APit> GetPlayerPits(Player p){ return GetPitsForType(p, PitType.Pit); }

    public List<APit> PitList { get; set; }
}