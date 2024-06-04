using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MainMenuTest
{
    private GameObject mainMenuGameObject;
    private MainMenu mainMenu;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject and add the MainMenu component to it
        mainMenuGameObject = new GameObject();
        mainMenu = mainMenuGameObject.AddComponent<MainMenu>();
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after each test
        Object.Destroy(mainMenuGameObject);
    }

    [UnityTest]
    public IEnumerator PlayGame_LoadsNextScene()
    {
        // Arrange: Add a dummy scene to the build settings for testing
        SceneManager.LoadScene(0);
        yield return null;

        // Act: Call the PlayGame method
        mainMenu.PlayGame();
        yield return null;

        // Assert: Check if the next scene is loaded
        Assert.AreEqual(1, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator MultiPlayer_LoadsMultiplayerScene()
    {
        // Arrange: Add a dummy scene to the build settings for testing
        SceneManager.LoadScene(0);
        yield return null;

        // Act: Call the MultiPlayer method
        mainMenu.MultiPlayer();
        yield return null;

        // Assert: Check if the multiplayer scene is loaded
        Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);
    }

    [Test]
    public void QuitGame_LogsQuitMessage()
    {
        // Arrange: Use a LogAssert to capture logs
        LogAssert.Expect(LogType.Log, "Quit");

        // Act: Call the QuitGame method
        mainMenu.QuitGame();

        // Assert: Check if the quit message was logged
        LogAssert.NoUnexpectedReceived();
    }
}
