using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace JWTClient.Controllers
{
    public class jQueryApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Index(string username, string password)
        {
            if ((username != "Secret") || (password != "Secret"))
                return "Error";

            string tokenString = GenerateJSONWebToken();
            return tokenString;
        }

        private string GenerateJSONWebToken()
        {
            /*var claims = new[] {
                new Claim("Name", "Bobby"),
                new Claim(JwtRegisteredClaimNames.Email, "hello@yogihosting.com"),
            };*/

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MynameisJamesBond007"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://www.yogihosting.com",
                audience: "https://www.yogihosting.com",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
                //claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public IActionResult Reservation([FromBody]List<Reservation> rList)
        {
            return PartialView("Reservation", rList);
        }
    }
}