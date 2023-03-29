using CoffeePalace.Services.Dtos;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace CoffeePalace.Services;

public class CountriesService : ICountriesService
{
    private readonly List<CountryDto> countries;

    public CountriesService(IWebHostEnvironment WebHostEnvironment)
    {
        var path = Path.Combine(WebHostEnvironment.WebRootPath, "countries.json");
        this.countries = JsonConvert.DeserializeObject<List<CountryDto>>(File.ReadAllText(path))
                         ?? throw new Exception("Countries list is empty!");
    }

    public IEnumerable<CountryDto> GetAllCountries()
    {
        return countries;
    }

    public IEnumerable<string> GetAllCountriesNames()
    {
        return countries.Select(c => c.Name);
    }

    public IEnumerable<string> GetAllCountriesCodes()
    {
        return countries.Select(c => c.Code);
    }

    public IEnumerable<string> GetAllCountriesPhoneCodes()
    {
        return countries.Select(c => c.PhoneCode);
    }

    public string GetCountryPhoneCodeByCountryCode(string code)
    {
        return countries.FirstOrDefault(c => c.Code == code)?.PhoneCode 
               ?? throw new Exception("Could not find country!");
    }

    public string GetCountryPhoneCodeByCountryName(string country)
    {
        return countries.FirstOrDefault(c => c.Name == country)?.PhoneCode 
               ?? throw new Exception("Could not find country!");
    }

    public string GetCountryCodeByCountryName(string country)
    {
        return countries.FirstOrDefault(c => c.Name == country)?.Code 
               ?? throw new Exception("Could not find country!");
    }

    public string GetCountryNameByCountryCode(string code)
    {
        return countries.FirstOrDefault(c => c.Code == code)?.Name 
               ?? throw new Exception("Could not find country!");
    }

    public string GetCountryNameByCountryPhoneCode(string phoneCode)
    {
        return countries.FirstOrDefault(c => c.PhoneCode == phoneCode)?.Name 
               ?? throw new Exception("Could not find country!");
    }

    public string GetCountryCodeByCountryPhoneCode(string phoneCode)
    {
        return countries.FirstOrDefault(c => c.PhoneCode == phoneCode)?.Code 
               ?? throw new Exception("Could not find country!");
    }

    public bool IsCountryNameValid(string country)
    {
        return countries.Any(c => c.Name == country);
    }

    public bool IsCountryCodeValid(string code)
    {
        return countries.Any(c => c.Code == code);
    }

    public bool IsCountryPhoneCodeValid(string phoneCode)
    {
        return countries.Any(c => c.PhoneCode == phoneCode);
    }
}