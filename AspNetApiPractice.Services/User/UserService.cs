using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetApiPractice.Models.User;
using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.ViewModels.Shared;
using AspNetApiPractice.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AspNetApiPractice.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task CreateUser(CreateUserCommand command)
        {
            var user = new AppUser()
            {
                Email = command.Email,
                UserName = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Address = command.Address
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                var errorsModel = new ValidationErrors(result.Errors.Select(e => e.Description));
                throw new AppException(errorsModel);
            }
        }

        public async Task<LoginResponse> Login(LoginCredentials loginCredentials)
        {
            var user = await _userManager.FindByEmailAsync(loginCredentials.Email);
            bool signInSuccess = await _userManager.CheckPasswordAsync(user, loginCredentials.Password);
            if (user != null && signInSuccess)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                };

                authClaims.AddRange(
                    userRoles.Select(role => new Claim(ClaimTypes.Role, role))
                );

                var token = GetToken(authClaims);
                
                return new LoginResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
            throw new AppException("Incorrect email/password");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["APISecretKey"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["ValidIssuer"],
                    audience: _configuration["ValidAudience"],
                    expires: DateTime.Now.AddHours(72),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
