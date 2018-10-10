using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Static class of common functions to be used throughout the game
/// </summary>
public static class CommonFunctions {

    private static System.Random rng = new System.Random();

    /// <summary>
    /// Shuffle function using Fisher-Yates algorithm from SO
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    /// <param name="rng">new Random()</param>
    /// <param name="array">Array to be shuffled</param>
    public static void Shuffle<T>(this System.Random rng, T[] array) {
        int n = array.Length;
        while (n > 1) {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public static T RandomElement<T>(this List<T> list) {
        return list[rng.Next(list.Count)];
    }

    public static T RandomElement<T>(this T[] array) {
        return array[rng.Next(array.Length)];
    }
}
