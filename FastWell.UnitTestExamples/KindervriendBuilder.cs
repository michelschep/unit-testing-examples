namespace FastWell.UnitTestExamples
{
    public class KindervriendBuilder
    {
        protected internal string Name;
        protected internal int Leeftijd;
        protected internal string Woonplaats;

        public KindervriendBuilder()
        {
            // Dummy test values
            Name = "Name";
            Leeftijd = 0;
            Woonplaats = "Noordpool";
        }

        public KindervriendBuilder WithName(string naam)
        {
            Name = naam;

            return this;
        }

        public Kindervriend Build()
        {
            return new Kindervriend(Name, Woonplaats, Leeftijd);
        }
    }
}