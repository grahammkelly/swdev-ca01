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

## Methods

The software was;
* written on Mac (M1 Max, OS: Tahoe v26.6.2)
* compiled and run against Microsoft .NET SDK 10.0.401
* generated within Visual Studio Code 1.140.0 (commit [#07f806f999227108933c2e30515b26eecc1fda74](https://github.com/microsoft/vscode/tree/07f806f999227108933c2e30515b26eecc1fda74))

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

### Tests for the `Game` class

Sample test cases for the `Game` class can be found in the `test-game.csx` file, which runs test instances of the 
class and provides guesses without user interaction. One run should 'win' a game, while the other should 'lose'. 

I specifically did not use unit testing here. Firstly because we were specifically instructed to only use concepts 
already covered inthe module. Additionally, I am familiar with JUnit/TestNG testing, but not unit testing concepts on
.NET platforms.

You will need the dotnet script module installed to run the `test-game.csx` file. You can install it using:

```bash
dotnet tool install -g dotnet-script
```

Run the tests using the following command (Linux and Mac only):

```bash
dotnet script test-game.csx >/dev/null
```

This will redirect the output from the game itself and leave output from the tests only (pass or fail). 

_I'm not familiar Windows, so less sure on redirection there. TI don't have access to Windows so this is untested but
redirect to `nul` (command prompt) or `$null` (powershell) instead of `/dev/null` should work there._

> [!WARNING]  
> On my VS Code, when the `test-game.csx` file is loaded, every line of code is shown as an error. This is because VS 
> Code does not natively understand the C# script file format and treats it as a regular C# file, leading to syntax 
> highlighting and error reporting issues.
>
> Please disregard the error highlighting in VS Code for _this_ file if this occurs for you.


## License

This project is licensed under the GNU General Public License v3.0 - see the LICENSE file for details.
