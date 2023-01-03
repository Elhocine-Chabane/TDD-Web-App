using Entities;
using ServiceContracts.DTO.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonUpdateRequest
    {
        /// <summary>
        /// Represents the DTO class that contains the perons details to update
        /// </summary>
        
        
            [Required(ErrorMessage ="PersonID can not be blank")]
            public Guid PersonID { get; set; }


            [Required(ErrorMessage = "Person Name can not be blank")]
            public string? PersonName { get; set; }

            [Required(ErrorMessage = "Email can't be blank")]
            [EmailAddress(ErrorMessage = "Email value should be a valid email")]
            public string? Email { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public GenderOptions? Gender { get; set; }
            public Guid? CountryID { get; set; }
            public string? Address { get; set; }
            public bool ReceiveNewsLetters { get; set; }
            /// <summary>
            /// It converts the current object of 
            /// PersonAddRequest into a new object of Person
            /// </summary>
            /// <returns></returns>
            public Person ToPerson()
            {
                return new Person()
                {
                    PersonID = PersonID,
                    PersonName = PersonName,
                    Email = Email,
                    DateOfBirth = DateOfBirth,
                    Gender = Gender.ToString(),
                    CountryID = CountryID,
                    Address = Address,
                    ReceiveNewsLetters = ReceiveNewsLetters,


                };
            }


     }
    
}
