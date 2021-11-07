using FluentAssertions;
using LibraryUsage.Controllers;
using LibraryUsage.Database;
using LibraryUsage.Mapper;
using LibraryUsage.Models;
using LibraryUsage.Services;
using LibraryUsage.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LibraryUsageTests.UserController_IntegrationTests
{
    public abstract class UserController_IntegrationTests : IDisposable
    {
        private readonly Context _context;
        private readonly List<User> _usersInserted;

        public UserController_IntegrationTests()
        {
            _context = new Context(new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDb").Options);
            _usersInserted = InsertUsers();
        }

        public void Dispose()
        {
            var users = _context.Users.ToList();
            _context.Users.RemoveRange(users);
            _context.SaveChanges();
        }

        [Collection("Sequential")]
        public class Get_Without_Id_Parameter : UserController_IntegrationTests
        {
            [Fact]
            public void Should_Return_All_Users()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = sut.Get();

                //Then
                var okResult = Assert.IsType<OkObjectResult>(result);
                var users = Assert.IsType<List<User>>(okResult.Value);
                users.Should().NotBeNull();
                users.Count.Should().Be(_usersInserted.Count);
                users.Should().BeEquivalentTo(_usersInserted);
            }
        }

        [Collection("Sequential")]
        public class Get_With_Id_Parameter : UserController_IntegrationTests
        {
            [Fact]
            public void When_ID_Exists_Should_Return_User()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = sut.Get(_usersInserted[0].Id);

                //Then
                var okResult = Assert.IsType<OkObjectResult>(result);
                var user = Assert.IsType<User>(okResult.Value);
                user.Should().NotBeNull();
                user.Should().BeEquivalentTo(_usersInserted[0]);
            }

            [Fact]
            public void When_ID_Doesnt_Exists_Should_Return_NotFound()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = sut.Get(-1);

                //Then
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Collection("Sequential")]
        public class Delete : UserController_IntegrationTests
        {
            [Fact]
            public async Task When_ID_Exists_Should_Return_Removed_User()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = await sut.Delete(_usersInserted[0].Id);
                var usersInDbCount = _context.Users.Count();

                //Then
                var okResult = Assert.IsType<OkObjectResult>(result);
                var user = Assert.IsType<User>(okResult.Value);
                user.Should().NotBeNull();
                user.Should().BeEquivalentTo(_usersInserted[0]);
                usersInDbCount.Should().Be(_usersInserted.Count - 1);
            }

            [Fact]
            public async Task When_ID_Doesnt_Exists_Should_Return_NotFound()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = await sut.Delete(-1);

                //Then
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        [Collection("Sequential")]
        public class Create : UserController_IntegrationTests
        {
            private CreateUser _createUser;

            public Create() : base()
            {
                _createUser = new CreateUser()
                {
                    Name = "P",
                    Surname = "P",
                    Email = "e",
                    Address = "L",
                    PhoneNr = "+",
                    Password = "A"
                };
            }

            [Fact]
            public async Task When_CreateUser_Is_Valid_Should_Return_Ok_And_User()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = await sut.Create(_createUser);

                //Then
                var okResult = Assert.IsType<OkObjectResult>(result);
                var user = Assert.IsType<User>(okResult.Value);
                user.Should().NotBeNull();
                var createdUser = _createUser.ConvertToUser();
                createdUser.Id = user.Id;   // Created User doesnt have ID property so we convert it to User and set Id equal to user returned by controller
                user.Should().BeEquivalentTo(createdUser);
            }

            [Fact]
            public async Task When_CreateUser_Is_InValid_Should_Return_BadRequest()
            {
                //Given
                var sut = CreateSut(false);

                //When
                var result = await sut.Create(_createUser);

                //Then
                Assert.IsType<BadRequestObjectResult>(result);
                _context.Users.Count().Should().Be(_usersInserted.Count);
            }
        }

        [Collection("Sequential")]
        public class Update : UserController_IntegrationTests
        {
            private CreateUser _updateUser;

            public Update() : base()
            {
                _updateUser = new CreateUser()
                {
                    Name = "P",
                    Surname = "P",
                    Email = "e",
                    Address = "L",
                    PhoneNr = "+",
                    Password = "A"
                };
            }

            [Fact]
            public async Task When_UpdateUser_Is_Valid_Should_Return_Ok_And_UpdatedUser()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = await sut.Update(_usersInserted[0].Id, _updateUser);

                var updatedUser = _updateUser.ConvertToUser();
                updatedUser.Id = _usersInserted[0].Id;

                var updatedUserInDB = _context.Users.Where(x => x.Id == updatedUser.Id).FirstOrDefault();
                //Then
                var okResult = Assert.IsType<OkObjectResult>(result);
                var user = Assert.IsType<User>(okResult.Value);
                user.Should().NotBeNull();
                user.Should().BeEquivalentTo(updatedUserInDB);
                user.Should().BeEquivalentTo(updatedUser);
            }

            [Fact]
            public async Task When_UpdateUser_Is_Invalid_Should_Return_BadRequest()
            {
                //Given
                var sut = CreateSut(false);

                //When
                var result = await sut.Update(_usersInserted[0].Id, _updateUser);

                //Then
                Assert.IsType<BadRequestObjectResult>(result);
            }

            [Fact]
            public async Task When_UpdateUser_ID_DoesntExists_Should_Return_NotFound()
            {
                //Given
                var sut = CreateSut(true);

                //When
                var result = await sut.Update(-1,_updateUser);

                //Then
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        private List<User> InsertUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Petras",
                    Surname="Petrauskas",
                    Email="el@gmail.com",
                    Address="Lietuva",
                    PhoneNr="+37061234567",
                    Password="Aa123!qwe"
                },
                new User()
                {
                    Name = "Algis",
                    Surname="Alg",
                    Email="el@gmail.com",
                    Address="Anglija",
                    PhoneNr="+37061234568",
                    Password="Aa1#3!qwe"
                }
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();

            return users;
        }

        private UserController CreateSut(bool userValidatorReturns)
        {
            var userValidator = new Mock<IUserValidator>();
            userValidator.Setup(x => x.IsValid(It.IsAny<CreateUser>())).Returns(userValidatorReturns);

            var userService = new UserService(_context, userValidator.Object);
            return new UserController(userService);
        }
    }
}
