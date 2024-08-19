
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;



namespace Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly UserService _userService;
    private readonly IConfiguration _config;

    public UserController(UserService userServivce, IConfiguration config)
    {
        _userService = userServivce;
        _config = config;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        {
            if (user == null)
                return NotFound();
        }
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        Log.Information("Logging in user: " + login.Username);
        var user = await _userService.GetUserByLogin(login.Username);
        if (user == null)
        {
            return Unauthorized(new { message = "Username not found" });
        }
        // Hash the provided password with the stored salt
        byte[] salt = Convert.FromBase64String(user.PasswordSalt);
        using var hmac = new HMACSHA512(salt);

        byte[] hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(login.Password));
        if (Convert.ToBase64String(hash) != user.Password)
        {
            Log.Information("Controller: User {0} entered invalid password", login.Username);
            return Unauthorized(new { message = "Invalid password" });
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["JWT"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.Id.ToString())
            ]),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _config["JWT_ISSUER"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        Log.Information("Controller: User {0} logged in", login.Username);

        return Ok(new { Token = tokenString, UserId = user.Id });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> CreateAccount([FromBody] User user)
    {
        Log.Information("Creating new user: " + user);
        //1.) check if user.login exists
        //2.) check if model state is valid
        //3.) hash and create password salt
        //4.) call userService.createAccount

        if (await _userService.GetUserByLogin(user.Username) != null)
        {
            return BadRequest(new { message = "Username is already taken!" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Hash password and generate password salt
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        using var hmac = new HMACSHA512(salt);
        byte[] hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Password));

        user.Password = Convert.ToBase64String(hash);
        user.PasswordSalt = Convert.ToBase64String(salt);

        try
        {
            var result = await _userService.CreateUser(user);

        }
        catch (Exception)
        {
            return StatusCode(500);
        }

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }



}