using System;
using System.Collections.Generic;
using Xunit;
using Entities;
using Services;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.enums;
using System.Reflection;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        public PersonsServiceTest()
        {
            _personsService = new PersonsService();
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
    }
}
