public static class DisplaySums {
    public static void Run() {
        // I start with a simple ordered list so you can review the pair logic with the easiest case first.
        DisplaySumPairs([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
        // Should show something like (order does not matter):
        // 6 4
        // 7 3
        // 8 2
        // 9 1 

        // I print a separator here so each practice case is easier for you to read in the console.
        Console.WriteLine("------------");
        // I included negatives and zero in this case so you can review that the same rule still works.
        DisplaySumPairs([-20, -15, -10, -5, 0, 5, 10, 15, 20]);
        // Should show something like (order does not matter):
        // 10 0
        // 15 -5
        // 20 -10

        // I use one more mixed example here so you can review the pattern on less predictable values for Week 03 practice.
        Console.WriteLine("------------");
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
        // I keep track of numbers already visited so I can find each matching pair in one pass.
        var valuesSeen = new HashSet<int>();

        foreach (var number in numbers) {
            // If the partner needed to make 10 was seen earlier, I print the pair immediately.
            if (valuesSeen.Contains(10 - number))
                Console.WriteLine($"{number} {10 - number}");

            // I record the current number so later values can match with it.
            valuesSeen.Add(number);
        }
    }
}
