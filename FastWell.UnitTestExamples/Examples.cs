using System;
using FluentAssertions;
using Xunit;

namespace FastWell.UnitTestExamples
{
    public class Examples
    {
        // Feedback bij het testen is belangrijk om snel en duidelijk te lezen waarom
        // een test faalt.
        [Fact]
        public void CanFindKindervriend()
        {
            // Arrange
            var handler = new KindervriendQueryHandler();

            // Act 
            var actual = handler.FindDetails("Kerstman");

            // Assert
            var expected = new Kindervriend("Kerstman", "Noordpool", leeftijd: int.MaxValue);
            Assert.Equal(expected, actual);
        }
        // De assert werkt, maar de feedback is (zeker bij grote objecten) lastig te lezen


        // Met Fluent asserts kun je de assertions als zinnen schrijven: leesbaarder dus
        // En de feedback is specifieker
        [Fact]
        public void CanFindKindervriend_BetterFeedbackWithFluentAssertions_Example1()
        {
            // Arrange
            var handler = new KindervriendQueryHandler();

            // Act 
            var actual = handler.FindDetails("Kerstman");

            // Assert
            var expected = new Kindervriend("Kerstman", "Noordpool", int.MaxValue);
            actual.Should().BeEquivalentTo(expected);
        }

        // Feedback Fluent assertions bij strings
        [Fact]
        public void CanFindKindervriend_BetterFeedbackWithFluentAssertions_Example2()
        {
            // Arrange
            var handler = new KindervriendQueryHandler();

            // Act 
            var actual = handler.FindDetails("Kerstman");

            // Assert
            actual.Should().BeEquivalentTo(new Kindervriend("Kersttman", "Noordpool", int.MaxValue));
        }
            
        // Wat gebeurt er wanneer Kindervriend een nieuwe property krijgt (bv telefoonnummer)?
        // Alle tests aanpassen? Dat is juist wat we niet willen.
        [Fact]
        public void CanFindKindervriend_ChangesInModel()
        {
            // Arrange
            var handler = new KindervriendQueryHandler();

            // Act 
            var actual = handler.FindDetails("Sinterklaas");

            // Assert
            // Gebruik een builder zodat je alleen de relevante properties specificeert
            // Zodat je ook alleen maar die aspecten ziet in de test waar het om gaat.
            var expectedKindervriend = new KindervriendBuilder()
                .WithName("Sinterklaas")
                .Build(); //                <== BUILDER voor objecten die je in test gebruikt

            actual.Should().BeEquivalentTo(expectedKindervriend, options => options.Including(x => x.Name));
        }

        // Gebruik een builder test onafhankelijk te maken van constructors en dus ook van de implementatie.
        // De test moet functioneel uitdrukken wat je wilt testen, zodat je de test niet aan hoeft te passen
        // wanneer een internal aangepast wordt.
        [Fact]
        public void CanFindPerson_MaarWeZijnEchtAlleenInDeNaamGeinteresseerd()
        {
            // Arrange
            var handler = new KindervriendQueryHandlerBuilder().Build(); // <== BUILDER voor de SUT

            // Act 
            var actual = handler.ExecuteQuery("Sinterklaas");

            // Assert
            // Gebruik een builder zodat je alleen de relevante properties specificeert
            // Zodat je ook alleen maar die aspecten ziet in de test waar het om gaat.
            // We willen weten of de naam klopt. Andere aspecten boeit on (hier) niet. 
            var expectedKindervriend = new KindervriendBuilder() 
                .WithName("Sinterklaas")
                .Build();                   // <== BUILDER voor objecten die je in test gebruikt

            // Bij testen kun je aangeven waar test naar moet kijken
            // Gebruik options om alleen te asserten op wat relevant is
            actual.Should().BeEquivalentTo(expectedKindervriend, options => options.Including(x => x.Name));
        }
        
        // Wat nu als de handler de kindervriend niet kan vinden?
        // En je kunt ook asserten op expected exceptions
        [Fact]
        public void CannotFindKindervriend()
        {
            // Arrange
            var handler = new KindervriendQueryHandlerBuilder().Build();

            // Act 
            Action zoekKindervriend = () => handler.ExecuteQuery("Boeman");

            // Assert
            zoekKindervriend.Should().Throw<Exception>()
                .WithMessage("Wie is de Boeman?!!11!");
        }

        // Hoe kunnen we injection/blacklist gedrag nu mocken?
        [Fact]
        public void CannotFindKindervriend_MockBlacklist()
        {
            // Arrange
            var handler = new KindervriendQueryHandlerBuilder()
                .WithBlacklist(new Blacklist {"DIT-IS-GEEN-KINDERVRIEND"}) // <== hier kun je dus ook eventuele andere mocks gemaakt met Moq doorgeven
                .Build();

            // Act 
            Action act = () => handler.ExecuteQuery("DIT-IS-GEEN-KINDERVRIEND");

            // Assert
            act.Should().Throw<Exception>()
                .WithMessage("Wie is de DIT-IS-GEEN-KINDERVRIEND?!!11!");
        }

        // Nu voorbeeld van een Theory
        [Fact]
        public void OnePlusOneShouldBeTwo()
        {
            // Arrange
            var pakketjesOpteller = new PakketjesOptellerBuilder().Build();

            // Act 
            var actual = pakketjesOpteller.Sum(1, 1);

            // Assert
            actual.Should().Be(2);
        }

        // Hm... maar we hebben heel veel randgevallen die op dezefde manier getest kunnen worden
        // Ideaal voor formules
        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(-1, 1, 0)]
        [InlineData(21, 21, 42)]
        [InlineData(21, 21, 43)]
        public void XPlusYShouldBeZ(int x, int y, int z)
        {
            // Arrange
            var pakketjesOpteller = new PakketjesOptellerBuilder().Build();

            // Act 
            var actual = pakketjesOpteller.Sum(x, y);

            // Assert
            actual.Should().Be(z);
        }
    }
}