namespace Mankala;

public class MankalaBoard : IBoard
{
    public MankalaBoard(int pits = 12,int amountInPits = 4)
    {
        List<APit> tempMankalaList = new List<APit>();
        APit? previous = null;
        for (int i = 0; i < pits+1; i++)//+1 is actually +2(for homepits) -1(for indexes)
        {
            APit current;
            if (i == pits / 2 - 1) { current = new HomePit(0, i, Player.P1); }
            else if (i == pits) { current = new HomePit(0, i, Player.P1); }
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

    public void DoMove()
    {
        throw new NotImplementedException();
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