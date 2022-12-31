using ServiceContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using ServiceContracts.DTO;
using Xunit;

namespace CRUDTests
{
    public  class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();   
        }
        #region AddCountry
        // when CountryAddRequest is null, it should throw ArgumentNullException

        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange 
            CountryAddRequest? request = null;

            //Assert 
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
               _countriesService.AddCountry(request);

            });
        }
        // When CountryAddRequest has a countryName equal to null, it should throw
        // ArgumentExeption
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange 
            CountryAddRequest? request = new CountryAddRequest() { CountryName= null };
            //Act 

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);

            });
        }
        //When the countryName is duplicate, it should throw
        //ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsDuplicate()
        {
            //Arrange
            CountryAddRequest? request1 = new CountryAddRequest()
            { 
                CountryName = "USA"
            };
            CountryAddRequest request2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        // when the countryName is valid, it should add it to the CountriesList 
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange

            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "France"
            };
            //Act
            CountryResponse response = _countriesService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();
            //Assert
            Assert.True(response.CountryID !=Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);



        }
        #endregion
        #region GetAllCountries
        [Fact]
        // The list of countries should be empty by default(before adding any new countries)
        public void GetAllCountries_EmptyList()
        {
            //Arrange 

            //Act 
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();
            
            
            //Assert
            Assert.Empty(actual_country_response_list);
        }
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest()
                {
                    CountryName = "USA"
                },
                new CountryAddRequest()
                {
                    CountryName = "FRANCE"
                }
            };
            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (var country in country_request_list)
            {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country));
            }
            //Assert
            List<CountryResponse> Actual_Response = _countriesService.GetAllCountries();
            foreach( var country in countries_list_from_add_country)
            {
                Assert.Contains(country, Actual_Response);
            }


        }

        #endregion
        #region GetCountryByCountryID


        [Fact]
        // if we submit a null countryID it will return null as CountryResponse
        public void GetCountryByCountryID_NullCountryID()
        {
            // Arrange
            Guid? countryID = null;

            //Act 
            CountryResponse? ExpectedResponse = _countriesService.GetCountryByCountryID(countryID);
            //Assert
            Assert.Null(ExpectedResponse);

        }

        [Fact]
        // If we supply a valid countryID, it should return the matching country details as CountryResponse object


        public void GetCountryByCountryID_ValidCountryID()
        {
            //Arrange 
            CountryAddRequest? country_add_request = new CountryAddRequest()
            {
                CountryName = "CHINA"
            };
            CountryResponse response=  _countriesService.AddCountry(country_add_request);
            Guid? countryID = response.CountryID;
            //Act 
            CountryResponse? ExpectedResponse = _countriesService.GetCountryByCountryID(countryID);

            // Assert 
            Assert.Equal(response, ExpectedResponse);
        }
        #endregion
    }
}
