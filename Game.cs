// Copyright (C) 2026 Graham Kelly

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://gnu.org>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA01;

/*
 * This is the Hangman game implementation in C#.
 * One instantiation of this class will play a single game (which
 * you can play multiple times, but it would be boring as the same
 * word would be re-used).
 *
 * One thing to note. You can add a string of guesses to the class
 * before initiating the game (_instance_.withTestGuesses()). In this
 * case, the game just runs through the supplied guesses when you `Play()`.
 * This was done for test purposes and no checks are done that the string
 * contains enough characters to actually guess the word or to supply
 * enough incorrect guesses. It's assumed that the tester should take
 * care of that as it's a test consideration, NOT a real usage consideration.
 *
 * Created using Clean code principles,
 * - mostly no more than 1 level of nesting inside a method
 * - methods are short and focused
 * - meaningful method names, so don't need documentation on the method's purpose
 */
public class Game {
    private const uint DEFAULT_MAX_TRIES = 4;
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    //Don't want a new instance of these arrays for every game, so make them static
    private static readonly string[] NOT_HUNG_YET = {
        "       ------",
        "        ;   Help me!",
        "        O    |",
        "       \\|/   |",
        "       /^\\   |",
        "      mMMMMm |",
        "    -=======-|"            
    };
    private static readonly string[] HUNG = {
        "       ------",
        "        |   *Crick!*",
        "        X    |",
        "       /|\\   |",
        "       /^\\   |",
        "             |",
        "    -\\    /-|",
        "    | MMMmmM |"
    };
    private static readonly string[] FREEDOM = {
        "       ------",
        "        !    |",
        "            I'm free!",
        "       \\O/   |",
        "        |    |",
        "       /^\\   |",
        "    -=======-|",
        "    |        |"
    };

    public string Answer { get; set; }
    private uint maxTries;
    private List<char> correctGuesses = new List<char>();
    private List<char> alreadyGuessed = new List<char>();

    private bool testRun;
    private char[] testGuesses = new char[0];
    private uint testGuessIndex = 0;

    private string currentState;

    private string lastError = "";  //Will be used to display the last werror. 
    //The 'Clear' gets rid of any errors before the user can see them

    // Creates a game, seeded with the provided answer and number of tries allowed
    public Game(string answer, uint maxTries) {
        this.Answer = answer.ToUpper();
        this.maxTries = maxTries;

        correctGuesses.Add(' ');    //Seed ' ' as a correct guess
        currentState = ObfucateAnswer();
    }

    public Game(string answer) : this(answer, DEFAULT_MAX_TRIES) {}

    // Allows me to test the game with a predefined sequence of guesses.
    //
    // Mentioned above, but no checks are made to ensure the guesses passed in here
    // will contain enough characters to ensure either a correct guess or that all 
    // tries occur. It's up to the caller to ensure that as it's a test consideration,
    // NOT something that can occur in the real usage of the class.
    //
    public Game withTestGuesses(string testGuesses) {
        // If playing a test game, we'll asssume the amount of guesses will exhaust the tries.
        this.testGuesses = testGuesses.ToUpper().ToCharArray();
        this.testRun = testGuesses.Length > 0;
        return this;
    }


    //
    // Plays the hangman game.
    // You get `maxTries` attempts to guess the answer correctly.
    // While not fully solved, shows the man on the gallows, but not hanged yet.
    // If you fail `maxTries` times, the gallows drops and the man is hanged.
    // Returns true if the answer was guessed correctly, false otherwise.
    //
    public GameResult Play() {
        uint failedGuesses = 0;
        char guess = Char.MinValue;

        while (DisplayCurrentState(failedGuesses)) {
            Console.Write("Enter your guess [A-Z0-9]: ");
            guess = GetAGuessFromUser();
            failedGuesses += (IsInTheAnswer(guess) ? 0u : 1u);
        }
        return new GameResult(this.Answer,!currentState.Contains('_'), alreadyGuessed, failedGuesses);
    }

    private char GetAGuessFromUser() { 
        char guess = (testRun ? testGuesses[testGuessIndex++] : Console.ReadKey().KeyChar);
        Console.WriteLine(testRun?guess:"");    //Only need to re-output if the user is not directly entering the 
                                                // guess, but we ALWAYS need a newline!
        return Char.ToUpper(guess);
    }

    private bool IsInTheAnswer(char c) {
        char upperC = Char.ToUpper(c);

        //Fast fail for invalid or already guessed characters
        if (!ALPHABET.Contains(upperC)) {
            lastError = "Invalid entry. Please enter a valid alpha-numeric [A-Z0-9].";
            return true;        //Don't count this as an incorrect guess
        }
        if (alreadyGuessed.Contains(upperC)) {
            lastError = "You have already guessed " + upperC + ".";
            return true;        //Don't count this as an incorrect guess
        }

        //Could also have used `ALPHABET.indexOf(upperC) >= 0` (above) or `Answer.indexOf(upperC) != -1` (below) to 
        // check if the character is valid. But NOT for the `alreadyGuessed` check, as that is stored as a 
        // Collection.

        //Not guessed before, check if it's in the answer and add to the CorrectGuesses array if it is
        if (Answer.Contains(upperC)) {
            correctGuesses.Add(upperC);
            currentState = ObfucateAnswer();
            lastError = ""; //Clear the last error
        } else {
            lastError = "Incorrect guess - " + upperC;
        }
        alreadyGuessed.Add(upperC);
        return Answer.Contains(upperC);
    }

    private string ObfucateAnswer() {
        string displayStr = "";

        //Easier than using String.Replace for each character
        foreach (char c in Answer) {
            displayStr += (correctGuesses.Contains(c) ? c : '_');
        }
        return displayStr;
    }

    // Displays the obfuscated answer and the drawing of the man on the gallows.
    // Returns true if the game is still ongoing, false if the game has ended.
    private bool DisplayCurrentState(uint failedGuesses) {
        bool guessedCorrectly = !currentState.Contains('_');

        Console.Clear();
        Console.WriteLine(lastError);
        Console.WriteLine($"\nAnswer: {currentState}\n");
        if (guessedCorrectly) {
            DisplayGallows(FREEDOM);
            Console.WriteLine("\nJudge finds you innocent, you can walk away.");
        } else if (failedGuesses >= maxTries) {
            DisplayGallows(HUNG);
            Console.WriteLine($"\nJudge finds you guilty for your actions.\nThe correct answer was: {Answer}");
        } else {
            DisplayGallows(NOT_HUNG_YET);
            Console.WriteLine($"    |    {maxTries - failedGuesses}   |");
            Console.WriteLine($"\nJudge says you have {maxTries - failedGuesses} tries left before you swing!");
            return true;
        }
        return false;
    }

    private void DisplayGallows(string[] gallows) {
        foreach (string line in gallows) {
            Console.WriteLine(line);
        }
    }
}
