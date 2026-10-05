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

//Value class, no functionality
public class GameResult {
    public const string WON = "Won";
    public const string LOST = "Lost";

    private bool _won;

    public string Word { get; private set; }

    public List<char> Guesses { get; private set; } 

    public uint NumIncorrectGuesses { get ; private set; }

    public string Result { get { return _won ? WON : LOST; } }
    public uint NumGuesses { get { return (uint)Guesses.Count; } }


    public float Accuracy { 
        get { return NumGuesses == 0 ? 
                0.0f : (float)((NumGuesses - NumIncorrectGuesses) * 100 / (float)NumGuesses); } 
    }

    public GameResult(string word, bool won, List<char> guesses, uint numIncorrectGuesses) {
        this.Word = word;
        this._won = won;
        this.NumIncorrectGuesses = numIncorrectGuesses;
        this.Guesses = new List<char>(guesses);
    }

}
    