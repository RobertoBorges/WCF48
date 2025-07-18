using System;
using System.Runtime.Serialization;

namespace CustomerService.Contracts
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public Address Address { get; set; }
    }

    [DataContract]
    public class Address
    {
        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string Country { get; set; }
    }
}