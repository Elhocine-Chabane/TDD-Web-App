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
        public PersonsService(bool initialize = true)
        {
            _personList = new List<Person>(); 
            _countriesService = new CountriesService();
            if (initialize)
            {
                //  {}  {}
                //  {}  {}
                /*
                 *




Mortie,mkinnock5@livejournal.com,1999-01-09,Male,968 Redwing Trail,true




                 */
                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("0994C659-3619-4719-A2F6-C2998A5F1397"),
                    PersonName = "Jere",
                    Email = "jpersehouse0@seesaa.net",
                    DateOfBirth = DateTime.Parse("1992-07-24"),
                    Gender = "Male",
                    Address = "57314 Messerschmidt Circle",
                    CountryID = Guid.Parse("04F664E7-F68C-4601-8EEF-5D0389F72E52"),
                    ReceiveNewsLetters = true


                });
                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("8383BB08-9B96-465F-9C10-51113EC6B83A"),
                    PersonName = "Katalin",
                    Email = "kkington1@friendfeed.com",
                    DateOfBirth = DateTime.Parse("1995-03-09"),
                    Gender = "Female",
                    Address = "326 Dwight Plaza",
                    ReceiveNewsLetters= true,
                    CountryID = Guid.Parse("7958D0CA-DDC6-4C79-9FEC-4D3CA0AD8991")
                });
                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("ECE39752-25A2-4165-AF6A-95ABFFED68B0"),
                    PersonName = "Rafael",
                    Email = "rcanet2@examiner.com",
                    DateOfBirth = DateTime.Parse("1997-12-23"),
                    ReceiveNewsLetters = false,
                    Gender = "Male",
                    Address = "47 Marcy Crossing",
                    CountryID = Guid.Parse("35C98A4D-E0B0-4CD2-B2F9-21B6C63493B7")
                });

                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("A7E0A284-18B6-4840-9C3A-ABF4D7FC5199"),
                    PersonName = "Robby",
                    Email = "rheads3@woothemes.com",
                    DateOfBirth = DateTime.Parse("1994-10-29"),
                    ReceiveNewsLetters = false,
                    Gender = "Female",
                    Address = "31 South Hill",
                    CountryID = Guid.Parse("35C98A4D-E0B0-4CD2-B2F9-21B6C63493B7")
                    
                });

                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("02AF7A33-1B1A-445A-A28C-2A94F625F2DE"),
                    PersonName = "Jeno",
                    Email = "jbutterworth4@instagram.com",
                    DateOfBirth = DateTime.Parse("1995-02-19"),
                    ReceiveNewsLetters = false,
                    Gender = "Male",
                    Address = "8335 Rigney Point",
                    CountryID = Guid.Parse("F0764FAA-49E2-4C56-8DB0-3BE2C925AA0F")
                    
                });

                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("2FCB24F6-3FF9-418C-87A7-ADA0B462E5C7"),
                    PersonName = "Darren",
                    Email = "dbolduc9@free.fr",
                    DateOfBirth = DateTime.Parse("1990-12-14"),
                    ReceiveNewsLetters = true,
                    Gender = "Male",
                    Address = "286 Utah Way",
                    CountryID = Guid.Parse("F0764FAA-49E2-4C56-8DB0-3BE2C925AA0F")
                    
                });
                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("40C824D9-B399-4207-9453-22B64FFC317F"),
                    PersonName = "Sutherland",
                    Email = "scohane8@npr.org",
                    DateOfBirth = DateTime.Parse("1998-08-14"),
                    ReceiveNewsLetters = true,
                    Gender = "Male",
                    Address = "16 Westport Court",
                    CountryID = Guid.Parse("53E294F0-85AB-46C3-BED1-43F763B74AC2")
                    
                });
                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("AA93F1C6-655E-4FCA-9A7C-7853FCB4E360"),
                    PersonName = "Conroy",
                    Email = "cleverage7@artisteer.com",
                    DateOfBirth = DateTime.Parse("1994-02-20"),
                    ReceiveNewsLetters = false,
                    Gender = "Male",
                    Address = "782 Pawling Alley",
                    CountryID = Guid.Parse("53E294F0-85AB-46C3-BED1-43F763B74AC2")
                   
                });
                _personList.Add(new Person()
                {
                    PersonID = Guid.Parse("47F37705-5CB8-47BA-9C71-407ACCFE28B0"),
                    PersonName = "Nonie",
                    Email = "nabrahamovitz6@fda.gov",
                    DateOfBirth = DateTime.Parse("1996-03-05"),
                    ReceiveNewsLetters = false,
                    Gender = "Female",
                    Address = "68642 Holmberg Crossing",
                    CountryID = Guid.Parse("04F664E7-F68C-4601-8EEF-5D0389F72E52")

                });

                


            }
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
            return _personList.Select(temp => ConvertPersonToPersonResponse(temp)).ToList();
        }

        public PersonResponse GetPersonByPersonID(Guid? personID)
        {
            if (personID == null) return null;
            Person? person = _personList.FirstOrDefault(p => p.PersonID == personID);
            //return person.ToPersonResponse();
            return ConvertPersonToPersonResponse(person);
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

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if(personUpdateRequest == null) throw new ArgumentNullException(nameof(personUpdateRequest));
            // validation of data (using dataanotation)
            ValidationHelper.ModelValidation(personUpdateRequest);

            // Get matching person object to update 
            Person? matchingPerson = _personList.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonID);
            if(matchingPerson == null)
            {
                throw new ArgumentException("given person id does not exist");
            }
            // update all details

            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return matchingPerson.ToPersonResponse();
           
        }

        public bool DeletePerson(Guid? personID)
        {
            if(personID == null)
            {
                throw new ArgumentNullException(nameof(personID));
            }
            Person? person = _personList.FirstOrDefault(temp => temp.PersonID == personID);
            if (person == null) return false;
            else
            {
                _personList.RemoveAll(temp => temp.PersonID == personID);
                return true;
            }
            
        }
    }
}
