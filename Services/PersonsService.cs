using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers


namespace Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person>? _personList;
        private readonly ICountriesService _countriesService;
        public PersonsService()
        {
            _personList = new List<Person>(); 
            _countriesService = new CountriesService(); 
        }
        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null) throw new ArgumentNullException(nameof(personAddRequest));

            // Model Validation 
            ValidationHelper.ModelValidation(personAddRequest);

            // Convert the personAddRequest into Person Type  
            Person person = personAddRequest.ToPerson();
            // Generate the new Guid (PersonID)
            person.PersonID = Guid.NewGuid();

            //add it to the list of persons
            _personList.Add(person);

            // Convert the Person Object into PersonResponse Type and return it
            
            return ConvertPersonToPersonResponse(person);
        }









        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
