namespace Core.Entities.Identity
{
    public class Address
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string ZipCode { get; set; }
        public string PostalAddress { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public bool IsActive { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}