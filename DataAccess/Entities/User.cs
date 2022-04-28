using System;

namespace DataAccess.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
    }
}
