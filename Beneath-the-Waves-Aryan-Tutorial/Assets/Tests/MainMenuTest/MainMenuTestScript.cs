using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class MainMenuTests
{
    private GameObject mainMenuObject;
    private MainMenu mainMenu;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject and add the MainMenu component
        mainMenuObject = new GameObject();
        mainMenu = mainMenuObject.AddComponent<MainMenu>();
    }

    [TearDown]
    public void Teardown()
    {
        // Destroy the GameObject after each test
        Object.DestroyImmediate(mainMenuObject);
    }

    [UnityTest]
    public IEnumerator PlayGame_LoadsNextScene()
    {
        // Arrange
        int initialSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Act
        mainMenu.PlayGame();
        yield return new WaitForSeconds(0.1f); // Give some time for the scene to load

        // Assert
        Assert.AreEqual(initialSceneIndex + 1, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator MultiPlayer_LoadsSecondNextScene()
    {
        // Arrange
        int initialSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Act
        mainMenu.MultiPlayer();
        yield return new WaitForSeconds(0.1f); // Give some time for the scene to load

        // Assert
        Assert.AreEqual(initialSceneIndex + 2, SceneManager.GetActiveScene().buildIndex);
    }

    [Test]
    public void QuitGame_QuitsApplication()
    {
        // Arrange
        var applicationQuitMethodCalled = false;
        Application.wantsToQuit += () => applicationQuitMethodCalled = true;

        // Act
        mainMenu.QuitGame();

        // Assert
        Assert.IsTrue(applicationQuitMethodCalled);
    }
}
