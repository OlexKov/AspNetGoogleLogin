using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Specifications;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;




namespace BusinessLogic.Services
{
    internal class AccountService(IImageService imageService,
        IJwtService jwtService,
        IRepository<User> repository,
        IRepository<UserRole> rolesRepository,
        IConfiguration config) : IAccountService
    {
        private readonly IRepository<UserRole> rolesRepository = rolesRepository;
        private readonly IImageService imageService = imageService;
        private readonly IJwtService jwtService = jwtService;
        private readonly IRepository<User> repository = repository;
        private readonly IConfiguration config = config;

        public async Task<AuthResponse> GoogleLoginAsync(string googleAccessToken)
        {
            using HttpClient httpClient = new ();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", googleAccessToken);
            HttpResponseMessage response = await httpClient.GetAsync(config["GoogleUserInfoUrl"]);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(responseBody)!;
            var user = await repository.GetItemBySpec(new UserSpecs.GetByEmail(userInfo.Email));
            if (user == null)
            {
                user = new()
                {
                    Id = 0,
                    Username = userInfo.Email,
                    Password = string.Empty,
                    Name = userInfo.Given_Name,
                    Surname = userInfo.Family_Name,
                    Email = userInfo.Email,
                    Birthdate = new DateTime(),
                    Image = await imageService.SaveImageByUrlAsync(userInfo.Picture),
                    IsAccountNonExpired = true,
                    IsAccountNonLocked = true,
                    IsCredentialsNonExpired = true,
                    IsEnabled = true,
                    Cart = new HashSet<CartProduct>(),
                    Roles = new HashSet<UserRole>(),
                    FavoriteProducts = new HashSet<Product>()
                };
                var role = await rolesRepository.GetItemBySpec(new UserRolesSpecs.GetByName("User"));
                user.Roles.Add(role);
                await repository.InsertAsync(user);
                await repository.SaveAsync();
            }
            return new AuthResponse() { token = jwtService.CreateToken(user)};
        }
    }
}
