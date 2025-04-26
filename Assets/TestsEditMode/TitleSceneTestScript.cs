using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using UnityEditor.SceneManagement;

public class TitleSceneTestScript
{
    [SetUp]
    public void Setup()
    {
        // This method is called before each test
        Debug.Log("Setup started");
        EditorSceneManager.OpenScene("Assets/Scenes/TitleScreen.unity");
    }

    [Test]
    public void SceneIsOpenedTest()
    {
        Debug.Log("Scene is opened test started");
        var scene = EditorSceneManager.GetActiveScene().name;
        Assert.AreEqual("TitleScreen", scene, "Scene is not opened correctly.");
    }
}
