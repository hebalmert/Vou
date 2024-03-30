using Microsoft.EntityFrameworkCore;
using Vou.API.APIServices;
using Vou.API.Helper;
using Vou.Common.CountryApi;
using Vou.Shared.CountryApi;
using Vou.Shared.Entities;
using Vou.Shared.EntitiesSoft;
using Vou.Shared.Enum;
using Vou.Shared.Responses;

namespace Vou.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IAPIService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IAPIService apiService, IUserHelper userHelper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckCountriesAsync();
            await CheckPlanSoft();
            await CheckUserAsync("Nexxtplanet", "SPI", "soporte@nexxtplanet.net",
                "+1 786 503 4489", UserType.Admin);

            await CheckCorporateTest();

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await _userHelper.CheckRoleAsync(UserType.Technical.ToString());
            await _userHelper.CheckRoleAsync(UserType.UserAux.ToString());
            await _userHelper.CheckRoleAsync(UserType.Cachier.ToString());
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email,
                string phone, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    FullName = $"{firstName} {lastName}",
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Job = "Administrador",
                    UserFrom = "SeedDb",
                    UserType = userType,
                    Activo = true,
                };

                await _userHelper.AddUserAsync(user, "cris53285987");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                //Para Confirmar automaticamente el Usuario y activar la cuenta
                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
                await _userHelper.AddUserClaims(userType, email);
            }
            return user;
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                Response responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
                if (responseCountries.IsSuccess)
                {
                    List<CountryResponse> NlistCountry = (List<CountryResponse>)responseCountries.Result!;
                    List<CountryResponse> countries = NlistCountry.Where(x => x.Name == "Colombia" ||
                    x.Name == "Peru" || x.Name == "Venezuela" || x.Name == "Ecuador" || x.Name == "Chile" || x.Name == "Mexico").ToList();

                    foreach (CountryResponse item in countries)
                    {
                        Country? country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == item.Name);
                        if (country == null)
                        {
                            country = new() { Name = item.Name!, States = new List<State>() };
                            Response responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{item.Iso2}/states");
                            if (responseStates.IsSuccess)
                            {
                                List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                                foreach (StateResponse stateResponse in states!)
                                {
                                    State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        Response responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{item.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.IsSuccess)
                                        {
                                            List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                            foreach (CityResponse cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }

        private async Task CheckPlanSoft()
        {
            if (!_context.SoftPlans.Any())
            {
                //Tipos de planes de venta del sistema
                _context.SoftPlans.Add(new SoftPlan
                {
                    Name = "Plan 1 mes",
                    Price = 30,
                    MaxMikrotik = 200,
                    TimeMonth = 1,
                    Activo = true
                });
                _context.SoftPlans.Add(new SoftPlan
                {
                    Name = "Plan 3 meses",
                    Price = 80,
                    MaxMikrotik = 200,
                    TimeMonth = 3,
                    Activo = true
                });
                _context.SoftPlans.Add(new SoftPlan
                {
                    Name = "Plan 6 meses",
                    Price = 140,
                    MaxMikrotik = 200,
                    TimeMonth = 6,
                    Activo = true
                });
                _context.SoftPlans.Add(new SoftPlan
                {
                    Name = "Plan 12 meses",
                    Price = 210,
                    MaxMikrotik = 200,
                    TimeMonth = 12,
                    Activo = true
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCorporateTest()
        {
            if (!_context.Corporates.Any())
            {
                _context.Corporates.Add(
                    new Corporate
                    {
                        Name = "NexxtCargo",
                        Document = "91234343423",
                        PhoneNumber = "786 345 3456",
                        Address = "Calle 45 AVE 56 - Suba",
                        Email = "nexxtcargo@gmail.com",
                        SoftPlanId = 4,
                        ToStar = Convert.ToDateTime("2023-06-15-06"),
                        ToEnd = Convert.ToDateTime("2024-06-15-06"),
                        CountryId = 2,
                        StateId = 49,
                        CityId = 1469,
                        Activo = true
                    });
                await _context.SaveChangesAsync();
            }

            if (!_context.Registers.Any())
            {
                _context.Registers.Add(
                    new Register
                    {
                        OrderTickets = 0,
                        Tickets = 0,
                        Sells = 0,
                        SellCachier = 0,
                        CorporateId = 1
                    });
                await _context.SaveChangesAsync();
            }


        }
    }
}
