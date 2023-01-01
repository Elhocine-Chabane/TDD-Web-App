using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person Entity
    /// </summary>
    public  interface IPersonsService
    {
        /// <summary>
        /// Adds a new person into the list of persons
        /// </summary>
        /// <param name="personAddRequest">Person To Add</param>
        /// <returns>Returns the same person details, along with newly generated PersonID</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);
        /// <summary>
        /// Returns all persons
        /// </summary>
        /// <returns>Returns a list of objects of PersonResponse type</returns>
        List<PersonResponse> GetAllPersons();
        /// <summary>
        /// Returns the peron object based on the given person Id
        /// </summary>
        /// <param name="personID">Person Id to search </param>
        /// <returns>Returns the matching PersonResponse Object</returns>
        PersonResponse GetPersonByPersonID(Guid? personID);


        /// <summary>
        /// Returns all person objects that matches with the given
        /// search field and search string
        /// </summary>
        /// <param name="searchBy">search field to search in </param>
        /// <param name="searchstring">Search string to search</param>
        /// <returns>Returns all matching persons based on the given search
        /// field and search string</returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchstring);
      
    }
}
