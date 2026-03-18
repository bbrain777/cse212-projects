using Microsoft.VisualBasic;

public static class DisplaySums {
    public static void Run() {
        // Example 1: find every pair that adds up to 10.
        DisplaySumPairs([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
        // Should show something like (order does not matter):
        // 6 4
        // 7 3
        // 8 2
        // 9 1 

        // Separator to make the output easier to read.
        Console.WriteLine("------------");
        // Example 2: this list includes negative numbers and zero.
        DisplaySumPairs([-20, -15, -10, -5, 0, 5, 10, 15, 20]);
        // Should show something like (order does not matter):
        // 10 0
        // 15 -5
        // 20 -10

        // Separator to make the output easier to read.
        Console.WriteLine("------------");
        // Example 3: another mixed list to verify the logic still works.
        DisplaySumPairs([5, 11, 2, -4, 6, 8, -1]);
        // Should show something like (order does not matter):
        // 8 2
        // -1 11
    }

    /// <summary>
    /// Display pairs of numbers (no duplicates should be displayed) that sum to
    /// 10 using a set in O(n) time.  We are assuming that there are no duplicates
    /// in the list.
    /// </summary>
    /// <param name="numbers">array of integers</param>
    private static void DisplaySumPairs(int[] numbers) {
        // Store numbers we have already visited.
        var valuesSeen = new HashSet<int>();

        // Read each number once.
        foreach (var n in numbers) {
            // If the matching number needed to make 10 was seen earlier,
            // we have found a valid pair.
            if (valuesSeen.Contains(10 - n)) {
                Console.WriteLine($"{n} {10 - n}");
            }

            // Save the current number so later values can pair with it.
            valuesSeen.Add(n);
        }
    }
}
