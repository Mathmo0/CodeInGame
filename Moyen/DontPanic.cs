using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    public enum Action
    {
        WAIT, 
        BLOCK
    }

    public class Elevator
    {
        int elevatorFloor;
        int elevatorPos;

        public Elevator(int ef, int ep)
        {
            elevatorFloor = ef;
            elevatorPos = ep;
        }

        public int getPos(){ return elevatorPos; }
        public int getFloor(){ return elevatorFloor; }
    }

    public static int distance(int a, int b)
    {
        if(a > b)
            return a - b;
        else
            return b - a;
    }

    static void Main(string[] args)
    {
        string res = Action.WAIT.ToString();

        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int nbFloors = int.Parse(inputs[0]); // number of floors
        int width = int.Parse(inputs[1]); // width of the area
        int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
        int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
        int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
        int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
        int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
        int spawnPos = 0;
        bool once = true;

        int nbElevators = int.Parse(inputs[7]); // number of elevators
        List<Elevator> elevators = new List<Elevator>();
        for (int i = 0; i < nbElevators; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
            int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
            elevators.Add(new Elevator(elevatorFloor, elevatorPos));
        }

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
            int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
            if(once)
            {
                spawnPos = clonePos;
                once = false;
            }
            string direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            if((clonePos == -1 && cloneFloor == -1) || clonePos == spawnPos || elevators.Any<Elevator>(x => x.getFloor() == cloneFloor && x.getPos() == clonePos))
                res = Action.WAIT.ToString();
            else if(cloneFloor == exitFloor)
                res = distance(direction == "RIGHT" ? clonePos + 1 : clonePos - 1, exitPos) < distance(clonePos, exitPos) ? Action.WAIT.ToString() : Action.BLOCK.ToString();
            else
            {
                Console.Error.WriteLine("salut");
                Elevator el = elevators.Find(x => x.getFloor() == cloneFloor);
                if(el == null)
                    res = Action.WAIT.ToString();
                else
                    res = distance(direction == "RIGHT" ? clonePos + 1 : clonePos - 1, el.getPos()) < distance(clonePos, el.getPos()) ? Action.WAIT.ToString() : Action.BLOCK.ToString();
            }

            Console.WriteLine(res); // action: WAIT or BLOCK

        }
    }
}