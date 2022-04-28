using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryTests
{
    public class UserRepositoryTests
    {

        [Fact]
        public void ValidateByFirstName()
        {
            // Order
            var context = GetDatabaseContext().Result;
            var search = "John";

            // Act
            var result = context.UserRepository().GetBySearchFilter(search, false);

            // Assert
            foreach (var item in result)
            {
                Assert.Contains(search, item.FirstName);
            }
        }

        [Fact]
        public void ValidateByLastName()
        {
            // Order
            var context = GetDatabaseContext().Result;
            var search = "Doe";

            // Act
            var result = context.UserRepository().GetBySearchFilter(search, false);

            // Assert
            foreach (var item in result)
            {
                Assert.Contains(search, item.LastName);
            }
        }

        [Fact]
        public void ValidateByFullName()
        {
            // Order
            var context = GetDatabaseContext().Result;
            var search = "Pedro     Garcés ";

            // Act
            var result = context.UserRepository().GetBySearchFilter(search, false);

            // Assert
            foreach (var item in result)
            {
                foreach (var word in search.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    Assert.Contains(word, item.FullName);
                }
            }
        }

        [Fact]
        public void ValidateByUsername()
        {
            // Order
            var context = GetDatabaseContext().Result;
            var username = "peter";

            // Act
            var result = context.UserRepository().GetBySearchFilter(username, false);

            // Assert
            foreach (var item in result)
            {
                Assert.Contains(username, item.Username);
            }
        }

        [Fact]
        public void GerOrderedByFullName()
        {
            // Order
            var context = GetDatabaseContext().Result;
            var search = "Adri";

            // Act
            var result = context.UserRepository().GetBySearchFilter(search, false);

            // Assert
            Assert.Contains("Adrian", result[0].FullName);
            Assert.Contains("Beatriz", result[1].FullName);
        }


        private async Task<DBContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DBContext(options);
            databaseContext.Database.EnsureCreated();

            var users = new List<User>()
            {
                new User { ID = 6, FirstName = "Adrian", LastName = "Perez", Username = "adri", FullName = "Adrian Perez", CreationDate = DateTime.Now},
                new User { ID = 5, FirstName = "Beatriz Adriana", LastName = "García", Username = "adrina", FullName = "Beatriz Adriana García", CreationDate = DateTime.Now},
                new User { ID = 1, FirstName = "John", LastName = "Doe", Username = "johndoe", FullName = "John Doe", CreationDate = DateTime.Now},
                new User { ID = 2, FirstName = "Pedro", LastName = "Garcés", Username = "peter", FullName = "Pedro Garcés", CreationDate = DateTime.Now},
                new User { ID = 3, FirstName = "Juan", LastName = "Domínguez", Username = "nacho", FullName = "Juan Domínguez", CreationDate = DateTime.Now},
                new User { ID = 4, FirstName = "Elías", LastName = "Noevas", Username = "eliasjesus", FullName = "Elías Jesus Noevas", CreationDate = DateTime.Now},
            };

            foreach (var user in users)
            {
                databaseContext.UserRepository().Insert(user);
            }
            await databaseContext.SaveChangesAsync();

            return databaseContext;
        }
    }
}
