using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MazeProjectGameDev
{

    public class Maze
    {

        public class GraphNode
        {
            private GraphNode top;
            private GraphNode bottom;
            private GraphNode left;
            private GraphNode right;
            public GraphNode parent;
            public int location;
            public bool isEnd;
            public bool isVisited;

            public GraphNode()
            {
                isVisited = false;

            }
            public void addTop(GraphNode node)
            {
                top = node;

            }
            public void addBottom(GraphNode node)
            {
                bottom = node;

            }
            public void addLeft(GraphNode node)
            {
                left = node;

            }
            public void addRight(GraphNode node)
            {
                right = node;

            }

            public GraphNode getRight() { return right; }
            public GraphNode getLeft() { return left; }
            public GraphNode getTop() { return top; }
            public GraphNode getBottom() { return bottom; }




        }
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
        public Stack<GraphNode> shortestPath;
        public List<GraphNode> nodes = new List<GraphNode>();
        public List<int> locations = new List<int>();
        public List<Edges> edges2 = new List<Edges>();
        

        private int sizeX;
        private int sizeY;
        public Maze(int sizeX, int sizeY, int playerPosition)
        {



            this.sizeX = sizeX;
            this.sizeY = sizeY;
            // Initialize map



            for (int i = 0; i < sizeX * sizeY; i++)
            {
                nodes.Add(new GraphNode());
                nodes[i].location = i;
                nodes[i].isEnd = false;
            }

            nodes[nodes.Count - 1].isEnd = true;

            for (int i = 0; i < sizeX * sizeY; i++)
            {
                if (i + sizeX < sizeX * sizeY)
                {
                    edges2.Add(new Edges(i, i + sizeX));

                }
                if ((i + 1) % sizeX != 0 || i == 0)
                {
                    edges2.Add(new Edges(i, i + 1));

                }
            }
            createMaze();
            findShortestPath(playerPosition);

        }

        public void findShortestPath(int position)
        {
            shortestPath = new Stack<GraphNode>();
            Queue<GraphNode> queue = new Queue<GraphNode>();

            queue.Enqueue(nodes[position]);
            bool foundEnd = false;
            while (queue.Count > 0)
            {
                GraphNode node = queue.Dequeue();
                GraphNode bottom = node.getBottom();
                GraphNode top = node.getTop();
                GraphNode left = node.getLeft();
                GraphNode right = node.getRight();
                if (right != null)
                {
                    if (!right.isVisited)
                    {
                        right.isVisited = true;
                        right.parent = node;
                        if (right.isEnd)
                        {
                            foundEnd = true;
                        }
                        queue.Enqueue(right);
                    }

                }
                if (left != null)
                {
                    if (!left.isVisited)
                    {
                        left.isVisited = true;
                        left.parent = node;
                        if (left.isEnd)
                        {
                            foundEnd = true;
                        }
                        queue.Enqueue(left);
                    }


                }
                if (bottom != null)
                {
                    if (!bottom.isVisited)
                    {
                        bottom.isVisited = true;
                        bottom.parent = node;
                        if (bottom.isEnd)
                        {
                            foundEnd = true;
                        }
                        queue.Enqueue(bottom);
                    }
                }

                if (top != null)
                {
                    if (!top.isVisited)
                    {
                        top.isVisited = true;
                        top.parent = node;
                        if (top.isEnd)
                        {
                            foundEnd = true;
                        }
                        queue.Enqueue(top);

                    }

                }
                if (foundEnd)
                {
                    queue.Clear();
                }

            }
            bool done = false;

            GraphNode tempNode = nodes[nodes.Count - 1];

            while (!done)
            {
                tempNode = tempNode.parent;
                shortestPath.Push(tempNode);
                locations.Insert(0,tempNode.location);
                if (tempNode == nodes[position])
                {
                    done = true;
                }

            }
            shortestPath.Pop();
            locations.RemoveAt(0);

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

            DisjointSet set = new DisjointSet(sizeX, sizeY);

            ShuffleList(edges2);

            for (int i = 0; i < edges2.Count; i++)
            {
                if (set.find(edges2[i].from) != set.find(edges2[i].to))
                {

                    int difference = Math.Abs(edges2[i].to - edges2[i].from);

                    if (difference % sizeX == 0)
                    {
                        nodes[edges2[i].from].addBottom(nodes[edges2[i].to]);
                        nodes[edges2[i].to].addTop(nodes[edges2[i].from]);
                    }
                    else
                    {
                        nodes[edges2[i].from].addRight(nodes[edges2[i].to]);
                        nodes[edges2[i].to].addLeft(nodes[edges2[i].from]);
                    }
                    set.union(set.find(edges2[i].from), set.find(edges2[i].to));
                }
            }
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                for (int j = 0; j < sizeY * sizeX; j++)
                {
                    if (set.find(i) != set.find(j))
                    {
                        throw new Exception(
                                   "Maze did not build correctly");
                    }
                }

            }

        }



        







    }



}
