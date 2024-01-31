using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProjectGameDev
{
    
    public class DisjointSet
    {
        List<int> set = new List<int>();
        public DisjointSet(int sizeX, int sizeY) 
        { 
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                set.Add(-1);
                
            }

        }
        public void union(int root1, int root2)
        {
            if (root1 == root2)
            {
                return;
            }
            if (set[root2] < set[root1])
            {
                //root2 is larger, because it is more negative
                set[root2] += set[root1];
                set[root1] = root2;
            }
            else
            {
                //root 1 is equal or larger
                set[root1] += set[root2];
                set[root2] = root1;
            }
        }
    
        public int find(int node)
        {

            if (set[node] < 0) return node;

            else
            {
                set[node] = find(set[node]);
            }
            return set[node];

        }
    }
}
