public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        // 1) Create a result array with the required number of elements.
        // 2) Loop from index 0 to length - 1.
        // 3) For each position, compute the multiple using number * (index + 1).
        // 4) Store the computed value in the matching position in the result.
        // 5) Return the populated array.

        double[] result = new double[length];
        for (int i = 0; i < length; ++i)
        {
            result[i] = number * (i + 1);
        }

        return result; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        // 1) Convert the rotation amount into an equivalent shift using modulo list size.
        // 2) If the effective shift is 0, the list is already in the final order.
        // 3) Capture the right-side segment that will move to the front.
        // 4) Remove that segment from the end of the original list.
        // 5) Insert the captured segment at index 0 to finish the rotation.
        int shift = amount % data.Count;
        if (shift == 0)
        {
            return;
        }

        int startIndex = data.Count - shift;
        List<int> tail = data.GetRange(startIndex, shift);
        data.RemoveRange(startIndex, shift);
        data.InsertRange(0, tail);
    }
}
