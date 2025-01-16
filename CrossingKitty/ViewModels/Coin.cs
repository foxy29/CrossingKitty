using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingKitty.ViewModels
{
    public class Coin : Image
    {
        public int Id { get; set; }
        public int Points { get; set; } = 10;
        public string CoinUrl { get; set; } 
        public bool IsCollected { get; set; }
        public double Col {  get; set; }
        public int Row {  get; set; }

        public Coin()
        {

        }
       
    }
}
