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

namespace CA01 {

    public class Stats {
        private List<GameResult> _results = new List<GameResult>();
        public uint Won { get; private set; }
        public uint Lost { get { return TotalGames - Won; } }

        public List<GameResult> Games { get { return _results; } }

        public uint TotalGames { get { return (uint)_results.Count; } }

        public void AddGame(GameResult result) {
            _results.Add(result);
            if (result.Result == GameResult.WON) {
                Won++;
            }
        }

    }

    public class Hangman {
        private const uint PLAY_GAME = 1;
        private const uint VIEW_STATS = 2;
        private const uint EXIT = 3;

        private readonly List<string> films = new List<string>();

        private string playerName = "";
        
        private Stats stats = new Stats();


        public static void Main(string[] args) {
            //Ignoring command line arguments for now
            Hangman game = new Hangman();
            game.Play();
        }

        public Hangman(List<string> films) {
            this.AddFilms(films.ToList());
        }

        public Hangman() {
                this.AddFilms(new List<string>() {
                    "Star Wars",
                    "The Godfather",
                    "Pulp Fiction",
                    "The Dark Knight",
                    "Forrest Gump",
                    "Castaway",
                    "A Series of unfortunate Events",
                    "Inception",
                    "The Matrix",
                    "Gladiator",
                    "Titanic",
                    "Jurassic Park",
                    "The Lord of the Rings",
                    "The Lion King",
                    "Avatar",
                    "Interstellar",
                    "The Shawshank Redemption",
                    "Fight Club",
                    "The Avengers",
                    "Guardians of the Galaxy",
                });
        }

        public Hangman AddFilms(List<string> films) {
            this.films.AddRange(films.ToList());
            return this;
        }

        public void Play() {
            playerName = GetPlayerName();

            //Lets ask for thre number of guesses
            Console.WriteLine("How many incorrect guesses would you like to allow per game [0-10, default 6]? ");
            uint numTries = 6;
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && uint.TryParse(input, out uint parsedTries)) {
                numTries = (parsedTries <= 10 ? parsedTries : 6);
            }

            uint menuItem = PLAY_GAME;
            Random rnd = new Random();
            while (menuItem == PLAY_GAME) {
                stats.AddGame(new Game(films[rnd.Next(films.Count)], numTries).Play());
                menuItem = PlayAgain();
            }
            
            //Report stats
            if (menuItem == VIEW_STATS) {
                ReportStats();
            }
            Console.WriteLine("Thank you for playing Hangman!");
        }

        private uint PlayAgain() {
            uint response = 0;
            while (!IsValidResponse(response, 3u)) {
                Console.Write("\n1. Play again\n2. View stats\n3. Exit\nSelect an option: ");
                try {
                    response = (uint)(Console.ReadKey().KeyChar - '0');
                } catch {
                    //Unable to convert the input to a valid number, try again
                    response = 0;
                }
                Console.WriteLine();
                if (!IsValidResponse(response, 3u)) {
                    Console.WriteLine("Invalid input, please enter a number between 1 and 3.\n");
                }
            }

            return response;
        }

        private bool IsValidResponse(uint resp, uint max) {
            return resp >= 1 && resp <= max;
        }

        private string GetPlayerName() {
            Console.WriteLine("Welcome to Hangman!");
            string? name = "";
            while(string.IsNullOrWhiteSpace(name)) {
                Console.Write("What is your name? ");
                name = Console.ReadLine();
                Console.WriteLine($"{(string.IsNullOrWhiteSpace(name) ? 
                        "Invalid input, please enter your name." : $"Hello, {name}!\n")}\n");
            }
            return name;
        }

        private void ReportStats() {
            Console.WriteLine($"\nStatistics for {playerName}:");
            Console.WriteLine($"Played: {stats.TotalGames} - Won {stats.Won}, Lost {stats.Lost}\n");
            foreach (var result in stats.Games) {
                Console.WriteLine($"\t{result.Word, -15}: {result.Result}\tGuesses: {result.NumGuesses} " + 
                        $"({result.NumIncorrectGuesses} incorrect) [{result.Accuracy:F1}% accurate]"); 
            }
        }
    }
}