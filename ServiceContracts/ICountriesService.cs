
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

        /// <summary>
        /// Returns all countries from the list 
        /// </summary>
        /// <returns>All Countries from the list as List of CountryResponse</returns>

        List<CountryResponse> GetAllCountries();
        /// <summary>
        /// it returns a DTO CountryResponse Object based 
        /// on the CountryID subbmited by the user 
        /// </summary>
        /// <param name="countryID">CountryID (Guid) to search</param>
        /// <returns>It return the matching DTO CountryResponse Object </returns>

        CountryResponse? GetCountryByCountryID(Guid? countryID); 
    }
}