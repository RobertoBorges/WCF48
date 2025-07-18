using System.ComponentModel.DataAnnotations;

namespace CustomerService.WebApi.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public Address Address { get; set; } = new Address();
    }

    public class Address
    {
        [StringLength(100)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string State { get; set; } = string.Empty;

        [StringLength(10)]
        public string ZipCode { get; set; } = string.Empty;

        [StringLength(50)]
        public string Country { get; set; } = string.Empty;
    }
}