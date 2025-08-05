using FiapCloudGamesWebAPI.Application.DTOs.Game;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Controllers
{
    /// <summary>
    /// Endpoint simples para verificar se a API está online.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        public PingController()
        {
        }
        #region GET

        /// <summary>
        /// Retorna uma resposta simples indicando que a API está no ar.
        /// </summary>
        /// <returns>"pong"</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "pong" });
        }

        #endregion
    }
}
