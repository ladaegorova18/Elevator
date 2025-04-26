using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using UnityEditor.SceneManagement;

/// <summary>
/// Test script for the MainScene in Unity in editor mode
/// </summary>
public class MainSceneTestScript
{
    // public GameObject elevator;

    [SetUp]
    public void Setup()
    {
        // This method is called before each test
        Debug.Log("Setup started");
        EditorSceneManager.OpenScene("Assets/Scenes/MainScene.unity");
    }

    [Test]
    public void ElevatorIsNotNullTest()
    {
        Debug.Log("Elevator is not null test started");
        var elevator = GameObject.FindGameObjectWithTag("elevator");
        Assert.NotNull(elevator, "Elevator is not found in the scene.");
    }

    [Test]
    public void HouseIsNotNullTest()
    {
        Debug.Log("House is not null test started");
        var house = GameObject.FindGameObjectWithTag("house");
        Assert.NotNull(house, "House is not found in the scene.");
    }
    
    [Test]
    public void SceneIsOpenedTest()
    {
        Debug.Log("Scene is opened test started");
        var scene = EditorSceneManager.GetActiveScene().name;
        Assert.AreEqual("MainScene", scene, "Scene is not opened correctly.");
    }
    
}
