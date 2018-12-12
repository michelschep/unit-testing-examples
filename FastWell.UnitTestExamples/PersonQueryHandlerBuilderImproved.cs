namespace FastWell.UnitTestExamples
{
    public class KindervriendQueryHandlerBuilder
    {
        protected internal Blacklist Blacklist;

        public KindervriendQueryHandlerBuilder()
        {
            // Default summy test blacklist
            Blacklist = new Blacklist()
            {
                "Boeman"
            };
        }

        public KindervriendQueryHandlerBuilder WithBlacklist(Blacklist blacklist)
        {
            Blacklist = blacklist;

            return this;
        }

        public KindervriendQueryHandlerBasedOnBlacklist Build()
        {
            return new KindervriendQueryHandlerBasedOnBlacklist(Blacklist);
        }
    }
}