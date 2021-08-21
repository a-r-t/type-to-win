/*
 * Alright, so I can't even run this test
 * Because Visual Studio 2019 is bugged and keeps deleting MSTest from my dependencies when trying to run tests
 * Idk what's going on...
 */

/* 
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using type_to_win.Sprites;
using type_to_win.Sprites.Enemies;

namespace type_to_win.Tests
{
    [TestClass]
    public class TestEnemy
    {
        Enemy enemy = new EnemyTest(new string[] { "test" }, 5, new Rectangle(0, 0, 0, 0), "test", Vector2.Zero);

        [TestMethod]
        public void TestGetMovement()
        {
            Assert.AreEqual(enemy.GetMovement(), Vector2.Zero);
        }

        [TestMethod]
        public void TestGetWordPosition()
        {
            Assert.AreEqual(enemy.GetWordPosition(), Vector2.Zero);
        }

        [TestMethod]
        public void TestIsActive()
        {
            Assert.AreEqual(enemy.IsActive(1), false);
            Assert.AreEqual(enemy.IsActive(7), true);
            enemy.CurrentWordIndex = 1;
            Assert.AreEqual(enemy.IsActive(1), false);

        }

        [TestMethod]
        public void TestIsDead()
        {
            Assert.AreEqual(enemy.isDead(), false);
            enemy.CurrentWordIndex = 1;
            Assert.AreEqual(enemy.isDead(), true);
            enemy.CurrentWordIndex = 0;
        }

        class EnemyTest : Enemy
        {
            public EnemyTest(string[] words, int spawnAtTick, Rectangle validRectangleBounds, string spriteSheetName, Vector2 spriteSize)
                : base(words, spawnAtTick, validRectangleBounds, spriteSheetName, spriteSize)
            {

            }

            public override Vector2 GetMovement()
            {
                return Vector2.Zero;
            }

            public override Vector2 GetWordPosition()
            {
                return Vector2.Zero;
            }

            public override void LoadAnimations()
            {
                return;
            }
        }
    }
}
*/