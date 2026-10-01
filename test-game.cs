
using CA01;

// Tests for the Game class in the CA01 namespace
// 
// Sending output to stderr so I can separate it from standard output
// To see the messages from _this_, run the program and redirect stdio somewhere else (/dev/null??)


GameResult result;

// Should fail
result = new Game("ANSWER", 5).withTestGuesses("ABC$DEFG").Play();
Console.Error.WriteLine($"Expected failure - You {result.Result}. Guesses: [{string.Join(", ", result.Guesses)}]");

Console.Error.WriteLine($"\n{"".PadLeft(50, '+')}\n");
//Should find the answer correctly
result = new Game("ANSWER", 5).withTestGuesses("FAEXSWGRNVHIOP").Play();
Console.Error.WriteLine($"Expected Win - You {result.Result}! Guesses: [{string.Join(", ", result.Guesses)}]");

Console.Error.WriteLine($"\n{"".PadLeft(50, '+')}\n");
// result = new Game("Star Wars").Play();
// Console.WriteLine($"You {result.Result}!");
