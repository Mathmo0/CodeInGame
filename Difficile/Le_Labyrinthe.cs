using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;

class Player
{
    const int _INF_ = 9999;

    public enum Result
    {
        UP, 
        DOWN,
        LEFT,
        RIGHT
    }

    public enum State
    {
        EXPLORE,
        BACK,
        FINAL
    }

    static Result Direction(Cell A, Cell B)
    {
        if(B.x == A.x - 1)
        {
            return Result.UP;
        }
        else if(B.x == A.x + 1)
        {
            return Result.DOWN;
        }
        else if(B.y == A.y - 1)
        {
            return Result.LEFT;
        }
        else
        {
            return Result.RIGHT;
        }
    }

    static Result Direction((int ax, int ay) A, (int bx, int by) B)
    {
        if(B.bx == A.ax - 1)
        {
            return Result.UP;
        }
        else if(B.bx == A.ax + 1)
        {
            return Result.DOWN;
        }
        else if(B.by == A.ay - 1)
        {
            return Result.LEFT;
        }
        else
        {
            return Result.RIGHT;
        }
    }

    public class Cell
    {
        public int x;
        public int y;
        public Cell Parent;
        public int g;
        public int h;
        public char type;

        public Cell(int xp, int yp, Cell p)
        {
            x = xp;
            y = yp;
            Parent = p;
            g = 0;
            h = 0;
        }

        public Cell()
        {
            x = -1;
            y = -1;
            Parent = null;
            g = 0;
            h = 0;
        }

        public string ToString()
        {
            return x + " " + y;
        }

        public int f()
        {
            return g + h;
        }
    }

    class CellComparer : IComparer<Cell>
    {
        public int Compare(Cell a, Cell b)
        {
            int fComparison = a.f().CompareTo(b.f());
            if (fComparison == 0)
            {
                int hComparison = a.h.CompareTo(b.h);
                if (hComparison == 0)
                {
                    int xComparison = a.x.CompareTo(b.x);
                    if (xComparison == 0)
                    {
                        return a.y.CompareTo(b.y);
                    }
                    return xComparison;
                }
                return hComparison;
            }
            return fComparison;
        }
    }

    static int Heuristic(Cell start, Cell goal)
    {
        return Math.Abs(start.x - goal.x) + Math.Abs(start.y - goal.y);
    }

    static bool IsVoisin(Cell current, int row, int col)
    {
        if ((row == current.x - 1 && col == current.y) ||
        (row == current.x + 1 && col == current.y) ||
        (row == current.x && col == current.y - 1) ||
        (row == current.x && col == current.y + 1))
        {
            return true;
        }

        return false;
    }

    static bool IsCoins(Cell current, int row, int col)
    {
        if((row == current.x - 1 && col == current.y - 1) ||
            (row == current.x - 1 && col == current.y + 1) ||
            (row == current.x + 1 && col == current.y - 1) ||
            (row == current.x + 1 && col == current.y + 1))
        {
            return true;
        }
        return false;
    }

    // Fonction pour choisir la direction, on test les possibilité en tournant dans les aiguilles d'une montre
    static Result ChooseDirection(Result direction, List<Cell> VOISINS, int KR, int KC, Cell goal) 
    {
        List<Result> ValsList = new List<Result>();
        Result[] vals;
        if(VOISINS.Any(cell => cell.x == KR + 1 && cell.y == KC))
        {
            ValsList.Add(Result.DOWN);
        }
        if(VOISINS.Any(cell => cell.x == KR && cell.y == KC + 1)) // a droite ?
        {
            ValsList.Add(Result.RIGHT);
        }
        if(VOISINS.Any(cell => cell.x == KR - 1 && cell.y == KC)) 
        {
            ValsList.Add(Result.UP);
        }
        if(VOISINS.Any(cell => cell.x == KR && cell.y == KC - 1))
        {
            ValsList.Add(Result.LEFT);
        }

        if(ValsList.Where(v => v != GetOpp(direction)).Count() != 0)
        {
            vals = ValsList.Where(v => v != GetOpp(direction)).ToArray();
        }
        else
        {
            vals = ValsList.ToArray();
        }
        Random ran = new Random();
        Result res = vals[ran.Next(vals.Length)];
        Console.Error.WriteLine("res temp = " + res.ToString());
        
        // Vers la droite
        switch(direction) 
        {
            case Result.RIGHT:
                if(VOISINS.Any(cell => cell.x == KR + 1 && cell.y == KC))
                {
                    res = Result.DOWN;
                }
                else if(VOISINS.Any(cell => cell.x == KR && cell.y == KC + 1)) // a droite ?
                {
                    res = Result.RIGHT;
                }
                else  if(VOISINS.Any(cell => cell.x == KR - 1 && cell.y == KC)) // a droite ?
                {
                    res = Result.UP;
                }
                else if(VOISINS.Any(cell => cell.x == KR && cell.y == KC - 1))
                {
                    res = Result.LEFT;
                }
                break;
            case Result.LEFT:
                if(VOISINS.Any(cell => cell.x == KR - 1 && cell.y == KC)) // a droite ?
                {
                    res = Result.UP;
                }
                else if(VOISINS.Any(cell => cell.x == KR && cell.y == KC - 1))
                {
                    res = Result.LEFT;
                }
                else if(VOISINS.Any(cell => cell.x == KR + 1 && cell.y == KC))
                {
                    res = Result.DOWN;
                }
                else if(VOISINS.Any(cell => cell.x == KR && cell.y == KC + 1)) // a droite ?
                {
                    res = Result.RIGHT;
                }
                break;
            case Result.UP:
                if(VOISINS.Any(cell => cell.x == KR && cell.y == KC + 1)) // a droite ?
                {
                    res = Result.RIGHT;
                }
                else if(VOISINS.Any(cell => cell.x == KR - 1 && cell.y == KC)) // a droite ?
                {
                    res = Result.UP;
                }
                else if(VOISINS.Any(cell => cell.x == KR && cell.y == KC - 1))
                {
                    res = Result.LEFT;
                }
                else if(VOISINS.Any(cell => cell.x == KR + 1 && cell.y == KC))
                {
                    res = Result.DOWN;
                }
                break;
            case Result.DOWN:
                if(VOISINS.Any(cell => cell.x == KR && cell.y == KC - 1))
                {
                    res = Result.LEFT;
                }
                else if(VOISINS.Any(cell => cell.x == KR + 1 && cell.y == KC))
                {
                    res = Result.DOWN;
                }
                else if(VOISINS.Any(cell => cell.x == KR && cell.y == KC + 1)) // a droite ?
                {
                    res = Result.RIGHT;
                }
                else if(VOISINS.Any(cell => cell.x == KR - 1 && cell.y == KC)) // a droite ?
                {
                    res = Result.UP;
                }
                break;
        }

        return res;
    }

    // Avoir la direction opposé
    static Result GetOpp(Result res)
    {
        switch(res)
        {
            case Result.RIGHT:
                return Result.LEFT;
            
            case Result.LEFT:
                return Result.RIGHT;

            case Result.UP:
                return Result.DOWN;

            case Result.DOWN:
                return Result.UP;

            default:
                return Result.RIGHT;
        }
    }

    static (int a, int b) GetPreviousCell(Result Anciennedirection, int KR, int KC)
    {
        switch(Anciennedirection)
        {
            case Result.RIGHT:
                return (KR, KC - 1);
            case Result.LEFT:
                return (KR, KC + 1);
            case Result.UP:
                return (KR + 1, KC);
            case Result.DOWN:
                return (KR - 1, KC);
            default:
                return (-1, -1);
        }
    }

    static (int a, int b) GetNextCell(Result Anciennedirection, int KR, int KC)
    {
        switch(Anciennedirection)
        {
            case Result.RIGHT:
                return (KR, KC + 1);
            case Result.LEFT:
                return (KR, KC - 1);
            case Result.UP:
                return (KR - 1, KC);
            case Result.DOWN:
                return (KR + 1, KC);
            default:
                return (-1, -1);
        }
    }

    static List<Cell> GetNeighbors(Cell curr, List<Cell> maze)
    {
        List<Cell> neighbors = new List<Cell>();

        foreach (var direction in new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
        {
            int newRow = curr.x + direction.Item1;
            int newCol = curr.y + direction.Item2;

            Cell neighbor = maze.FirstOrDefault(cell => cell.x == newRow && cell.y == newCol);
            if (neighbor != null && neighbor.type != '#' && neighbor.type != '?')
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    static List<Cell> ReconstructPath(Cell current)
    {
        List<Cell> totalPath = new List<Cell> { current };
        while (current.Parent != null)
        {
            current = current.Parent;
            totalPath.Add(current);
        }
        totalPath.Reverse();
        return totalPath;
    }

    static List<Cell> AStar(Cell start, Cell goal, List<Cell> maze)
    {
        SortedSet<Cell> openSet = new SortedSet<Cell>( new CellComparer());
        HashSet<Cell> closedSet = new HashSet<Cell>();

        start.g = 0;
        start.h = Heuristic(start, goal);
        openSet.Add(start);

        while(openSet.Count() > 0)
        {
            Console.Error.WriteLine("open count = " + openSet.Count());
            Cell current = openSet.Min;
            if(current.x == goal.x && current.y == goal.y)
            {
                return ReconstructPath(current);
            }

            openSet.Remove(current);
            if(closedSet.Contains(current))
            {
                continue;
            }
            closedSet.Add(current);

            Console.Error.WriteLine("nb voisins = " + GetNeighbors(current, maze).Count());

            foreach(var n in GetNeighbors(current, maze))
            {
                if(closedSet.Contains(n))
                {
                    continue;
                }
                // if(!closedSet.Any(cell => cell.x == n.x && cell.y == n.y) || 
                //         (openSet.Any(cell => cell.x == n.x && cell.y == n.y) && n.g >= openSet.FirstOrDefault(cell => cell.x == n.x && cell.y == n.y).g))
                // {
                //     int tentative = current.g + 1;
                // }


                int tentative = current.g + 1;
                // Console.Error.WriteLine("g = " + tentative);
                if(!openSet.Contains(n) || tentative < n.g) //  !openSet.Contains(n) ||
                {
                    n.Parent = current;
                    n.g = tentative;
                    n.h = n.g + Heuristic(n, goal);

                    if(!openSet.Any(cell => cell.x == n.x && cell.y == n.y))
                    {
                        openSet.Add(n);
                    }
                }
                // if(tentative >= n.g)
                // {
                //     continue;
                // }
                // else if(!openSet.Any(cell => cell.x == n.x && cell.y == n.y))//.Contains(n))
                // {
                //     n.h = Heuristic(n, goal);
                //     openSet.Add(n);
                // }

                // n.Parent = current;
                // n.g = tentative;
            }
        }

        return null;
    }

    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int R = int.Parse(inputs[0]); // number of rows.
        int C = int.Parse(inputs[1]); // number of columns.
        int A = int.Parse(inputs[2]); // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.
        Cell temp = new Cell();
        bool goalFind = false;
        State reverse = State.EXPLORE;
        bool goalAttein = false;

        List<Cell> MAZE = new();

        List<Cell> OPEN = new List<Cell>();
        List<Cell> CLOSE = new List<Cell>();

        Queue<Cell> PATHRESULT = new Queue<Cell>();

        Cell goal = new Cell(-1, -1, null);
        Cell start = new Cell(-1, -1, null);
        Cell actuel = new Cell(-1, -1, null);

        Dictionary<(int, int), int> closed = new();

        int KR = 0;
        int KC = 0;

        Result DerniereDirection = Result.RIGHT;
        Result DirectionActuel = Result.RIGHT;

        Queue<(int, int)> queue = new Queue<(int, int)>();

        // game loop
        while (true)
        {
            List<Cell> VOISINS = new List<Cell>();
            List<Cell> COINS = new List<Cell>();

            inputs = Console.ReadLine().Split(' ');
            KR = int.Parse(inputs[0]); // row where Rick is located.
            KC = int.Parse(inputs[1]); // column where Rick is located.
                 
            actuel = new Cell(KR, KC, null);

            for (int i = 0; i < R; i++)
            {
                string ROW = Console.ReadLine(); // C of the characters in '#.TC?' (i.e. one line of the ASCII maze).
                Console.Error.WriteLine(ROW);
                for(int j = 0; j < C; j++)
                {
                    // On regarde si la Cell existe dans le maze
                    if(ROW[j] != '#' && ROW[j] != '?' && !MAZE.Any(cell => cell.x == i && cell.y == j))
                    {
                        Cell toAdd = new Cell(i, j, null);
                        toAdd.type = ROW[j];
                        MAZE.Add(toAdd);
                    }

                    // On recupere les voisins à actuel
                    if(ROW[j] != '#' && IsVoisin(actuel, i, j))
                    {
                        VOISINS.Add(new Cell(i, j, null));
                    }

                    // On récupère les coins
                    if(ROW[j] != '#' && IsCoins(actuel, i, j))
                    {
                        COINS.Add(new Cell(i, j, null));
                    }

                    // On regarde si on a trouvé le goal
                    if(!goalFind)
                    {
                        switch(ROW[j])
                        {
                            case '?':
                                if(!goalFind)
                                {
                                    if(goal.x == -1 || goal.y == -1)
                                    {
                                        goal = new Cell(i, j, null);
                                    }
                                    else if(Heuristic(actuel, goal) > Heuristic(actuel, new Cell(i, j, null)))
                                    {
                                        goal = new Cell(i, j, null);
                                    }
                                }
                                break;

                            case 'C':
                                goal = new Cell(i, j, null);
                                goalFind = true;
                                break;
                            
                            case 'T':
                                start = new Cell(i, j, null);
                                break;
                        } 
                    }               
                }
            }

            if(!goalAttein)
            {
                // Si le goal est attein on fait AStar et on passe en mode retour
                if(goalFind && KR == goal.x && KC == goal.y)
                {
                    goalAttein = true;
                    // Faire AStar pour revenir en arriere
                    Console.Error.WriteLine("Arrived");
                    PATHRESULT = new Queue<Cell>(AStar(goal, start, MAZE));
                    Console.Error.WriteLine("Algo complete");
                    var Avoid = PATHRESULT.Dequeue();
                    DirectionActuel = Direction(actuel, PATHRESULT.Dequeue());
                }
                else
                {
                    // Si on doit parcourir le maze dans lautre sens pour revenir au goal
                    if(reverse == State.BACK && VOISINS.Any(cell => cell.x == start.x && cell.y == start.y))
                    {
                        DirectionActuel = ChooseDirection(DerniereDirection, VOISINS.Where(cell => cell.x != start.x || cell.y != start.y).ToList(), KR, KC, goal); 
                        closed.Clear();
                        reverse = State.FINAL;
                    }
                    else if(reverse == State.EXPLORE && goalFind && VOISINS.Any(cell => cell.x == goal.x && cell.y == goal.y)) // On a attein le goal on repars pour visiter tout le maze
                    {
                        DirectionActuel = ChooseDirection(DerniereDirection, VOISINS.Where(cell => cell.x != goal.x || cell.y != goal.y).ToList(), KR, KC, goal);
                        closed.Clear();
                        reverse = State.BACK;
                    }
                    else if(goalFind && VOISINS.Any(cell => cell.x == goal.x && cell.y == goal.y))
                    {
                        DirectionActuel = Direction(actuel, goal);
                    }
                    else if(VOISINS.Count() == 1)
                    {
                        DirectionActuel = Direction(actuel, VOISINS.FirstOrDefault());
                    }
                    else 
                    {
                        COINS = COINS.Where(coincoin => 
                                            {
                                                int nbVoisins = VOISINS.Count(v => IsVoisin(coincoin, v.x, v.y));
                                                return nbVoisins >= 2;
                                            }).ToList();

                        // Si c'est une bifurcation alors on ajoute au closed la ou on ira
                        if(VOISINS.Count() > 2 && COINS.Count() == 0)
                        {
                            // Ajout de la ou on vient
                            var fin = GetPreviousCell(DerniereDirection, KR, KC);
                            if(!closed.ContainsKey(fin))
                            {
                                closed[fin] = 1;
                            }
                            else
                            {
                                closed[fin]++;
                            }

                            // On regarde parmis les voisins les possibilités
                            // Si le nombre de closed est sup à 0 en EXCLUANT le potentiel close précédeant,
                            if(VOISINS.Where(cell => (cell.x != fin.a || cell.y != fin.b) && closed.ContainsKey((cell.x, cell.y))).Count() != 0)
                            {
                                if(closed[(fin.a, fin.b)] < 2) // On peut aller en arriere
                                {
                                    Console.Error.WriteLine("on peut aller en arriere");
                                    DirectionActuel = GetOpp(DerniereDirection);
                                }
                                else
                                {
                                    if(VOISINS.Where(cell => !closed.ContainsKey((cell.x, cell.y))).Count() != 0) // On va vers les closed à 0
                                    {
                                        DirectionActuel = ChooseDirection(DerniereDirection, VOISINS.Where(cell => !closed.ContainsKey((cell.x, cell.y))).ToList(), KR, KC, goal);
                                    }
                                    else if(VOISINS.Where(cell => closed.ContainsKey((cell.x, cell.y)) && closed[(cell.x, cell.y)] < 2).Count() != 0) // sinon vers un closed à 1
                                    {
                                        DirectionActuel = ChooseDirection(DerniereDirection, VOISINS.Where(cell => closed.ContainsKey((cell.x, cell.y)) && closed[(cell.x, cell.y)] < 2).ToList(), KR, KC, goal);
                                    }
                                    else // sinon on va vers le premier voisin de dispo
                                    {
                                        DirectionActuel = ChooseDirection(DerniereDirection, VOISINS, KR, KC, goal); 
                                    }
                                }
                            }
                            else
                            {
                                DirectionActuel = ChooseDirection(DerniereDirection, VOISINS, KR, KC, goal);
                            }

                            // On rajoute le debut du tunnel aux closed
                            var debut = GetNextCell(DirectionActuel, KR, KC);
                            if(!closed.ContainsKey(debut))
                            {
                                closed[debut] = 1;
                            }
                            else
                            {
                                closed[debut]++;
                            }
                        }
                        else if(VOISINS.Count() == 2 && COINS.Count() == 0)
                        {
                            var prev = GetPreviousCell(DerniereDirection, KR, KC);
                            DirectionActuel = Direction(actuel, VOISINS.Where(c => c.x != prev.a || c.y != prev.b).FirstOrDefault()); 
                        }
                        else
                        {
                            if(VOISINS.Where(cell => closed.ContainsKey((cell.x, cell.y)) && closed[(cell.x, cell.y)] > 1).Count() != 0)
                            {
                                DirectionActuel = ChooseDirection(DerniereDirection, VOISINS.Where(cell => !closed.ContainsKey((cell.x, cell.y)) || (closed.ContainsKey((cell.x, cell.y)) && closed[(cell.x, cell.y)] < 2)).ToList(), KR, KC, goal);
                            }
                            else
                            {
                                DirectionActuel = ChooseDirection(DerniereDirection, VOISINS, KR, KC, goal);
                            }
                        }
                    }
                }
            }
            else
            {
                DirectionActuel = Direction(actuel, PATHRESULT.Dequeue());
            }

            DerniereDirection = DirectionActuel;

            Console.WriteLine(DerniereDirection);
        }
    }
}