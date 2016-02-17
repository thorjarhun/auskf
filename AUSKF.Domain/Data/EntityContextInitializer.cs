namespace AUSKF.Domain.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.IO;
    using Entities;
    using Entities.Identity;
    using Extensions;
    using Interfaces;

    public sealed class EntityContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>, ISingletonLifestyle
    {
        private List<User> users;
        private List<Rank> kendoRanks;
        private List<UserRole> userRoles;
        private List<Federation> federations;
        private List<Address> addresses;
        
        protected override void Seed(DataContext context)
        {

            this.AddAddresses(context);
            this.AddKendoRanks(context);
            this.AddRoles(context);

            string importDirectory = ConfigurationManager.AppSettings["DataImportDirectory"];

            // ok try this for legacy stuff
            // hard-coding the path just to test if this will actually work.
            var federationTableSqlFile = File.ReadAllText(importDirectory + "dbo.Federations.Table.sql");
            context.Database.ExecuteSqlCommand(federationTableSqlFile);

            var dojoTableSqlFile = File.ReadAllText(importDirectory + "dbo.Dojos.Table.sql");
            context.Database.ExecuteSqlCommand(dojoTableSqlFile);

            var userProfileTableSqlFile = File.ReadAllText(importDirectory + "dbo.UserProfiles.Table.sql");
            context.Database.ExecuteSqlCommand(userProfileTableSqlFile);

            var userTableSqlFile = File.ReadAllText(importDirectory + "dbo.Users.Table.sql");
            context.Database.ExecuteSqlCommand(userTableSqlFile);

            var promotionsTableSqlFile = File.ReadAllText(importDirectory + "dbo.Promotions.Table.sql");
            context.Database.ExecuteSqlCommand(promotionsTableSqlFile);
            
            this.AddFederations(context);
        }

        private void AddAddresses(DataContext context)
        {
           
            this.addresses = new List<Address>();

            var homeAddress = new Address
            {
                AddressLine1 = "123 any street",
                AddressLine2 = "any location",
                City = "Minneapolis",
                State = "MN",
                ZipCode = "55444",
                CreateDate = DateTime.UtcNow,
                ModifyDate = DateTime.UtcNow,
            };

            context.Addresses.Add(homeAddress);
            context.Commit();
            this.addresses.Add(homeAddress);

        }

        private void AddKendoRanks(DataContext context)
        {
            this.kendoRanks = new List<Rank>
            {



                new Rank {  RankName="Sankyu", RankNumeric = 11,
                    Eligibility ="The examination for kyu shall be determined by each organization.",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1 , RankType = RankType.Kendo },

                new Rank {  RankName="Nikyu", RankNumeric = 10,
                    Eligibility ="The examination for kyu shall be determined by each organization.",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1 , RankType = RankType.Kendo },

                new Rank {RankName="Ikkyu", RankNumeric = 9,
                    Eligibility ="No time period stipulated. Matches, Kata 1-3, Written examination",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1 , RankType = RankType.Kendo},

                new Rank {  RankName="Shodan", RankNumeric = 8,
                    Eligibility ="3 months or more after receipt of Ikkyu and age 14 or higher. Matches, Kata 1-5, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Yondan or higher", NumberOfExaminers=5 , RankType = RankType.Kendo},

                new Rank {  RankName="Nidan", RankNumeric = 7,
                    Eligibility ="1 year or more after receipt of Shodan. Matches, Kata 1-7, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Godan or higher", NumberOfExaminers=5 , RankType = RankType.Kendo },

                new Rank {  RankName="Sandan", RankNumeric = 6,
                    Eligibility ="2 years or more after receipt of Nidan. Matches, Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Godan or higher", NumberOfExaminers=5 , RankType = RankType.Kendo },

                new Rank {  RankName="Yondan", RankNumeric = 5,
                    Eligibility ="3 years or more after receipt of Sandan. Matches, Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Rokudan or higher", NumberOfExaminers=7  , RankType = RankType.Kendo},

                new Rank {  RankName="Godan", RankNumeric = 4,
                    Eligibility ="4 years or more after receipt of Yondan. Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7 , RankType = RankType.Kendo },

                new Rank {  RankName="Rokudan", RankNumeric = 3,
                    Eligibility ="5 years or more after receipt of Godan. Kata 1-7 and kodachi kata 1-3, Written examination & refereeing" ,
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7 , RankType = RankType.Kendo},

                new Rank {  RankName="Nanadan", RankNumeric = 2,
                    Eligibility ="6 years or more after receipt of Rokudan. Kata 1-7 and kodachi kata 1-3, Written examination & refereeing" ,
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7, RankType = RankType.Kendo },

                new Rank {  RankName="Hachi-Dan", RankNumeric = 1,
                    Eligibility ="10 years or more after receipt of Nanadan and age 46 or higher. Kata 1-7 and kodachi kata 1-3 Written examination & thesis",
                    ConsentingExaminersRequired = 7, MinimumRankOfExaminers = "Hachi-Dan", NumberOfExaminers=7 , RankType = RankType.Kendo },

                // Iaido

                new Rank {  RankName="Sankyu", RankNumeric = 11,
                    Eligibility ="The examination for kyu shall be determined by each organization.",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1 , RankType = RankType.Iaido },

                new Rank {  RankName="Nikyu", RankNumeric = 10,
                    Eligibility ="The examination for kyu shall be determined by each organization.",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1 , RankType = RankType.Iaido },

                new Rank {RankName="Ikkyu", RankNumeric = 9,
                    Eligibility ="No time period stipulated. Matches, Kata 1-3, Written examination",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1 , RankType = RankType.Iaido},

                new Rank {  RankName="Shodan", RankNumeric = 8,
                    Eligibility ="3 months or more after receipt of Ikkyu and age 14 or higher. Matches, Kata 1-5, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Yondan or higher", NumberOfExaminers=5 , RankType = RankType.Iaido},

                new Rank {  RankName="Nidan", RankNumeric = 7,
                    Eligibility ="1 year or more after receipt of Shodan. Matches, Kata 1-7, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Godan or higher", NumberOfExaminers=5 , RankType = RankType.Iaido },

                new Rank {  RankName="Sandan", RankNumeric = 6,
                    Eligibility ="2 years or more after receipt of Nidan. Matches, Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Godan or higher", NumberOfExaminers=5 , RankType = RankType.Iaido },

                new Rank {  RankName="Yondan", RankNumeric = 5,
                    Eligibility ="3 years or more after receipt of Sandan. Matches, Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Rokudan or higher", NumberOfExaminers=7  , RankType = RankType.Iaido},

                new Rank {  RankName="Godan", RankNumeric = 4,
                    Eligibility ="4 years or more after receipt of Yondan. Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7 , RankType = RankType.Iaido },

                new Rank {  RankName="Rokudan", RankNumeric = 3,
                    Eligibility ="5 years or more after receipt of Godan. Kata 1-7 and kodachi kata 1-3, Written examination & refereeing" ,
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7 , RankType = RankType.Iaido},

                new Rank {  RankName="Nanadan", RankNumeric = 2,
                    Eligibility ="6 years or more after receipt of Rokudan. Kata 1-7 and kodachi kata 1-3, Written examination & refereeing" ,
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7, RankType = RankType.Iaido },

                new Rank {  RankName="Hachi-Dan", RankNumeric = 1,
                    Eligibility ="10 years or more after receipt of Nanadan and age 46 or higher. Kata 1-7 and kodachi kata 1-3 Written examination & thesis",
                    ConsentingExaminersRequired = 7, MinimumRankOfExaminers = "Hachi-Dan", NumberOfExaminers=7 , RankType = RankType.Iaido },




            };

            this.kendoRanks.ForEach(kr => context.KendoRanks.Add(kr));
            context.Commit();
        }

        private void AddRoles(DataContext context)
        {
            this.userRoles = new List<UserRole>
            {
                 new UserRole { RoleName="Administrator" },
                 new UserRole {RoleName = "Contributor" },
                 new UserRole { RoleName="Member" }
            };
            this.userRoles.ForEach(ur => context.UserRoles.Add(ur));
            context.Commit();
        }

        public void AddUsers(DataContext context)
        {



            this.users = new List<User>()
            {
                new User()
                {

                Active = true,
                    DisplayName = "Travis Stronach",
                    Email = "travis.stronach@gmail.com",
                EmailConfirmed = true,
                JoinedDate = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                LastSearch = "na",
                MaximumDaysBetweenPasswordChange = 180,
                PasswordLastChangedDate = DateTime.UtcNow,
                Profile = new UserProfile
                {
                        AllowHtmlSig = true,
                         FirstName = "Travis",
                    MiddleName = "Lee",
                    LastName = "Stronach",
                    Gender = "M",
                    BirthDay = new DateTime(1982, 2, 6),
                    AuskfIdNumber = 6035,
                        Rank = this.kendoRanks.FindLast(x => x.RankName == "Godan"),
                        Address = new Address()
        {
                        AddressLine1 = "3226 E 53rd St",
                        City = "Minneapolis",
                        State = "MN",
                        ZipCode = "55417"
                    },
                    },
                    UserName = "tstron",
                    Password = "P@ssword1".Sha256Hash(),
                    PasswordHash = "P@ssword1".Sha256Hash()
                },
                new User
                {
                    Active = true,
                    DisplayName = "Webmaster",
                    Email = "Admin@auskf.org",
                    EmailConfirmed = true,
                    JoinedDate = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow,
                    LastSearch = "na",
                    MaximumDaysBetweenPasswordChange = 180,
                    PasswordLastChangedDate = DateTime.UtcNow,
                    Profile = new UserProfile
                    {
                        FirstName = "Web",
                    LastName = "Master",
                    Gender= "M",
                    BirthDay = new DateTime(1982, 2, 6),
                        AllowHtmlSig = true,
                        Rank = this.kendoRanks.FindLast(x => x.RankName != ""),
                        Address =  new Address()
                        {
                            AddressLine1 = "3226 E 53rd St",
                            City = "Minneapolis",
                            State = "MN",
                            ZipCode = "55417"
                        },
                },
                    UserName = "Admin",
                    Password = "P@ssword1".Sha256Hash(),
                    PasswordHash = "P@ssword1".Sha256Hash()
                }
            };

            this.userRoles.ForEach(ur => this.users[1].Roles.Add(ur));
            this.users.ForEach(u => context.Users.Add(u));
            context.Commit();

            // now read in legacy data

        }

        private void AddFederations(DataContext context)
                {
            this.federations = new List<Federation>()
                    {
                new Federation()
                {
                    Name = "All Eastern United States Kendo Federation",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    //FederationId = Guid.NewGuid(),
                    Phone = "555-121-2121",
                    WebsiteUrl = "google.com"
                    },
                new Federation()
                {
                    Name = "Central California Kendo Federation",                  
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    //FederationId = Guid.NewGuid(),
                    Phone = "555-121-2121",
                    WebsiteUrl = "google.com"
                },
                new Federation()
                {
                    Name = "Eastern United States Kendo Federation",
                    WebsiteUrl = "http://www.euskf.com/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "East Central U.S. Kendo Federation",
                    WebsiteUrl = "http://www.ecuskf.com",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Greater Northeastern United States Kendo Federation",
                    WebsiteUrl = "http://gneuskf.com/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Midwest Kendo Federation",
                    WebsiteUrl = "http://www.midwestkendofederation.com/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Northern California Kendo Federation",
                    WebsiteUrl = "http://www.nckf.org/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Pacific Northwest Kendo Federation",
                    WebsiteUrl = "http://www.pnkf.org/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Rocky Mountain Kendo / Iaido Federation",
                    WebsiteUrl = "http://www.hisamurai.com/rmkif.html",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Southern California Kendo Federation",
                    WebsiteUrl = "http://scko.org/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Southern California Kendo Organization",
                    WebsiteUrl = "http://www.midwestkendofederation.com/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Southeastern United States Kendo Federation",
                    WebsiteUrl = "http://www.seuskf.org/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                 new Federation()
                {
                    Name = "Southern United States Kendo and Iaido Federation",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Southwest Kendo and Iaido Federation",
                    WebsiteUrl = "http://www.dfwkik.org/swkif/index.html",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                    },
                new Federation()
                {
                    Name = "Western Kendo Federation",
                    WebsiteUrl = "http://www.westernkendofederation.org/",
                    Email= "Add@email.com",
                    CreateDate = DateTime.UtcNow,
                    ModifyDate = DateTime.UtcNow,
                }
            };


            this.federations.ForEach(f => context.Federations.Add(f));
            context.Commit();
        }

    }
}