using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA01 {

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
}