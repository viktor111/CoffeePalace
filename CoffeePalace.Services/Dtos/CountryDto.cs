using Newtonsoft.Json;

namespace CoffeePalace.Services.Dtos;

public class CountryDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("code")]
    public string Code { get; set; }
    
    [JsonProperty("phone_code")]
    public string PhoneCode { get; set; }
}