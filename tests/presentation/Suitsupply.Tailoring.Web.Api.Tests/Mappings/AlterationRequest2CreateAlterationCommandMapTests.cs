using AutoMapper;
using FizzWare.NBuilder;
using NUnit.Framework;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Controllers.Requests;
using Suitsupply.Tailoring.Web.Api.Mappings;

namespace Suitsupply.Tailoring.Web.Api.Tests.Mappings
{
    [TestFixture]
    public class AlterationRequest2CreateAlterationCommandMapTests
    {
        [Test]
        public void SuccessMap()
        {
            var request = Builder<AlterationRequest>.CreateNew()
                .With(r => r.ShortenSleeves, Builder<Shortening>.CreateNew().Build())
                .With(r => r.ShortenTrousers, Builder<Shortening>.CreateNew().Build())
                .Build();
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<AlterationRequest2CreateAlterationCommandMap>();
            });
            var mapper = new Mapper(mapperConfiguration);

            var command = mapper.Map<CreateAlterationCommand>(request);
            
            Assert.That(command.Alteration, Is.Not.Null);

            var alteration = command.Alteration;
            Assert.That(alteration.CustomerId, Is.EqualTo(request.CustomerId));
            Assert.That(alteration.ShortenSleevesLeft, Is.EqualTo(request.ShortenSleeves.Left));
            Assert.That(alteration.ShortenSleevesRight, Is.EqualTo(request.ShortenSleeves.Right));
            Assert.That(alteration.ShortenTrousersLeft, Is.EqualTo(request.ShortenTrousers.Left));
            Assert.That(alteration.ShortenTrousersRight, Is.EqualTo(request.ShortenTrousers.Right));
        } 
    }
}