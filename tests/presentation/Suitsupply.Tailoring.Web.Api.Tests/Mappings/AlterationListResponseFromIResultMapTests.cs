using System.Linq;
using AutoMapper;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.Web.Api.Controllers.Responses;
using Suitsupply.Tailoring.Web.Api.Mappings;

namespace Suitsupply.Tailoring.Web.Api.Tests.Mappings
{
    [TestFixture]
    public class AlterationListResponseFromIResultMapTests
    {
        [Test]
        public void SuccessMap()
        {
            var alterations = Builder<Alteration>
                .CreateListOfSize(3)
                .Build().ToArray();
            var result = Result.SuccessResult(alterations);
            
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<AlterationListResponseFromIResultMap>();
            });
            var mapper = new Mapper(mapperConfiguration);

            var response = mapper.Map<AlterationListResponse>(result);
            
            Assert.That(response.Alterations, Is.Not.Null);

            response.Alterations.Should()
                .BeEquivalentTo(alterations, options =>
                    options.Excluding(a => a.PayDate)
                        .IncludingNestedObjects()
                        .ExcludingMissingMembers()
                );
        }
    }
}