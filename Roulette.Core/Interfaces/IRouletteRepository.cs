using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roulette.Core.Entities;
using Roulette.Core.DTOs;

namespace Roulette.Core.Interfaces
{
    public interface IRouletteRepository
    {
        OpenBetDTO CreateRoulette();
        string OpenBet(OpenBetDTO openBet);
        string BetPay(BetGame betPlay, string key);
        BetFinalResult CloseBet(OpenBetDTO openBet);
    }
}
