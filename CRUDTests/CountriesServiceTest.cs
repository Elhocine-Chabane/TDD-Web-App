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
            //Assert
            Assert.True(response.CountryID !=Guid.Empty);



        }
    }
}
