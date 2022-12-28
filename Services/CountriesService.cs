using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        // Create the private list because we don't have yet a DB
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();

        }

        
        
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {

             
            // Validation : CountryAddRequest can not be null otherwise we throw an ArgumentNullException 
            if (countryAddRequest == null) throw new ArgumentNullException(nameof(countryAddRequest));
            // Validation : CountryAddRequest.CountryName  can not be null otherwise we throw an ArgumentException
            if (countryAddRequest.CountryName == null) throw new ArgumentException(nameof(countryAddRequest.CountryName));
            if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0) throw new ArgumentException(" Given Country name already exists");


            //Convert object from CountryAddRequest To a Country object 
            Country country =  countryAddRequest.ToCountry();
            

            // Generate a new Guid for the country 
            country.CountryID = Guid.NewGuid();
            // Add the Country Object into _countries list
            _countries.Add(country);
            return country.ToCountryResponse();

        }
    }
}