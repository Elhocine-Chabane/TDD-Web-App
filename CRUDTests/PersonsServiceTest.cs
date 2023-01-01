using System;
using System.Collections.Generic;
using Xunit;
using Entities;
using Services;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.enums;
using System.Reflection;
using System.Net.Http.Headers;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson
        //when we supply null value as PersonAddRequest
        //it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest personAddRequest = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _personsService.AddPerson(personAddRequest);
            });
        }

        //when we supply null value as PersonName
        //it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest() { PersonName = null };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personsService.AddPerson(personAddRequest);
            });
        }

        //when we supply a proper person Details
        //it should add a person object to the list of persons and
        // it returns a object of type PersonResponse
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Elhocine",
                Email = "elhocine.chabane1294@gmail.com",
                Address = "3 Rue lucien Sampaix",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true,
            };
            //Act
            PersonResponse ActualResponse =  _personsService.AddPerson(personAddRequest);
            List<PersonResponse> personsList = _personsService.GetAllPersons();

            //Assert
            Assert.True(ActualResponse.PersonID != Guid.Empty);
            Assert.Contains(ActualResponse, personsList);


        }

        #endregion
        
        
        #region GetPersonByPersonID
        //if  we supply null as PersonID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByID_NullPersonID()
        {
            //Arrange 
            Guid? personID = null;

            //act
            PersonResponse? response = _personsService.GetPersonByPersonID(personID);
            //Assert
            Assert.Null(response);  
        }

        // if we supply a valid person ID, it should retrun a valid person details 

        [Fact] 
        public void GetPersonByID_ProperPersonID()
        {
            //Arrange
            CountryAddRequest country_request = new CountryAddRequest()
            {
                CountryName = "Canada"
            };
            CountryResponse country_response = _countriesService.AddCountry(country_request);

            //Act
            PersonAddRequest person_request = new PersonAddRequest()
            {
                PersonName = "person name..",
                Email = "email@gmail.com",
                Address = "Address",
                CountryID = country_response.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false,

            };
            PersonResponse expected_response = _personsService.AddPerson(person_request);
            PersonResponse actual_response = _personsService.GetPersonByPersonID(expected_response.PersonID);


            //Assert
            Assert.Equal(expected_response, actual_response);
            
        }

        #endregion


        #region GetAllPersons
        // the GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> persons_from_get = _personsService.GetAllPersons();
            //Assert
            Assert.Empty(persons_from_get);
            
        }

        // first we will add few persons; and then
        // we call the GetAllPersons(), we should get the same list of persons
        // were added 
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);
            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = false,
                Address = "address of smith"
            };
            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of Mary",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2002-02-02"),
                ReceiveNewsLetters = true,

            };
            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of Rahman",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("1999-03-03")

            };
            List<PersonAddRequest> persons_request = new List<PersonAddRequest>()
            {
                person_request_1,
                person_request_2,
                person_request_3
            };
            List<PersonResponse> persons_response_list_from_add = new List<PersonResponse>();
            foreach(PersonAddRequest person_request in persons_request)
            {
                PersonResponse person_response =  _personsService.AddPerson(person_request);
                persons_response_list_from_add.Add(person_response);
            }

            // print persons_response_list_from_add
            _testOutputHelper.WriteLine("Expected:  ");
            foreach(var person_response in persons_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response.ToString());

            }
            //Act
            List<PersonResponse> person_response_list_from_get = _personsService.GetAllPersons();
            // print person_response_list_from_get 
            _testOutputHelper.WriteLine("Actual : ");
            foreach(var person in person_response_list_from_get)
            {
                _testOutputHelper.WriteLine(person.ToString()); 
            }

            //Assert 
            foreach(PersonResponse persons_response_from_add in persons_response_list_from_add)
            {
                Assert.Contains(persons_response_from_add, person_response_list_from_get);
            }
        }

        #endregion
        #region GetFilteredPersons

        // if the search text is empty and search by is "PersonName", 
        //it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);
            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = false,
                Address = "address of smith"
            };
            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of Mary",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2002-02-02"),
                ReceiveNewsLetters = true,

            };
            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of Rahman",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("1999-03-03")

            };
            List<PersonAddRequest> persons_request = new List<PersonAddRequest>()
            {
                person_request_1,
                person_request_2,
                person_request_3
            };
            List<PersonResponse> persons_response_list_from_add = new List<PersonResponse>();
            foreach (PersonAddRequest person_request in persons_request)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                persons_response_list_from_add.Add(person_response);
            }

            // print persons_response_list_from_add
            _testOutputHelper.WriteLine("Expected:  ");
            foreach (var person_response in persons_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response.ToString());

            }
            //Act
            List<PersonResponse> person_response_list_from_research = _personsService.GetFilteredPersons(nameof(Person.PersonName), "");


            // print person_response_list_from_get 
            _testOutputHelper.WriteLine("Actual : ");
            foreach (var person in person_response_list_from_research)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Assert 
            foreach (PersonResponse persons_response_from_add in persons_response_list_from_add)
            {
                Assert.Contains(persons_response_from_add, person_response_list_from_research);
            }
        }
        // First we will add few persons, then we will search based on 
        //person name with some search string
        // it should return the matching persons 
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);
            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@example.com",
                Gender = GenderOptions.Male,
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = false,
                Address = "address of smith"
            };
            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "Mary",
                Email = "mary@example.com",
                Gender = GenderOptions.Female,
                Address = "address of Mary",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2002-02-02"),
                ReceiveNewsLetters = true,

            };
            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "Rahman",
                Email = "rahman@example.com",
                Gender = GenderOptions.Male,
                Address = "address of Rahman",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("1999-03-03")

            };
            List<PersonAddRequest> persons_request = new List<PersonAddRequest>()
            {
                person_request_1,
                person_request_2,
                person_request_3
            };
            List<PersonResponse> persons_response_list_from_add = new List<PersonResponse>();
            foreach (PersonAddRequest person_request in persons_request)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                persons_response_list_from_add.Add(person_response);
            }

            // print persons_response_list_from_add
            _testOutputHelper.WriteLine("Expected:  ");
            foreach (var person_response in persons_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response.ToString());

            }
            //Act
            List<PersonResponse> person_response_list_from_research = _personsService.GetFilteredPersons(nameof(Person.PersonName), "ma");


            // print person_response_list_from_get 
            _testOutputHelper.WriteLine("Actual : ");
            foreach (var person in person_response_list_from_research)
            {
                _testOutputHelper.WriteLine(person.ToString());
            }

            //Assert 
            foreach (PersonResponse persons_response_from_add in persons_response_list_from_add)
            {
                if(persons_response_from_add.PersonName != null)
                {
                    if(persons_response_from_add.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(persons_response_from_add, person_response_list_from_research);
                    }

                }
                
            }
        }
        #endregion


    }
}
