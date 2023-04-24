using System.Collections;
using TestableScripts;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

public class EnemyTest
{
    
    private TestableEnemy InitializeEnemy()
    {
        GameObject testObject = new GameObject();
        TestableEnemy testableEnemy = testObject.AddComponent<TestableEnemy>();
        
        return testableEnemy;
    }
    
    [UnityTest]
    public IEnumerator EnemyIsAliveTest()
    {
        TestableEnemy testableEnemy = InitializeEnemy();
        
        Assert.AreEqual(true, testableEnemy.GetIsAlive());
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator EnemyIsDeadWithoutWaitingTest()
    {
        TestableEnemy testableEnemy = InitializeEnemy();
        testableEnemy.StartCoroutine(testableEnemy.Die());

        Assert.AreEqual(true, testableEnemy.GetIsAlive());
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnemyIsDeadWithWaitingTest()
    {
        TestableEnemy testableEnemy = InitializeEnemy();
        testableEnemy.StartCoroutine(testableEnemy.Die());

        yield return new WaitForSeconds(5);

        Assert.AreEqual(false, testableEnemy.GetIsAlive());
    }
}
