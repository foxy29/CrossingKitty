using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingKitty.ViewModels
{
    public class Player : Image
    {
        public int Id { get; set; }
        public int Lives { get; set; } = 3;
        public string avatarUrl { get; set; } 
        public double Col { get; set; }
        public int Row { get; set; }

        public Player()
        {

        }

    }
}
