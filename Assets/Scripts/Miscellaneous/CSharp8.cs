using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Audio;


/// <summary>
/// Just a script to practice C# 8 features
/// </summary>
public class CSharp8
{
    [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        // Sound sound = new Sound();
        // Debug.Log($"{sound.Equals(default(Sound))}");
        // PrintState(null);
        // PrintState(true);
        // PrintState(false);

        // NullCoalesce();
        // var tup = (1,2);
        // Debug.Log((1,2,3));
        // Debug.Log($"{tup.Item1} and {tup.Item2}");

        int[] a = new int[] {1,2,3};

        // Debug.Log($"{a[0..3]}");
    }



    static void StackAllocNested()
    {
        // Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };
        // var ind = numbers.IndexOfAny(stackalloc[] { 2, 4, 6, 8 });
        // Console.WriteLine(ind);  // output: 1

    }

    static void RangePractice()
    {
        var words = new string[]
        {
                        // index from start    index from end
            "The",      // 0                   ^9
            "quick",    // 1                   ^8
            "brown",    // 2                   ^7
            "fox",      // 3                   ^6
            "jumped",   // 4                   ^5
            "over",     // 5                   ^4
            "the",      // 6                   ^3
            "lazy",     // 7                   ^2
            "dog"       // 8                   ^1
        };              // 9 (or words.Length) ^0

        // var allWords = words[..]; // contains "The" through "dog".
        // var firstPhrase = words[..4]; // contains "The" through "fox"
        // var lastPhrase = words[6..]; // contains "the", "lazy" and "dog"
    }

    int M()
    {
        int y = 5;
        int x = 7;
        return Add(x, y);

        static int Add(int left, int right) => left + right;
    }

    static void NullCoalesce()
    {
#nullable enable
        List<int>? numbers = null;
        int? i = null;

        numbers ??= new List<int>();
        numbers.Add(i ??= 17);
        numbers.Add(i ??= 20);

        Debug.Log(string.Join(" ", numbers));  // output: 17 17
        Debug.Log(i);  // output: 17
    }

    static void PatternMatching(bool? state)
    {
        Debug.Log(state switch
        {
            true => "State is true",
            false => "State is false",
            _ => "State is indeterminate"
        });

        string RockPaperScissors(string first, string second)
            => (first, second) switch
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
            };
        Debug.Log($"{RockPaperScissors("rock", "paper")}");
    }
}
