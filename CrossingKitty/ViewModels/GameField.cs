using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossingKitty.ViewModels
{
    public class GameField
    {
        public int Id { get; set; }
        public int GridRows { get; set; } = 6;
        public int GridCols { get; set; } = 10;
        private double CellSize { get; set;}  = 100;

    }
}
