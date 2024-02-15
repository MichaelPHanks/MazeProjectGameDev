using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProjectGameDev
{
    public class HighScores
    {

        public List<HighScore> highScores;

        public HighScores()
        {
            highScores = new List<HighScore>();
        }

        public void addHighScore(string gameSize, int score)
        {
            List<HighScore> tempHighScore = new List<HighScore>
            {
                
            };
            
            
                
                int tempScore = 0;
                int existingScores = 0;
                int totalSize = 0;
                while(existingScores < highScores.Count && tempScore < 1 && totalSize < 10) 
                {
                    if (score > highScores[existingScores].getScore())
                    {
                        tempScore++;
                        tempHighScore.Add(new HighScore(score, gameSize));
                         totalSize += 1;

                    }

                    else 
                    { 
                        tempHighScore.Add(highScores[existingScores]);
                        existingScores++;
                        totalSize += 1;


                    }
                }
                if (totalSize < 10)
                {
                    while (existingScores < highScores.Count && totalSize < 10)
                    {
                        tempHighScore.Add(highScores[existingScores]);
                        existingScores++;
                        totalSize++;
                    }
                    while (tempScore < 1 && totalSize < 10)
                    {
                        tempScore++;
                        tempHighScore.Add(new HighScore(score, gameSize));
                    totalSize++;
                    }
                }
            highScores = tempHighScore;
                
            
        }

        public class HighScore
        {
            private int score;
            private string gameSize;

            public HighScore(int score, string gameSize)
            {
                this.score = score;
                this.gameSize = gameSize;
            }
            public int getScore() { return score; }
            public string getGameSize() {  return gameSize; }

        }
    }
}
