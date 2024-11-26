using System;
using System.IO;

class HighScoreManager
{
    private string filePath;

    public HighScoreManager(string filePath)
    {
        this.filePath = filePath;
    }

    public int ReadHighScore()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "0");
            return 0;
        }

        string content = File.ReadAllText(filePath);
        return int.TryParse(content, out int highScore) ? highScore : 0;
    }

    public void WriteHighScore(int score)
    {
        File.WriteAllText(filePath, score.ToString());
    }
}
