namespace AUSKF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(Namespace="")]
    public abstract class EntityBase
    {
        // I removed the created user and modified user properties as they in 
        // too many cases have no meaning.

        protected EntityBase()
        {
            this.CreateDate = DateTime.Now;
            this.ModifyDate = DateTime.Now;
        }


        [DataType(DataType.Date)]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.Date)]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataMember]
        public DateTime ModifyDate { get; set; }
    }
}