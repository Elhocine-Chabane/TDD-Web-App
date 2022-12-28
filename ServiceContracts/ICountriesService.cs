
using ServiceContracts.DTO;
namespace ServiceContracts
    
{
    /// <summary>
    /// Represents business logic for manipulating 
    /// country Entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// A method that adds a countryobject to the list 
        /// of countries
        /// 
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns>Returns the country object after adding it including new generated id </returns>
         CountryResponse AddCountry(CountryAddRequest?  countryAddRequest);

    }
}