/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        // The dictionary stores one entry per player:
        // key = player ID, value = total career points.
        var players = new Dictionary<string, int>();

        // Open the CSV file and read one row at a time.
        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row

        // Process every season record in the file.
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            // Add this season's points to the player's running total.
            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        // Convert the dictionary into an array so we can sort by total points.
        var topPlayers = players.ToArray();

        // Sort from highest total points to lowest total points.
        Array.Sort(topPlayers, (player1, player2) => player2.Value - player1.Value);

        Console.WriteLine();

        // Print the first 10 entries after sorting, which are the top scorers.
        for (var i = 0; i < 10; ++i) {
            Console.WriteLine(topPlayers[i]);
        }
    }
}
