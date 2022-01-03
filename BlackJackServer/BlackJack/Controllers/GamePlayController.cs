using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.Contracts;
using BlackJack.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlayController : ControllerBase
    {
        IGamePlayService _gamePlayService;

        public GamePlayController(IGamePlayService gamePlayService)
        {
            _gamePlayService = gamePlayService;
        }

        [HttpGet]
        [Route("StartNewRound/{betAmount}")]
        public IActionResult StartNewRound(int betAmount)
        {
            try
            {
                return Ok(_gamePlayService.StartNewRound(betAmount));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error {ex.Message}");
            }
        }

        [HttpGet]
        [Route("HitPlayerNewCard")]
        public IActionResult HitPlayerNewCard()
        {
            try
            {
                return Ok(_gamePlayService.HitPlayerNewCard());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error {ex.Message}");
            }
        }

        [HttpGet]
        [Route("DonePlayerHitting")]
        public IActionResult DonePlayerHitting()
        {
            try
            {
                return Ok(_gamePlayService.DonePlayerHitting());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error {ex.Message}");
            }
        }
    }
}