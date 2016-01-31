namespace AUSKF.Tests.Domain.Entities
{
    using System.Collections.Generic;
    using AUSKF.Domain.Entities;
    using NUnit.Framework;

    [TestFixture]
    public class PromotionsTests
    {
        [Test]
        public void Sort_Should_Return_Promotions_OrderedBy_Date_Descending()
        {
            List<Promotion> promotions = new List<Promotion>
            {

                new Promotion
                {
                    RankDate = new System.DateTime(2011, 2, 2, 0, 0, 0, 0)
                },
                new Promotion
                {
                    RankDate = new System.DateTime(2001, 2, 2, 0, 0, 0, 0)
                },
                new Promotion
                {
                    RankDate = new System.DateTime(2009, 2, 2, 0, 0, 0, 0)
                },

                new Promotion
                {
                    RankDate = new System.DateTime(2003, 2, 2, 0, 0, 0, 0)
                },
                new Promotion
                {
                    RankDate = new System.DateTime(2015, 2, 2, 0, 0, 0, 0)
                }
            };


            promotions.Sort();
            Assert.AreEqual( 2015, promotions[0].RankDate.Value.Year);
        }
    }
}