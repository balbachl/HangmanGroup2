using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        /***********************************************************
          The default file location is Hangman/bin/Debug/net8.0/
          the file is stored in the Hangman folder, 
          so a path to that folder was coded in front of the file 
        **************************************************************/
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        string[] wordList = LoadWords("../../../words.txt");
        if (wordList.Length == 0)
        {
            Console.WriteLine("No words found in the file.");
            return;
        }
        Random random = new Random();
        string wordToGuess = wordList[random.Next(wordList.Length)];
        HashSet<char> guessedLetters = new HashSet<char>();
        int attempts = 6;

        while (attempts > 0)
        {
            Console.Clear();
            Console.BackgroundColor= ConsoleColor.Black;
            Console.ForegroundColor= ConsoleColor.DarkRed;
            DrawHangman(attempts);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            DisplayWord(wordToGuess, guessedLetters);
            Console.WriteLine($"Attempts remaining: {attempts}");
            Console.Write("Guess a letter: ");
            char guessedLetter = char.Parse(Console.ReadLine());
            Console.WriteLine();

            if (guessedLetters.Contains(guessedLetter))
            {
                Console.WriteLine("You've already guessed that letter.");
                continue;
            }

            guessedLetters.Add(guessedLetter);

            if (!wordToGuess.Contains(guessedLetter))
            {
                attempts--;
                Console.WriteLine("Incorrect guess!");
            }

            if (IsWordGuessed(wordToGuess, guessedLetters))
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                DrawHangman(attempts);
                DisplayWord(wordToGuess, guessedLetters);
                Console.WriteLine("Congratulations! You've guessed the word!");
                break;
            }
        }

        if (attempts == 0)
        {
            Console.Clear();
            DrawHangman(attempts);
            Console.WriteLine($"Game over! The word was: {wordToGuess}");
        }
    }
    static string[] LoadWords(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the file: {ex.Message}");
            return Array.Empty<string>();
        }
    }
    static void DrawHangman(int attempts)
    {
        string[] hangmanStages = {
            @"
              ------
              |    |
              |    O
              |   /|\
              |   / \
              |
            ---------
            ",
            @"
              ------
              |    |
              |    O
              |   /|\
              |   /
              |
            ---------
            ",
            @"
              ------
              |    |
              |    O
              |   /|
              |
              |
            ---------
            ",
            @"
              ------
              |    |
              |    O
              |    |
              |
              |
            ---------
            ",
            @"
              ------
              |    |
              |    O
              |
              |
              |
            ---------
            ",
            @"
              ------
              |    |
              |
              |
              |
            ---------
            ",
            @"
              ------
              |
              |
              |
              |
            ---------
            "
        };

        Console.WriteLine(hangmanStages[attempts]);
    }
    static void DisplayWord(string word, HashSet<char> guessedLetters)
    {
        foreach (char letter in word)
        {
            if (guessedLetters.Contains(letter))
                Console.Write(letter + " ");
            else
                Console.Write("_ ");
        }
        Console.WriteLine();
    }

    static bool IsWordGuessed(string word, HashSet<char> guessedLetters)
    {
        foreach (char letter in word)
        {
            if (!guessedLetters.Contains(letter))
                return false;
        }
        return true;
    }
}
