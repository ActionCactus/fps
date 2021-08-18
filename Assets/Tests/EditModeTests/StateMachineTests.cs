using FPS.Infrastructure;
using NUnit.Framework;

enum TestStateEnum
{
    a,
    b
}

public class StateTest
{
    [Test]
    public void TestAddingAcyclicRefCreatesOneWayReference()
    {
        State stateA = new State(TestStateEnum.a);
        State stateB = new State(TestStateEnum.b);
        stateA.AcyclicRef(stateB);
        Assert.AreEqual(stateA.References.Count, 1);
        Assert.AreEqual(stateB.References.Count, 0);
    }

    [Test]
    public void TestAddingCyclicRefCreatesTwoWayReference()
    {
        State stateA = new State(TestStateEnum.a);
        State stateB = new State(TestStateEnum.b);
        stateA.CyclicRef(stateB);
        Assert.AreEqual(stateA.References.Count, 1);
        Assert.AreEqual(stateB.References.Count, 1);
    }
}