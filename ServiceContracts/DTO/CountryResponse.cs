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
    /// DTO class is used as return type for most 
    /// of CountriesService Mehods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if (obj.GetType() != typeof(CountryResponse)) return false;
            CountryResponse? country_To_Compare = obj as CountryResponse;
            return country_To_Compare.CountryName == this.CountryName && country_To_Compare.CountryID == this.CountryID;
            
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public static class CountryExtensiosn
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName,
            };
                
        }
    }
 
}