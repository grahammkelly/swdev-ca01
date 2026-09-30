
using CA01;

// GameResult result;

// // Should fail
// result = new Game("ANSWER", 5).withTestGuesses("ABC$DEFG").Play();
// Console.WriteLine($"Expected failure - You {result.Result}. Guesses: [{string.Join(", ", result.Guesses)}]");

// Console.WriteLine($"\n{"".PadLeft(50, '+')}\n");
// //Should find the answer correctly
// result = new Game("ANSWER", 5).withTestGuesses("FAEXSWGRNVHIOP").Play();
// Console.WriteLine($"Expected Win - You {result.Result}! Guesses: [{string.Join(", ", result.Guesses)}]");

// Console.WriteLine($"\n{"".PadLeft(50, '+')}\n");
// GameResult result = new Game("Star Wars").Play();
// Console.WriteLine($"You {result.Result}!");

Hangman hangman = new Hangman();
hangman.Play();