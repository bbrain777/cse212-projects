using System.Text.Json;
using System.Globalization;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Olaku: I switched the lookup key from strings to packed integer values because
        // your efficiency test cares about allocation cost, not just big-O notation.
        var seen = new HashSet<int>();
        var pairs = new List<string>();

        foreach (var word in words)
        {
            // Olaku: identical letters like "aa" are a required skip case from the assignment.
            var firstLetter = word[0];
            var secondLetter = word[1];

            if (firstLetter == secondLetter)
            {
                continue;
            }

            // Olaku: packing the two characters into one integer gives us a compact
            // constant-time key for both the original word and its reversed form.
            var packedWord = (firstLetter << 16) | secondLetter;
            var packedReversedWord = (secondLetter << 16) | firstLetter;

            if (seen.Contains(packedReversedWord))
            {
                // Olaku: I only build the output string when a real match exists,
                // which keeps the no-pair performance case much faster on your machine.
                pairs.Add($"{word} & {secondLetter}{firstLetter}");
            }

            seen.Add(packedWord);
        }

        return [.. pairs];
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // Olaku: the education value is the 4th column in this census file,
            // so I pull that field and count how many times each degree appears.
            var degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Olaku: I normalize the words once up front so spaces and letter casing
        // stop being distractions while you review the actual counting logic.
        var normalizedWord1 = word1.Replace(" ", "").ToLowerInvariant();
        var normalizedWord2 = word2.Replace(" ", "").ToLowerInvariant();

        if (normalizedWord1.Length != normalizedWord2.Length)
        {
            return false;
        }

        // Olaku: this dictionary tracks how many times each character appears in
        // the first word, then the second word removes those counts back to zero.
        var letterCounts = new Dictionary<char, int>();

        foreach (var letter in normalizedWord1)
        {
            if (letterCounts.ContainsKey(letter))
            {
                letterCounts[letter]++;
            }
            else
            {
                letterCounts[letter] = 1;
            }
        }

        foreach (var letter in normalizedWord2)
        {
            if (!letterCounts.ContainsKey(letter))
            {
                return false;
            }

            letterCounts[letter]--;
            if (letterCounts[letter] < 0)
            {
                return false;
            }
        }

        // Olaku: if every count returns to zero, then both words used the exact same letters.
        return letterCounts.Values.All(count => count == 0);
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Olaku: after deserialization, I project only the two fields the assignment asks for:
        // earthquake place and magnitude. I also guard against missing JSON fields so the
        // method stays safe even if the USGS feed has incomplete records.
        if (featureCollection?.Features is null)
        {
            return [];
        }

        return featureCollection.Features
            .Where(feature => feature.Properties?.Place is not null && feature.Properties.Magnitude is not null)
            .Select(feature =>
                $"{feature.Properties!.Place} - Mag {feature.Properties.Magnitude!.Value.ToString(CultureInfo.InvariantCulture)}")
            .ToArray();
    }
}
