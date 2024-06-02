using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MedKitTests
{
    // Test the heal method to gain health
    [Test]
    public void MedKitHealTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<Player>();

        player.HP = player.getMAXHP();
        player.takeDamage(50f);
        player.healDamage(30f);

        Assert.AreEqual(80f, player.getHP());
    }

    // Test the heal method should not increase health past the max on 100f
    [Test]
    public void MedKitMaxHealTest()
    {
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<Player>();

        player.HP = player.getMAXHP();
        player.takeDamage(10f);
        player.healDamage(30f);

        Assert.AreEqual(player.getMAXHP(), player.getHP());
    }
}
