namespace Mankala;

public class MankalaBoard : IBoard
{
    public MankalaBoard(int pits = 6,int amountInPits = 4)
    {
        List<APit> tempMankalaList = new List<APit>();
        p1HomePit
    }

    public void MoveAmount(APit from, APit to, int amount)
    {
        to.Stones += amount;
        from.Stones -= amount;
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