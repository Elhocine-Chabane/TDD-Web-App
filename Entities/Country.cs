namespace Entities
{
    public class Country
    {
        /// <summary>
        /// Domain model for storing the country detail 
        /// in good behaviors, this Domain model must not be exposed to the 
        /// presentation layer (Controllers and Views)
        /// </summary>
        public Guid CountryID { get; set; }

        public string? CountryName { get; set; } 

    }
}