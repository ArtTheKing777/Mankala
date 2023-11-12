namespace TheTests;
using Mankala;

public class NewVariantTests
{
    private NewVariantBoard testBoard;
    private NewVariantRules rules;
    
    [SetUp]
    public void SetUp()
    {
        testBoard = new ();//wat 
        rules = new ();
    }
    [Test]
    public void TestTheWinner()
    {
        int outcome = rules.DetermineWinner(testBoard);
        Assert.AreEqual(0,outcome);//if the game just started there should be a draw
    }
    [Test]
    public void TestTheEndGame()
    {
        bool outcome = rules.EndGame(testBoard);
        Assert.IsFalse(outcome);//if the game just started there should moves that are possible
    }
    [Test]
    public void TestTheGetMovesQuantity()
    {
        int outcomeP1 = rules.GetMoves(testBoard,Player.P1).Count;
        int outcomeP2 = rules.GetMoves(testBoard,Player.P2).Count;
        int possibleMoves = (testBoard.PitList.Count - 2) / 2;
        Assert.AreEqual(possibleMoves,outcomeP1);//if the game just started there should pits/2 moves foreach player
        Assert.AreEqual(possibleMoves,outcomeP2);
    }
    [Test]
    public void TestTheDoMove()
    {
        Player outcomeP1 = testBoard.DoMove(Player.P1, 1);
        testBoard = new ();//reset
        Player outcomeP2 = testBoard.DoMove(Player.P2, 1);
        testBoard = new ();//reset
        Assert.AreEqual(Player.P2,outcomeP1);//if the game just started there should be a change in players if 1 gets moved
        Assert.AreEqual(Player.P1,outcomeP2);
    }
    [Test]
    public void TestTheGetPlayerPits()
    {
        int outcomeP1 = testBoard.GetPlayerPits(Player.P1).Count;
        int outcomeP2 = testBoard.GetPlayerPits(Player.P2).Count;
        int possibleMoves = (testBoard.PitList.Count - 2) / 2;
        Assert.AreEqual(possibleMoves,outcomeP1);//if the game just started there should pits/2 foreach player
        Assert.AreEqual(possibleMoves,outcomeP2);
    }
    [Test]
    public void TestTheGetPlayerHomePit()
    {
        int outcomeP1 = testBoard.GetHomePits(Player.P1).Count;
        int outcomeP2 = testBoard.GetHomePits(Player.P2).Count;
        Assert.AreEqual(1,outcomeP1);//if the game just started there should 1 foreach player
        Assert.AreEqual(1,outcomeP2);
    }
    [Test]
    public void TestTheMoveAmount()
    {
        int exepectedPit1 = testBoard.PitList[0].Stones - 5;
        int exepectedPit2 = testBoard.PitList[1].Stones + 5;
        testBoard.MoveAmount(testBoard.PitList[0],testBoard.PitList[1],5);
        int stonesPit1 = testBoard.PitList[0].Stones;
        int stonesPit2 = testBoard.PitList[1].Stones;
        Assert.AreEqual(exepectedPit1,stonesPit1);//should move 5 from pit 1 to pit 2
        Assert.AreEqual(exepectedPit2,stonesPit2);
        testBoard = new ();
    }
}