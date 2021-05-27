using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;


namespace MarsRoverTest
{
    [TestClass]
    public class UnitTest1
    {

        void SetBaseMap(KATA_MarsRover.RoverControls rover) {
            rover.ChangeMap(new bool[,]{
                    { false, false, false, false, false },
                    { false, true, true, false, true },
                    { false, true, false, false, false },
                    { false, true, false, false, true  },
                    { false, true, false, false, true  },
                });
        }
        [TestMethod]
        public void SetupAndMovementTest()
        {
            KATA_MarsRover.RoverControls rover = new KATA_MarsRover.RoverControls();
            SetBaseMap(rover);
            rover.SetPosition(0, 1);
            rover.SetDirection('S');
            string response = rover.SendCommands('f');
            Assert.AreEqual("OK", response);
            Assert.AreEqual("0;2", rover.GetPosition());
        }

        [TestMethod]
        public void RotateTest()
        {
            KATA_MarsRover.RoverControls rover = new KATA_MarsRover.RoverControls();
            SetBaseMap(rover);
            rover.SetPosition(0, 0);
            rover.SetDirection('W');
            string response = rover.SendCommands('l','l','f');
            Assert.AreEqual("OK", response);
            Assert.AreEqual("1;0", rover.GetPosition());
        }

        [TestMethod]
        public void ObstacleTest()
        {
            KATA_MarsRover.RoverControls rover = new KATA_MarsRover.RoverControls();
            SetBaseMap(rover);
            rover.SetPosition(3, 3);
            rover.SetDirection('E');
            string response = rover.SendCommands('f', 'f', 'f', 'l', 'f');
            Assert.AreEqual("4;3", response);
            Assert.AreEqual("3;3", rover.GetPosition());
        }

        [TestMethod]
        public void WrappingTest()
        {
            KATA_MarsRover.RoverControls rover = new KATA_MarsRover.RoverControls();
            SetBaseMap(rover);
            rover.SetPosition(0, 0);
            rover.SetDirection('W');
            string response = rover.SendCommands('f');
            Assert.AreEqual("OK", response);
            Assert.AreEqual("4;0", rover.GetPosition());
        }

        [TestMethod]
        public void ExplorationTest()
        {
            KATA_MarsRover.RoverControls rover = new KATA_MarsRover.RoverControls();
            SetBaseMap(rover);
            rover.SetPosition(0, 1);
            rover.SetDirection('N');
            string response = rover.SendCommands('r','r','f', 'f', 'f', 'f', 'l', 'f', 'f', 'b', 'r', 'f');
            Assert.AreEqual("1;1", response);
            Assert.AreEqual("1;0", rover.GetPosition());
            response = rover.SendCommands('l', 'f', 'f', 'r', 'f', 'f', 'r', 'b', 'b');
            Assert.AreEqual("OK", response);
            Assert.AreEqual("0;2", rover.GetPosition());
        }

    }
}
