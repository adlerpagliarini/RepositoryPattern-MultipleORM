using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class DapperUserRelation
    {
        public Dictionary<int, User> DictionaryUserId;

        public DapperUserRelation()
        {
            Dictionary<int, User> DictionaryUserId = new Dictionary<int, User>();
        }
    }
}
