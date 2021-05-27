using System;

namespace KATA_MarsRover
{
    public class RoverControls
    {

        static void Main() { }

        bool[,] map = new bool[,]{
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false },
        };

        int x = 0;
        int y = 0;
        int direction = 0;
        char[] directions = new char[] { 'N', 'E', 'S', 'W' };

        public string GetPosition() { return x + ";" + y; }

        public void SetPosition(int x, int y)
        {
            this.x = x; this.y = y;
        }

        public void ChangeMap(bool[,] map)
        {
            this.map = map;
        }

        public void SetDirection(char direction)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == direction) { this.direction = i; break; }
            }
        }

        public string SendCommands(params char[] commands)
        {
            string obstacleMessage = "OK";
            foreach (char c in commands)
            {
                switch (c)
                {
                    case 'f':
                        obstacleMessage = Move(true);
                        break;
                    case 'b':
                        obstacleMessage = Move(false);
                        break;
                    case 'l':
                        Rotate(false);
                        break;
                    case 'r':
                        Rotate(true);
                        break;
                }
                if (obstacleMessage != "OK") { break; }
            }
            return obstacleMessage;
        }

        string Move(bool forward)
        {
            int offSetX = 0;
            int offSetY = 0;
            switch (direction)
            {
                case 0: offSetY = -1; break;
                case 1: offSetX = 1; break;
                case 2: offSetY = 1; break;
                case 3: offSetX = -1; break;
            }
            if (!forward) { offSetX *= -1; offSetY *= -1; }
            int newX = x + offSetX; int newY = y + offSetY;
            //Wrapping
            if (newY >= map.GetLength(0)) { newY = 0; } else if (newY < 0) { newY = map.GetLength(0) - 1; }
            if (newX >= map.GetLength(1)) { newX = 0; } else if (newX < 0) { newX = map.GetLength(1) - 1; }
            //Collision Detection
            if (map[newY, newX]) { return newX + ";" + newY; }
            //Movement
            x = newX; y = newY;
            return "OK";
        }

        void Rotate(bool right)
        {
            direction += right ? 1 : -1;
            if (direction > 3) { direction = 0; } else if (direction < 0) { direction = 3; }
        }

    }
}
