using System.Globalization;

namespace CoffeePalace.Services.Common;

public class CountriesList
{
    public static List<string> GetCountryNamesToLower()
    {
        var cultures = CultureInfo
            .GetCultures(CultureTypes.SpecificCultures);
        
        var countries = cultures
            .Select(culture => new RegionInfo(culture.LCID).EnglishName
                .ToLower())
            .Distinct()
            .OrderBy(name => name)
            .ToList();

        return countries;
    }
}