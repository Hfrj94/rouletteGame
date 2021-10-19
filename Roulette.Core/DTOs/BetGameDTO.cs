using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Core.DTOs
{
    public class BetGameDTO
    {
        public string id_roulette { get; set; }
        public string MoneyBet { get; set; }
        public string TypeBet { get; set; }
        public string ValueBet { get; set; }
    }
}
