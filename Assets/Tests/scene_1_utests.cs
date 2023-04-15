using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using DistanceField;


public class scene_1_utests
{
    // A Test behaves as an ordinary method
    [Test]
    public void scene_1_utestsSimplePasses()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator scene_1_utestsWithEnumeratorPasses()
    {

        GameObject thePlayer = GameObject.Find("TestPlugin");
        if (thePlayer != null)
        {
            runDistanceQuery q = thePlayer.GetComponent<runDistanceQuery>();
            if (q != null)
            {
                Assert.AreEqual(q.ds.dist[0, 1],1);
            }
        }
        yield return null;
    }
}
