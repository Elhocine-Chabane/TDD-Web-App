using ServiceContracts;
using ServiceContracts.DTO;
using Entities;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        // Create the private list because we don't have yet a DB
        private readonly List<Country> _countries;

        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if(initialize)
            {
                // {04F664E7-F68C-4601-8EEF-5D0389F72E52} {7958D0CA-DDC6-4C79-9FEC-4D3CA0AD8991} {35C98A4D-E0B0-4CD2-B2F9-21B6C63493B7} {F0764FAA-49E2-4C56-8DB0-3BE2C925AA0F}
                //{53E294F0-85AB-46C3-BED1-43F763B74AC2}
                _countries.AddRange(new List<Country>()
                {
                                    new Country()
                {
                    CountryID = Guid.Parse("04F664E7-F68C-4601-8EEF-5D0389F72E52"),
                    CountryName = "USA"
                },
                                                    new Country()
                {
                    CountryID = Guid.Parse("7958D0CA-DDC6-4C79-9FEC-4D3CA0AD8991"),
                    CountryName = "Australia"
                },

                new Country()
                {
                    CountryID = Guid.Parse("35C98A4D-E0B0-4CD2-B2F9-21B6C63493B7"),
                    CountryName = "Canada"
                },


                new Country()
                {
                    CountryID = Guid.Parse("F0764FAA-49E2-4C56-8DB0-3BE2C925AA0F"),
                    CountryName = "UK"
                },                new Country()
                {
                    CountryID = Guid.Parse("53E294F0-85AB-46C3-BED1-43F763B74AC2"),
                    CountryName = "India"
                }

                });
              
              
                
            }

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

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
            
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null) return null;
            Country? country = _countries.FirstOrDefault(temp => temp.CountryID == countryID);
            if(country == null) return null;
            return country.ToCountryResponse();
            

        }

        
    }
}