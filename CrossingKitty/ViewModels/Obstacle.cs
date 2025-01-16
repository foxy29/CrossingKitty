using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingKitty.ViewModels
{
    public class Obstacle : Image
    {
        public int Id { get; set; }
        public int Damage { get; set; } = 1;
        public int Speed { get; set; } = 1;
        public string ObstacleUrl { get; set; }
        public bool IsCollided { get; set; }
        public double Col { get; set; }
        public int Row { get; set; }

        public Obstacle()
        {

        }


    }
}
