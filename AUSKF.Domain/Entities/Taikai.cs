namespace AUSKF.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Identity;

    [DataContract(Namespace="")]
    public class Taikai
    {
        public Taikai()
        {
            this.Divisions = new List<Division>();
        }

        [Key]
        [DataMember]
        public int TaikaiId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }
        
        [ForeignKey("Location")]
        [DataMember]
        public int? LocationId { get; set; }

        [DataMember]
        public Location Location { get; set; }

        [DataMember]
        public virtual ICollection<Division> Divisions { get; set; }
    }
}