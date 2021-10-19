using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roulette.Core.Interfaces;
using Roulette.Core.Entities;
using Roulette.Core.DTOs;

namespace RouletteAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class RouletteController : Controller
    {
        private readonly IRouletteRepository _rouletteRepos;
        public RouletteController(IRouletteRepository rouletteRepos)
        {
            _rouletteRepos = rouletteRepos;
        }
        [HttpGet]
        [Route("crearRuleta")]
        public IActionResult CreateRoulette() 
        {
            var result = _rouletteRepos.CreateRoulette();
            return Ok(result);
        }
        [HttpPost]
        [Route("crearApuestaRuleta")]
        public IActionResult openBetRoulette(OpenBetDTO openBet)
        {
            var result = _rouletteRepos.OpenBet(openBet);
            return Ok(result);
        }
        [HttpPost]
        [Route("crearApuestaRuleta")]
        public IActionResult betRoulette(BetGameDTO betPlay)
        {
            var obj = new BetGame();
            var userid = Request.Headers["userid"];
            if (userid.Count == 0)
            {
                return BadRequest("Error: no se encontro id del cliente");
            }
            
            obj.id_player = userid;
            obj.MoneyBet = betPlay.MoneyBet;
            obj.ValueBet = betPlay.ValueBet;
            var result = _rouletteRepos.BetPay( betPlay:obj, key:betPlay.id_roulette);
            return Ok(result);
        }
        [HttpPost]
        [Route("cierreApuestaRuleta")]
        public IActionResult finishBetRoulette(OpenBetDTO openBet)
        {
            var result = _rouletteRepos.CloseBet(openBet);
            return Ok(result);
        }
    }
}
