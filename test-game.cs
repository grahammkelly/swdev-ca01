
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
