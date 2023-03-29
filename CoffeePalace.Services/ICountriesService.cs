using CoffeePalace.Services.Dtos;

namespace CoffeePalace.Services;

public interface ICountriesService
{
    public IEnumerable<CountryDto> GetAllCountries();
    
    public IEnumerable<string> GetAllCountriesNames();
    
    public IEnumerable<string> GetAllCountriesCodes();
    
    public IEnumerable<string> GetAllCountriesPhoneCodes();
    
    public string GetCountryPhoneCodeByCountryCode(string code);
    
    public string GetCountryPhoneCodeByCountryName(string country);
    
    public string GetCountryCodeByCountryName(string country);
    
    public string GetCountryNameByCountryCode(string code);
    
    public string GetCountryNameByCountryPhoneCode(string phoneCode);
    
    public string GetCountryCodeByCountryPhoneCode(string phoneCode);
    
    public bool IsCountryNameValid(string country);
    
    public bool IsCountryCodeValid(string code);
    
    public bool IsCountryPhoneCodeValid(string phoneCode);
    
}