using CrossingKitty.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingKitty.ViewModels
{
    public class Game
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public Player? Player { get; set; }
        public bool IsGameOver { get; set; } 
        public GameField? GameField { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        public Game()
        {
            Score = 0;
            InitializeGame();
        }

        private void InitializeGame()
        {
            var player = new Player();
            var coins = new List<Coin>();
            var obstacles = new List<Obstacle>();
            var score = 0;
            var lives = Player.Lives;
            IsGameOver = false;

            var gameField = new GameField();
        }     
    }
}
