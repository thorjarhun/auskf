namespace AUSKF.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Exceptions;
    using Identity;

    [DataContract(Namespace="")]
    public class Event : EntityBase, IComparable<Event>
    {
        [Key]
        [DataMember]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        [Required, DataType(DataType.DateTime)]
        [DataMember]
        public DateTime EventDate { get; set; }
        
        [Required, MaxLength(256)]
        [DataMember]
        public string Title { get; set; }

        [Required, MaxLength(5000)]
        [DataMember]
        public string EventText { get; set; }

        [MaxLength(512)]
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public User CreatedBy { get; set; }

        [Required, ForeignKey("CreatedBy")]
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: Value Meaning Less than zero This
        /// object is less than the <paramref name="other" /> parameter.Zero This object is
        /// equal to <paramref name="other" />. Greater than zero This object is greater
        /// than <paramref name="other" />.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int CompareTo(Event other)
        {
            if (other == null)
            {
                throw new ParameterNullException("other");
            }
            return this.EventDate.CompareTo(other.EventDate);
        }
    }
}