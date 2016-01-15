namespace AUSKF.Domain.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Entities;
    using Entities.Identity;
    using Extensions;
    using Interfaces;

    public sealed class EntityContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>, ISingletonLifestyle
    {
        private List<User> users;
        private List<Rank> kendoRanks;
        private List<UserRole> userRoles;
        private List<Dojo> dojos;
        private List<Federation> federations;
        private List<Address> addresses;
        
        protected override void Seed(DataContext context)
        {

            this.AddAddresses(context);
            this.AddKendoRanks(context);
            this.AddRoles(context);
            this.AddUsers(context);
            this.AddFederations(context);
            this.AddDojos(context);
            this.AddEvents(context);
        }

        private void AddAddresses(DataContext context)
        {
           
            this.addresses = new List<Address>();

            var homeAddress = new Address
            {
                AddressId = Guid.NewGuid(),
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

        private void AddEvents(DataContext context)
        {
            var event1 = new Event
            {
                EventId = Guid.NewGuid(),

                CreateDate = DateTime.Now,
                Id = this.users[0].Id,
                Description = "2016 New Year’s Greeting from AUSKF President",
                EventText =
                    "<div class=\"entry\"><p>Happy New Year !</p><p>Reflecting on the past year, the biggest event was the 16th World Kendo Championship held in Tokyo on May 29-31. I would like to congratulate that, as a result of long preparations and hard practices, Team USA won the third place for both men’s and women’s team division. In the men’s individual division, all the four competitors won their preliminary rounds and advanced to court finals and finals against Japanese players. The crowd’s applause to our players and continued fairly long afterwards as they competed very well with a lot of spirit. As for the women’s, they advanced all the way to the semi-final after beating Germany, who they had lost to in the last championship. It was wonderful to see them show a lot of character and team work.</p><p>Going forward, I am optimistic about the future performance of Team USA as long as we do both of the followings: (1) to expand membership for young kenshi and nurture their growth, (2) to raise the overall skill levels of instructors as they strive to improve their own kendo and thereby inspire their students to follow their footsteps.</p><p>Let me briefly comment below on the activities of the AUSKF for the past year and our plans going forward.</p><p>The education committee plans to host a number of seminars. In addition to the annual events such as summer camp, the 8-dan tour, the Japanese champion tour, and the junior Team USA camp, we plan to add seminars designed specifically for mudansha, women, and instructors.</p><p>When it comes to competition, I always feel that the level of shinpan needs to be raised further. We plan to offer shinpan seminars in different geographical zones, which I strongly encourage all the eligible kenshi to attend and learn from.</p><p>As for promotion, we are working hard to coordinate with local federations and share with them the same perspectives. We will try to expedite application and delivery processes for certificates as well. Finally, we will consider incorporating “bokuto-ni-yoru-kendo-kihon-waza-keiko-ho” by holding seminars to educate and spread the method.</p><p>As for iaido, four senseis that hold the rank of 7 dan are working together with local federations to improve the skill levels and increase the membership for iaido practitioners in this country.</p><p>Finally, I will continue to work hard so that you can enjoy learning the correct kendo in the collegial and friendly environment. Let’s work together!</p><p>I hope that this year will be a good one for you.</p><p>Yoshiteru Tagawa<br>President of All United States Kendo Federation</p></div>",
                EventDate = new DateTime(2015, 12, 29),
                ModifyDate = new DateTime(2015, 12, 29),

                Title = "2016 New Year’s Greeting from AUSKF President"
            };

            context.Events.Add(event1);
            context.Commit();
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
                    FirstName = "Travis",
                    MiddleName = "Lee",
                    LastName = "Stronach",
                    Gender = "M",
                    DateOfBirth = new DateTime(1982, 2, 6),
                    AuskfIdNumber = 6035,
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
                    FirstName = "Web",
                    LastName = "Master",
                    Gender= "M",
                    DateOfBirth = new DateTime(1982, 2, 6),
                    Email = "Admin@auskf.org",
                    EmailConfirmed = true,
                    JoinedDate = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow,
                    LastSearch = "na",
                    MaximumDaysBetweenPasswordChange = 180,
                    PasswordLastChangedDate = DateTime.UtcNow,
                    Profile = new UserProfile
                    {
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

        private void AddDojos(DataContext context)
                    {
            this.dojos = new List<Dojo>
                    {
                new Dojo
                {
                    DojoName = "Minnehaha Kendo Club",
                    Federation = this.federations.Find(f => f.Name == "Midwest Kendo Federation"),
                    Address = new Address
                    {
                        AddressLine1 = "Minnesota Sword Club",
                        AddressLine2 = "4744 Chicago Ave.",
                        City = "Minneapolis",
                        State = "MN",
                        ZipCode = "55417"
                    },
                    PrimaryContact = this.users.Find(u => u.LastName == "Stronach"),
                    Phone = "612-823-6715",
                    EmailAddress = "Rcochran_Minnehaha_at_msn_dot_com",
                    WebsiteUrl= "http://minnehahakendo.org/" 
                }
                //,
                //new Dojo
                //{
                //    DojoName = "Battle Creek Kendo Kai",
                //    Address = new Address
                //    {
                //        AddressLine1 = "West Dickman Fitness Center",
                //        AddressLine2 = "2851 W. Dickman Rd.",
                //        City = "Battle Creek",
                //        State = "MI",
                //        ZipCode = "49037"
                //    },
                //    Contact = "David Christman",
                //    Phone = "269-965-6590",
                //    EmailAddress = "dtc12_at_comcast_dot_net"
                //},
                //new Dojo
                //{
                //    DojoName = "Bloomington Normal Mitsubishi Kendo Club",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Bromen Adult Day Care Center",
                //        AddressLine2 = "202 E. Locust",
                //        City = "Bloomington",
                //        State = "IL",
                //        ZipCode = "61701"
                //    },
                //    Contact = "David Shaneman",
                //    Phone = "unknown",
                //    EmailAddress = "ilwroby_at_springnet1_dot_com"
                //},
                //new Dojo
                //{
                //    DojoName = "Carleton College Kendo Club",
                //    Address = new Address
                //    {
                //        AddressLine1 = "",
                //        AddressLine2 = "",
                //        City = "",
                //        State = "MN",
                //        ZipCode = ""
                //    },
                //    Contact = "John T.Born",
                //    Phone = "unknown",
                //    EmailAddress = "John born_at_mnkyudo_dot_org"
                //},
                //new Dojo
                //{
                //    DojoName = "Central Indiana Kendo Club",
                //    Address = new Address
                //    {
                //        AddressLine1 = "",
                //        AddressLine2 = "",
                //        City = "",
                //        State = "IN",
                //        ZipCode = ""
                //    },
                //    Contact = "Hajime Sugawara",
                //    Phone = "unknown",
                //    EmailAddress = "hajime222_at_yahoo_dot_com"
                //},
                //new Dojo
                //{
                //    DojoName = "Chicago Kendo Dojo",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Buddhist Temple of Chicago",
                //        AddressLine2 = "1151 W. Leland Ave.",
                //        City = "Chicago",
                //        State = "IL",
                //        ZipCode = "60640"
                //    },
                //    Contact = "Frank Matsumoto",
                //    Phone = "773-83-2282",
                //    EmailAddress = "",
                //    WebsiteUrl = "http://www.chicagokendo.org"
                //},
                //new Dojo
                //{
                //    DojoName = "Choyokan Kendo Dojo - Des Plaines",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Golf Maine Park District Gym",
                //        AddressLine2 = "9229 W. Emerson Ave.",
                //        City = "Des Plaines",
                //        State = "IL",
                //        ZipCode = "60016"
                //    },
                //    Contact = "Tom Okawara",
                //    Phone = "",
                //    EmailAddress = "Tomo-jo_at_sbcglobal_dot_net",
                //    WebsiteUrl = ""
                //},
                //new Dojo
                //{
                //    DojoName = "Detroit Kendo Dojo",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Seaholm High School",
                //        AddressLine2 = "2436 W. Lincoln St.",
                //        City = "Birmingham",
                //        State = "MI",
                //        ZipCode = "48009"
                //    },
                //    Contact = "Katsuhiko Kan",
                //    Phone = "",
                //    EmailAddress = "detroitkendodojo_at_hotmail_dot_com",
                //    WebsiteUrl = ""
                //},
                //new Dojo
                //{
                //    DojoName = "Choyokan Kendo Dojo",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Ravenswood Fellowship United Methodist Church",
                //        AddressLine2 = "4511 N Hermitage",
                //        City = "Chicago",
                //        State = "IL",
                //        ZipCode = "60640"
                //    },
                //    Contact = "Dr. Ken Sakamoto",
                //    Phone = "",
                //    EmailAddress = "sakamotok_at_mac_dot_com",
                //    WebsiteUrl = "http://www.choyokan-kendo.org",
                //    Notes = "W F 8-10pm"
                //},
                //new Dojo
                //{
                //    DojoName = "Choyokan Kendo Dojo",
                //    Address = new Address
                //    {
                //        AddressLine1 = "UIC Sports and Fitness Center",
                //        AddressLine2 = "828 S. Wolcott",
                //        City = "Chicago",
                //        State = "IL",
                //        ZipCode = "60612"
                //    },
                //    Contact = "Dr. Ken Sakamoto",
                //    Phone = "",
                //    EmailAddress = "sakamotok_at_mac_dot_com",
                //    WebsiteUrl = "http://www.choyokan-kendo.org",
                //    Notes = "Su Noon-2pm"
                //},
                //new Dojo
                //{
                //    DojoName = "Eastern Michigan Kendo Dojo",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Eastern Michigan University",
                //        AddressLine2 = "Warner Gymnasium",
                //        City = "Ypsilanti",
                //        State = "MI",
                //        ZipCode = "48197"
                //    },
                //    Contact = "Charlie Kondek",
                //    Phone = "734-484-3284",
                //    EmailAddress = "charliekondek_at_yahoo_dot_com",
                //    WebsiteUrl = ""
                //},
                //new Dojo
                //{
                //    DojoName = "University of Wisconsin Kendo Club",
                //    Address = new Address
                //    {
                //        AddressLine1 = "University of Wisconsin-Madison Natatorium Gym Unit II",
                //        AddressLine2 = "2000 Observatory Drive",
                //        City = "Madison",
                //        State = "WI",
                //        ZipCode = "53706"
                //    },
                //    Contact = "J. Mark Kenoyer",
                //    Phone = "",
                //    EmailAddress = " jkenoyer_at_wisc_dot_edu",
                //    WebsiteUrl = ""
                //},
                //new Dojo
                //{
                //    DojoName = "Valley View Kendo Club",
                //    Address = new Address
                //    {
                //        AddressLine1 = "",
                //        AddressLine2 = "3939 County Road B",
                //        City = "La Crosse",
                //        State = "WI",
                //        ZipCode = "53706"
                //    },
                //    Contact = "Matthew Reardon",
                //    Phone = "",
                //    EmailAddress = "mjreards_at_yahoo_at_com",
                //    WebsiteUrl = "",
                //    Notes = " Th 8-9:30pm Sa 11am-1pm"
                //},
                //new Dojo
                //{
                //    DojoName = "University of Chicago Kendo Club",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Bartlette Gym",
                //        AddressLine2 = "5640 S. University Ave.",
                //        City = "Chicago",
                //        State = "IL",
                //        ZipCode = "60637"
                //    },
                //    Contact = "Hyonggun Choi",
                //    Phone = "773-702-0187",
                //    EmailAddress = "choihyonggun_at_msn_dot_com",
                //    WebsiteUrl = ""
                //},
                //new Dojo
                //{
                //    DojoName = "Musoshindenryu Iaido - Moorhead Dojo",
                //    Address = new Address
                //    {
                //        AddressLine1 = "",
                //        AddressLine2 = "121 6th Ave S",
                //        City = "Moorhead",
                //        State = "MN",
                //        ZipCode = "56560"
                //    },
                //    Contact = "Bradley Anderson",
                //    Phone = "218-329-7284",
                //    EmailAddress = "",
                //    WebsiteUrl = "http://www.musoshindenryu.com",
                //    Notes = "W 6:30-9:30pm"
                //},
                //new Dojo
                //{
                //    DojoName = "Moline Kendo Dojo",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Birdsell Chiropractic",
                //        AddressLine2 = "1201 5th avenue",
                //        City = "Moline",
                //        State = "IL",
                //        ZipCode = "61265"
                //    },
                //    Contact = "David Birdesll",
                //    Phone = "309-736-0240",
                //    EmailAddress = "davidbirdsell_at_sbcglobal_dot_net",
                //    WebsiteUrl = ""
                //},
                //new Dojo
                //{
                //    DojoName = "Hokkyokusei Kendo Club",
                //    Address = new Address
                //    {
                //        AddressLine1 = "Rochester Family YMCA",
                //        AddressLine2 = "720 1st Ave. SW",
                //        City = "Rochester",
                //        State = "MN",
                //        ZipCode = "55902"
                //    },
                //    Contact = "Stephen Voss",
                //    Phone = "507-281-3163",
                //    EmailAddress = "vosssenshu_at_aol_dot_com",
                //    WebsiteUrl = ""
                //}
            };

            this.dojos.ForEach(d => context.Dojos.Add(d));
            context.Commit();
        }

    }
}