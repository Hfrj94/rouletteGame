using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roulette.Core.DTOs;
using Roulette.Core.Entities;
using Roulette.Core.Interfaces;

namespace Roulette.Infraestructure.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IRedisDBContext _redis;
        public RouletteRepository(IRedisDBContext redis)
        {
            _redis = redis;
        }
        public string BetPay(BetGame betPlay, string key)
        {
            if(int.Parse(betPlay.MoneyBet) > 10000)
            {
                return "Proceso no exitoso: valor superior al tope de la apuesta;";
            }
            else
            {
                _redis.Set(key, betPlay);
                return "Proceso exitoso: apuesta ingresada con exito";
            }
        }

        public BetFinalResult CloseBet(OpenBetDTO openBet)
        {
            var betFinalResults = _redis.GetAllForKey<BetGame>(openBet.id_roulette);
            Random r = new Random();
            BetFinalResult finalResults = new BetFinalResult();
            Player playerWinner = new Player();
            var rouletteResult = r.Next(0, 36);
            if ((rouletteResult % 2) == 0)
                finalResults.winner_color = "rojo";
            else 
                finalResults.winner_color = "negro";
            foreach(var item in betFinalResults)
            {
                if (item.ValueBet.Equals(rouletteResult))
                {
                    playerWinner.id_player = item.id_player;
                    playerWinner.MoneyBet = item.MoneyBet;
                    playerWinner.TotalMoneyBet = (double.Parse(item.MoneyBet) * 5).ToString();
                    finalResults.winner_player.Add(playerWinner);
                }
                if (item.ValueBet.Equals(finalResults.winner_color))
                {
                    playerWinner.id_player = item.id_player;
                    playerWinner.MoneyBet = item.MoneyBet;
                    playerWinner.TotalMoneyBet = (double.Parse(item.MoneyBet) * 1.8).ToString();
                    finalResults.winner_player.Add(playerWinner);
                }
            }
            return finalResults;
        }

        public OpenBetDTO CreateRoulette()
        {
            string UniqueIdRoulette = "R00";
            int i = 1;
            RouletteGame obj = new RouletteGame();
            OpenBetDTO obj2 = new OpenBetDTO();
            while (i != 0)
            {
                obj = _redis.Get<RouletteGame>(UniqueIdRoulette + i.ToString());
                if (obj == null || obj == default)
                {
                    UniqueIdRoulette += i.ToString();
                    i = 0;
                }
                if (obj.id_roulette.Equals(UniqueIdRoulette + i.ToString()))
                    i++;
            }
            obj2.id_roulette = UniqueIdRoulette;
            return obj2;
        }

        public string OpenBet(OpenBetDTO openBet)
        {
            string resultOpenBet = "";
            RouletteGame obj = new RouletteGame();
            obj = _redis.Get<RouletteGame>(openBet.id_roulette);
            if (obj == null || obj == default)
            {
                obj.id_roulette = "Roulette: " + resultOpenBet;
                obj.sn_state = true;
                _redis.Set(openBet.id_roulette, obj);
                resultOpenBet = "Proceso exitoso: Ruleta " + openBet.id_roulette + " fue creada con exito;";
            }
            if (obj.id_roulette.Equals(openBet.id_roulette))
            {
                resultOpenBet = "Proceso no exitoso: Ruleta " + openBet.id_roulette + " ya se encuentra creada;";
            }
            return resultOpenBet;
        }
    }
}
