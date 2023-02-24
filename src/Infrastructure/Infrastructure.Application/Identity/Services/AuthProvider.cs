using Domain.DataTransferObjects.Identity;
using Domain.DbModels.Identity;
using Domain.Interfaces.Identity;
using Infrastructure.Providers.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Presentation.Middleware.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Application.Identity.Services
{
    public class AuthProvider : IAuthProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDataProtectionService _dataProtectionService;
        private readonly JwtSettings _jwtSettings;

        public AuthProvider(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager, 
            IDataProtectionService dataProtectionService) 
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _dataProtectionService = dataProtectionService;
        }

        public async Task<AuthResponse> Login(string credentials)
        {
            var up = _dataProtectionService.Unprotect(credentials);
            AuthRequest loginDetails = JsonSerializer.Deserialize<AuthRequest>(up);

            var user = await _userManager.FindByEmailAsync(loginDetails.Email);

            if (user == null)
            {
                throw new NotFoundException($"User with {loginDetails.Email} not found.", loginDetails.Email);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDetails.Password, false);

            if (result.Succeeded == false)
            {
                throw new BadRequestException($"Credentials for '{loginDetails.Email} aren't valid'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var roles = await _userManager.GetRolesAsync(user);
            var menus = GetMenuList(roles);

            var response = new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Menus = menus,
            };

            return response;
        }


        public async Task<RegistrationResponse> Register(string credentials)
        {
            var up = _dataProtectionService.Unprotect(credentials);
            RegistrationRequest loginDetails = JsonSerializer.Deserialize<RegistrationRequest>(up);

            var user = new ApplicationUser
            {
                FullName = loginDetails.FullName,
                Email = loginDetails.Email
            };

            var result = await _userManager.CreateAsync(user, loginDetails.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return new RegistrationResponse() { Email = user.Email };
            }
            else
            {
                StringBuilder str = new();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                }

                throw new BadRequestException($"{str}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim("roles", q)).ToList();

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
               signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private static List<MenuOptions> GetMenuList(IList<string> permissions)
        {
            List<MenuOptions> menus = new()
            {
                new MenuOptions
                {
                    Name = "Home",
                    Component = "MainPage",
                    Icon = "Home",
                    Path = "/",
                    permissions = new List<string>
                    {
                        "Administrator" , "User"
                    },
                    ShowInMenu = true
                },

                new MenuOptions
                {
                    Name = "ToDo List",
                    Component = "MainTable",
                    Icon = "ToDoList",
                    Path = "/todolist",
                    permissions = new List<string>
                    {
                        "Administrator" , "User"
                    },
                    ShowInMenu = true
                },

                new MenuOptions
                {
                    Name = "About",
                    Component = "About",
                    Icon = "About",
                    Path = "/about",
                    permissions = new List<string>
                    {
                        "Administrator" , "User"
                    },
                    ShowInMenu = true
                }
            };


            if (permissions.Contains("Administrator")){
                menus.Add(new MenuOptions
                {
                    Name = "Admin",
                    Component = "Admin",
                    Icon = "Admin",
                    Path = "/admin",
                    permissions = new List<string>
                    {
                        "Administrator"
                    },
                    ShowInMenu = true
                });
            }

            return menus;
        }
    }
}
