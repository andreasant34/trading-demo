using Microsoft.EntityFrameworkCore;
using Trading.Infrastructure.Data.Repositories;

namespace Trading.Infrastructure.Data.Tests;

public class UserRepositoryTests
{
    [Fact]
    public async Task ListUsers_ShouldReturnAllUsers()
    {
        var context = InitSeededTradingDbContext();
        var userRepository = new UserRepository(context);

        var allUsers = await userRepository.ListUsersAsync();
        Assert.Equal(allUsers.Count(), context.SeededUsers.Count());
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser_When_User_Exists()
    {
        var context = InitSeededTradingDbContext();
        var userRepository = new UserRepository(context);

        var existingUser = await userRepository.GetUserByIdAsync(context.SeededUsers.First().Id);
        Assert.NotNull(existingUser);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnNull_When_User_NotExists()
    {
        var context = InitSeededTradingDbContext();
        var userRepository = new UserRepository(context);

        var notFoundUser = await userRepository.GetUserByIdAsync(context.SeededUsers.Max(x => x.Id) + 1);
        Assert.Null(notFoundUser);
    }

    [Fact]
    public async Task GetUserByEmail_ShouldReturnUser_When_User_Exists()
    {
        var context = InitSeededTradingDbContext();
        var userRepository = new UserRepository(context);

        var existingUser = await userRepository.GetUserByEmailAsync(context.SeededUsers.First().Email);
        Assert.NotNull(existingUser);
    }

    [Fact]
    public async Task GetUserByEmail_ShouldReturnNull_When_User_NotExists()
    {
        var context = InitSeededTradingDbContext();
        var userRepository = new UserRepository(context);

        var notFoundUser = await userRepository.GetUserByEmailAsync("notfoundemail@gmail.com");
        Assert.Null(notFoundUser);
    }

    private static SeededTradingDbContext InitSeededTradingDbContext()
    {
        var options = new DbContextOptionsBuilder<TradingDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;

        var context = new SeededTradingDbContext(options);
        _ = context.Database.EnsureCreated();
        context.SeedUserData();
        return context;
    }
}