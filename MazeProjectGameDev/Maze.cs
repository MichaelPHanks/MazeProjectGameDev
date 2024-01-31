using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MazeProjectGameDev
{
    
public class Maze
    {
        public class Edges
        {
            public int to;
            public int from;
            public Edges(int from, int to)
            { 
                this.to = to;
                this.from = from;
            }
        }
        private List<List<Node>> map = new List<List<Node>>();
        

        private List<Edge> edges = new List<Edge>();

        public List<Edges> edges1 = new List<Edges>();

        private int sizeX;
        private int sizeY;
        public Maze(int sizeX, int sizeY) 
        { 
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            // Initialize map
            for (int i = 0; i < sizeX; i++)
            {
                map.Add(new List<Node>());
                for (int j = 0; j < sizeY; j++)
                {
                    map[i].Add(new Node(i, j));
                }
            }
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                if ((i + 1) % sizeY != 0)
                {
                    if (edges1.Any(obj => obj.from == i -1  && obj.to == i))
                    {
                    }
                    else
                    {
                        edges1.Add(new Edges(i, i + 1));
                    }

                }
                if (i % sizeY != 0 && i != 0)
                {
                    if (edges1.Any(obj => obj.from == i - 1 && obj.to == i))
                    {
                    }
                    else
                    {
                        edges1.Add(new Edges(i, i - 1));
                    }
                }
                if (i + sizeY < sizeX * sizeY)
                {
                    if (edges1.Any(obj => obj.from == i + sizeY && obj.to == i))
                    {
                    }
                    else
                    {
                        edges1.Add(new Edges(i, i + sizeY));
                    }

                }
                if (i - sizeY >= 0)
                {
                    if (edges1.Any(obj => obj.from == i - sizeY && obj.to == i))
                    {
                    }
                    else
                    {
                        edges1.Add(new Edges(i, i - sizeY));
                    }

                }
            }

            // Gather neighbors for each node

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (j != 0)
                    {
                        map[i][j].addNeighbor(map[i][j - 1]);

                        if (edges.Any(obj => obj.getFrom() == map[i][j - 1] && obj.getTo() == map[i][j]))
                        {
                            // Don't do anything here...
                        }
                        else 
                        {
                            edges.Add(new Edge(map[i][j], map[i][j - 1]));

                        }

                    }
                    if (j < sizeY - 1)

                    {
                        map[i][j].addNeighbor(map[i][j + 1]);
                        if (edges.Any(obj => obj.getFrom() == map[i][j + 1] && obj.getTo() == map[i][j]))
                        {
                            // Don't do anything here...
                        }
                        else
                        {
                            edges.Add(new Edge(map[i][j], map[i][j + 1]));

                        }


                    }
                    if (i < sizeX - 1)
                    {
                        map[i][j].addNeighbor(map[i + 1][j]);

                        if (edges.Any(obj => obj.getFrom() == map[i+1][j] && obj.getTo() == map[i][j]))
                        {
                            // Don't do anything here...
                        }
                        else
                        {
                            edges.Add(new Edge(map[i][j], map[i + 1][j]));

                        }


                    }

                    if (i != 0) 
                    {
                        map[i][j].addNeighbor(map[i - 1][j]);

                        if (edges.Any(obj => obj.getFrom() == map[i - 1][j] && obj.getTo() == map[i][j]))
                        {
                            // Don't do anything here...
                        }
                        else
                        {
                            edges.Add(new Edge(map[i][j], map[i - i][j]));

                        }


                    }
                }
            }
            createMaze();
            //var shuffledList = edges.OrderBy(_ => _rand.Next()).ToList();

        }
        static void ShuffleList<T>(List<T> list)
        {
            Random random = new Random();

            // Start from the end of the list and swap elements randomly
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i + 1);

                // Swap elements at randomIndex and i
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        private void createMaze()
        {
            // Randomize the list and set up the disjoint set

            DisjointSet set = new DisjointSet(sizeX,sizeY);

            ShuffleList(edges1);

            for (int i = 0; i < edges1.Count; i++)
            {
                if (set.find(edges1[i].from) != set.find(edges1[i].to))
                {
                    set.union(set.find(edges1[i].from), set.find(edges1[i].to));
                    edges1.RemoveAt(i);
                    i--;
                }
                

                
                        
            }

        }


        public List<List<Node>> getMap() { return map; }

        public void displayEdges()
        { 
            foreach (var edge in edges1) 
            {
                Console.WriteLine(edge.from + " " + edge.to);
            }
        }

        public void displayMap()
        {
            /*for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {


                    Console.Write(" " + map[i][j].getNeighbors().Count + " ");

                    if (j < sizeX - 1)
                    {
                        if (edges.Any(obj => (obj.getFrom() == map[i][j] && obj.getTo() == map[i][j + 1]) || (obj.getFrom() == map[i][j + 1] && obj.getTo() == map[i][j])))
                        {
                            Console.Write("|");

                        }
                        else 
                        {
                            Console.Write(" ");

                        }

                    }
                    

                }
                Console.WriteLine();

                for (int k = 0; k < sizeY; k++)
                {
                    if (i < sizeY - 1)
                    {
                        if (edges.Any(obj => (obj.getFrom() == map[i][k] && obj.getTo() == map[i + 1][k]) || (obj.getFrom() == map[i + 1][k] && obj.getTo() == map[i][k])))
                        {
                            Console.Write(" -  ");

                        }
                    }
                }
                Console.WriteLine();
            }*/


            for (int j = 0; j < sizeY * sizeX; j++)
            {
                Console.Write("\u25A0 ");
                if (edges1.Any(obj => (obj.from == j && obj.to == j + 1) || (obj.from == j+1 && obj.to == j)))
                {
                    Console.Write("| ");
                }
                else {
                    Console.Write(" ");
                }

                if ((j + 1) % sizeY == 0)
                {
                    Console.WriteLine();
                    for (int i = j; i < sizeY + j; i++)
                    {
                        if (edges1.Any(obj => (obj.from == i && obj.to == i + sizeX) || (obj.from == i  + sizeX && obj.to == i)))
                        {

                            Console.Write("- ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }
                    Console.WriteLine();
                }




            }
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    int currentCell = i * sizeX + j;

                    // Print the square character
                    Console.Write("\u25A0");

                    // Check for right wall
                    if (edges1.Any(obj => (obj.from == currentCell && obj.to == currentCell + 1) || (obj.from == currentCell + 1 && obj.to == currentCell)))
                    {
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();

                // Print horizontal walls
                for (int j = 0; j < sizeX; j++)
                {
                    int currentCell = i * sizeX + j;

                    // Check for bottom wall
                    if (edges1.Any(obj => (obj.from == currentCell && obj.to == currentCell + sizeX) || (obj.from == currentCell + sizeX && obj.to == currentCell)))
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

                Console.WriteLine();
            }




        }



        public class Edge
        {
            private Node from;
            private Node to;


            public Edge(Node from, Node to)
            { 
                this.from = from;
                this.to = to;
            }

            public Node getFrom() { return from; }  
            public Node getTo() { return to; }
        }


        public class Node
        {
            private List<Node> neighbors = new List<Node>();
            private int x;
            private int y;
            private int parent;
            public Node(int x, int y)
            {
                this.x = x;
                this.y = y;

            }

            public void addNeighbor(Node node)
                {
                neighbors.Add(node);
            }
            public List<Node> getNeighbors() { return neighbors;}

            public int getX() { return x; } 
            public int getY() { return y; }
            public int getParent() { return parent; }
        }
    }

    

}

    
