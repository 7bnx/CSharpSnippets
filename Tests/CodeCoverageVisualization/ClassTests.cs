namespace CodeCoverageVisualization;

[TestClass]
public class ClassTests
{
  [TestMethod]
  public void TestMethod1_Pass()
  {
    new Class().Method1();
    Assert.AreEqual(1, 1);
  }

  [TestMethod]
  [Ignore]
  public void TestMethod1_NotPass()
  {
    new Class().Method1();
    Assert.AreEqual(3, 1);
  }
}