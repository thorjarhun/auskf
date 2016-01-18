namespace AUSKF.Domain.Data
{
    using System.Data.Entity;
    using Entities;
    using Entities.Identity;

    public sealed partial class DataContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Rank> KendoRanks { get; set; }

        public DbSet<Dojo> Dojos { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Address> Addresses { get; set; }

        // we don't need this object
        // public DbSet<DojoMembership> DojoMemberships {get;set;}

        public DbSet<Federation> Federations { get; set; }

        // we really don't need this entity either
        public DbSet<FederationMembership> FederationMemberships { get; set; }

        public DbSet<FederationOfficer> FederationOfficers { get; set; }

        public DbSet<OfficerRole> OfficerRoles { get; set; }

        public DbSet<Event> Events { get; set; }
    }
}