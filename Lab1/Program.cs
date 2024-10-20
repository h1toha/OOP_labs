using System;
using System.Collections.Generic;
using NanoidDotNet;
public class GameAccount
{
    public string UserName { get; private set; }
    public int CurrentRating { get; private set; }
    public int GamesCount { get { return gamesHistory.Count; } }
    private List<Game> gamesHistory;

    public GameAccount(string userName )
    {
        UserName = userName;
        CurrentRating = 100;
        gamesHistory = new List<Game>();
    }

    public void WinGame(Game game)
    {
        CurrentRating += game.Rating;
        gamesHistory.Add(game);
    }

    public void LoseGame(Game game)
    {
        CurrentRating = Math.Max(1, CurrentRating - game.Rating);
        gamesHistory.Add(game);
    }

    public void GetStats()
    {
        Console.WriteLine("---------------------------------------------------------");
        Console.WriteLine($"Game history for {UserName}:\n");
        Console.WriteLine("Opponent:\t Result:\t Rating:\t Game ID:");

        foreach (var game in gamesHistory)
        {
            string result = game.IsWin(UserName) ? "Win" : "Loss";
            Console.WriteLine($"{game.GetOpponent(UserName)}\t {result}\t\t {game.Rating}\t\t {game.GameId}\t");
        }

        Console.WriteLine($"\nCurrent Rating: {CurrentRating}");
        Console.WriteLine($"Total Games Played: {GamesCount}\n");
    }
}

public class Game
{
    private string player1;
    private string player2;
    public int Rating { get; }
    public string GameId { get; }
    private string winner;

    public Game(string player1, string player2, int rating, string winner)
    {
        if (rating < 0)
            throw new ArgumentException("Rating cannot be negative.");

        this.player1 = player1;
        this.player2 = player2;
        Rating = rating;
        GameId = Nanoid.Generate(Nanoid.Alphabets.LowercaseLettersAndDigits, 7);
        this.winner = winner;
    }

    public bool IsWin(string playerName)
    {
        return playerName == winner;
    }

    public string GetOpponent(string playerName)
    {
        return playerName == player1 ? player2 : player1;
    }
}

public class Program
{
    public static void Main()
    {
        var player1 = new GameAccount("CoolGamer228");
        var player2 = new GameAccount("KillerPro_");
        var player3 = new GameAccount("GameMaster69");

        var game1 = new Game("CoolGamer228", "KillerPro_", 20, "CoolGamer228");
        player1.WinGame(game1);
        player2.LoseGame(game1);

        var game2 = new Game("KillerPro_", "GameMaster69", 15, "KillerPro_");
        player2.WinGame(game2);
        player3.LoseGame(game2);

        var game3 = new Game("GameMaster69", "CoolGamer228", 25, "GameMaster69");
        player3.WinGame(game3);
        player1.LoseGame(game3);


        player1.GetStats();
        player2.GetStats();
        player3.GetStats();
    }
}
