public static class UniqueLetters {
    public static void Run() {
        // Test a string that has no repeated letters.
        var test1 = "abcdefghjiklmnopqrstuvwxyz"; // Expect True because all letters unique
        Console.WriteLine(AreUniqueLetters(test1));

        // Test a string that repeats the letter 'a'.
        var test2 = "abcdefghjiklanopqrstuvwxyz"; // Expect False because 'a' is repeated
        Console.WriteLine(AreUniqueLetters(test2));

        // Test an empty string. It should still count as unique.
        var test3 = "";
        Console.WriteLine(AreUniqueLetters(test3)); // Expect True because its an empty string
    }

    /// <summary>Determine if there are any duplicate letters in the text provided</summary>
    /// <param name="text">Text to check for duplicate letters</param>
    /// <returns>true if all letters are unique, otherwise false</returns>
    private static bool AreUniqueLetters(string text) {
        // Keep track of every letter we have seen so far.
        var seenLetters = new HashSet<char>();

        // Check each character once from left to right.
        foreach (var letter in text) {
            // If Add returns false, that letter was already in the set,
            // which means the string contains a duplicate.
            if (!seenLetters.Add(letter))
                return false;
        }

        // Reaching this point means every letter was unique.
        return true;
    }
}
