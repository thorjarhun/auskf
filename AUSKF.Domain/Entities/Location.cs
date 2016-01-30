namespace AUSKF.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }

        public Address Address { get; set; }
    }
}