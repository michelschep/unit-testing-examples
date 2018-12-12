using System;
using System.Collections.Generic;
using System.Linq;

namespace FastWell.UnitTestExamples
{
    public class KindervriendQueryHandler
    {
        public Kindervriend FindDetails(string name)
        {
            if (name.ToLower().Equals("paashaas"))
                throw new Exception("Wie is de Paashaas?!!11!");
    
            return new Kindervriend(name, "Noordpool", int.MaxValue-20000);
        }
    }

    public class KindervriendQueryHandlerBasedOnBlacklist
    {
        private readonly Blacklist _blacklist;

        public KindervriendQueryHandlerBasedOnBlacklist(Blacklist blacklist)
        {
            _blacklist = blacklist;
        }

        public Kindervriend ExecuteQuery(string name)
        {
            if (_blacklist.Any(b => b.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                throw new Exception($"Wie is de {name}?!!11!");

            return new Kindervriend(name, "Noordpool", int.MaxValue);
        }
    }

    public class Blacklist : List<string>
    {
    }
}