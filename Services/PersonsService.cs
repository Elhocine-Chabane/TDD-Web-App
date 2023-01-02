using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.enums;
using Services.Helpers;


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
            return _personList.Select(person => person.ToPersonResponse()).ToList();
        }

        public PersonResponse GetPersonByPersonID(Guid? personID)
        {
            if (personID == null) return null;
            Person? person = _personList.FirstOrDefault(p => p.PersonID == personID);
            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchstring)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;
            if(string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchstring))
                return matchingPersons;
            switch(searchBy)
            {
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.PersonName) ? temp.PersonName.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(temp => (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.CountryID):
                    matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(searchstring, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                default: matchingPersons = allPersons; break;
                                      
            }

            return matchingPersons;

            


        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonResponse> sortedPersons = (sortBy, sortOrder)
                switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),
                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Gender).ToList(),
                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Gender).ToList(),


                (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),


                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _ => allPersons
            };
            return sortedPersons;
        }
    }
}
