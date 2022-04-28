using DataAccess.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataAccess.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DBContext context) : base(context) { }

        public User[] GetBySearchFilter(string search, bool orderByDescending)
        {
            search = RemoveDiacritics(search).ToLower();
            var usersQuery = dbSet.ToList().Where(x => ValidateSearch(search, x.FullName, x.Username));
            usersQuery = orderByDescending ?
                    usersQuery.OrderByDescending(x => x.FullName) :
                    usersQuery.OrderBy(x => x.FullName);

            return usersQuery.ToArray();
        }

        private bool ValidateSearch(string search, params string[] inputs)
        {
            var userDataToValidate = inputs.Select(x => RemoveDiacritics(x).ToLower());
            foreach (var word in search.Split(" ", System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (userDataToValidate.Any(x => x.Contains(word)))
                    return true;                
            }
            return false;
        }

        private string RemoveDiacritics(string input)
        {
            var builder = new StringBuilder();
            string normalized = input.Normalize(NormalizationForm.FormD);
            foreach (char ch in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
