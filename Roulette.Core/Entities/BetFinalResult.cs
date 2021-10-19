using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Core.Entities
{
    public class BetFinalResult
    {
        public string winner_color { get; set; }
        public int winner_number { get; set; }
        public List<Player> winner_player { get; set; }
    }
}
