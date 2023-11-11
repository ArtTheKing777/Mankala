using NUnit.Framework;

namespace Tests;

public class Tests
{
    [Test]
    public void TestTest()
    {
        IBoard boardToTest = new MankalaBoard();
        Assert.AreEqual(0,boardToTest);
    }
}