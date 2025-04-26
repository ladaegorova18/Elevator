using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayModeTests
{
    [SetUp]
    public void Setup()
    {
        Debug.Log("Setup started");
        SceneManager.LoadScene("TitleScreen");
    }

    [UnityTest]
    public IEnumerator TitleScreenIsOpenedTest()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("TitleScreen is opened test started");
        Assert.IsTrue(SceneManager.GetActiveScene().name == "TitleScreen", "TitleScreen is not opened correctly.");
        yield return null;
    }

    [UnityTest]
    public IEnumerator StartButtonIsPressedTest()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Start Button is pressed test started");
        var startButtonObj = GameObject.Find("StartButton");
        Assert.IsNotNull(startButtonObj, "Start button object not found in the scene.");

        var startButton = startButtonObj.GetComponent<Button>();
        Assert.IsNotNull(startButton, "Start button not found in the scene.");

        startButton.onClick.Invoke();
        
        yield return new WaitForSeconds(1f);
        Debug.Log(SceneManager.GetActiveScene().name);

        Assert.IsTrue(SceneManager.GetActiveScene().name == "MainScene", "MainScene was not opened correctly.");
        yield return null;
    }
}
