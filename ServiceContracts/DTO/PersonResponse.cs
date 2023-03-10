using ServiceContracts.DTO.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// It represents DTO class that is used as return 
    /// type of most methods of PersonsService 
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj.GetType() != typeof(PersonResponse)) return false;
            PersonResponse? person = obj as PersonResponse;
            return PersonID == person.PersonID && PersonName == person.PersonName && Email == person.Email && DateOfBirth == person.DateOfBirth
            && Gender == person.Gender && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters
            && CountryID == person.CountryID;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Person ID: {PersonID}, Person Name: {PersonName},  Email: {Email}, Gender: {Gender}, Date of Birth: {DateOfBirth?.ToString("dd mm yyyy")}, Country ID: {CountryID}";
        }
        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest() { PersonID = PersonID, PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth, Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true), Address = Address, CountryID = CountryID, ReceiveNewsLetters = ReceiveNewsLetters };

        }

    }
    public static class PersonExtensions
    {
        /// <summary>
        /// An extension method to convert an object of Person Class 
        /// into PersonResponse object
        /// </summary>
        /// <param name="person"></param>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                CountryID = person.CountryID,
                Gender = person.Gender,
                Age = (person.DateOfBirth != null)? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.24) : null


            };
            
        }
        
    }
    
}
