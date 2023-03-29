using System.Globalization;

namespace CoffeePalace.Services.Common;

[Obsolete("Country service is used instead")]
public class CountriesList
{
    public static List<string> GetCountryNamesToLower()
    {
        var cultures = CultureInfo
            .GetCultures(CultureTypes.AllCultures);

        var countries = cultures
            .Where(culture => culture.LCID < 0x1000 && !culture.IsNeutralCulture) // Filter out custom and neutral cultures
            .Select(culture => 
            {
                try
                {
                    return new RegionInfo(culture.Name).EnglishName.ToLower();
                }
                catch (ArgumentException)
                {
                    return null;
                }
            })
            .Where(name => name != null)
            .Distinct()
            .OrderBy(name => name)
            .ToList();

        return countries;
    }
}