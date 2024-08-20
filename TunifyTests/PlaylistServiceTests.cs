using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Services;
using TunifyPlatform.Data;

public class ArtistRepositoryTests
{
    private Mock<DbSet<Artist>> CreateMockDbSet(List<Artist> artists)
    {
        var queryableList = artists.AsQueryable();
        var mockSet = new Mock<DbSet<Artist>>();
        mockSet.As<IQueryable<Artist>>().Setup(m => m.Provider).Returns(queryableList.Provider);
        mockSet.As<IQueryable<Artist>>().Setup(m => m.Expression).Returns(queryableList.Expression);
        mockSet.As<IQueryable<Artist>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
        mockSet.As<IQueryable<Artist>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

        return mockSet;
    }

    [Fact]
    public async Task GetArtistByIdAsync_ReturnsArtist_WhenArtistExists()
    {
        // Arrange
        var mockContext = new Mock<TunifyDbContext>(new DbContextOptions<TunifyDbContext>());
        var mockArtistSet = CreateMockDbSet(new List<Artist>
        {
            new Artist { Id = 1, Name = "Artist 1" },
            new Artist { Id = 2, Name = "Artist 2" }
        });

        mockContext.Setup(c => c.Artists).Returns(mockArtistSet.Object);
        var repository = new ArtistRepository(mockContext.Object);

        // Act
        var result = await repository.GetArtistByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Artist 1", result.Name);
    }

    [Fact]
    public async Task GetArtistByIdAsync_ReturnsNull_WhenArtistDoesNotExist()
    {
        // Arrange
        var mockContext = new Mock<TunifyDbContext>(new DbContextOptions<TunifyDbContext>());
        var mockArtistSet = CreateMockDbSet(new List<Artist>
        {
            new Artist { Id = 1, Name = "Artist 1" },
            new Artist { Id = 2, Name = "Artist 2" }
        });

        mockContext.Setup(c => c.Artists).Returns(mockArtistSet.Object);
        var repository = new ArtistRepository(mockContext.Object);

        // Act
        var result = await repository.GetArtistByIdAsync(3);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddArtistAsync_AddsArtistSuccessfully()
    {
        // Arrange
        var mockContext = new Mock<TunifyDbContext>(new DbContextOptions<TunifyDbContext>());
        var mockArtistSet = new Mock<DbSet<Artist>>();
        mockContext.Setup(c => c.Artists).Returns(mockArtistSet.Object);

        var repository = new ArtistRepository(mockContext.Object);
        var newArtist = new Artist { Id = 3, Name = "Artist 3" };

        // Act
        await repository.AddArtistAsync(newArtist);

        // Assert
        mockArtistSet.Verify(m => m.AddAsync(newArtist, default), Times.Once());
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }
}