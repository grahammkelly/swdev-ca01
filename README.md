# Hangman

This codebase defines a terminal based Hangman game implemented in C#. Was created for SW DEV module coursework. Continuous Assessment 01.

It includes the main game logic, game result handling, and test cases for verifying the game's functionality.

The game should allow a player to guess letters in an attempt to reveal a hidden word, with a limited number of 
incorrect guesses allowed before the game is lost. 

* The amount of guesses a player is allowed is allowed to be set when initializing the game.
* After each 'game', the overall game will loop and offer the player the ability to start a new game, view their 
statistics, or exit.

Statistics should list the number of games played, overall wins/losses, and list each game answer, whether they won or 
lost and the number of guesses taken.

## Software requirements

* .NET SDK 10.0+

## Building and Running

To build the project, navigate to the project directory in your terminal and run:

```bash
dotnet build
```

To run the game, use:

```bash
dotnet run
```

Sample test cases for the `Game` class can be found in the `test-game.cs` file, which runs test instances of the 
class and provides guesses. One run should 'win' a game, while the other should 'lose'. A commented out example also
allows you to run a single game without the predefined test guesses and without the overall `Hangman` harness.

To run these tests, in the same directory as above, for Mac or Linux run;

```bash
dotnet run test-game.cs >/dev/null
```

This will redirect the output from the game itself and leave output from the tests only (pass or fail). 

_I'm not familiar Windows, so not sure on console output redirection on Windows. Therefore, these instructions only cover Mac/Linux. you should be able to run the tests on Windows with `dotnet run test-game.cs` but output from the tests and game will be intermingled._

## License

This project is licensed under the GNU General Public License v3.0 - see the LICENSE file for details."
