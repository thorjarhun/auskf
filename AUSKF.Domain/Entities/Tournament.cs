namespace AUSKF.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Identity;

    public class Tournament
    {
        [Key]
        public int TournamentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        public virtual ICollection<Division> Divisions { get; set; }

        public virtual ICollection<User> Shimpans { get; set; }
    }
}