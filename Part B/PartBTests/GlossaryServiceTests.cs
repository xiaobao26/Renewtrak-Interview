using Moq;
using Part_B.Domain.Entities;
using Part_B.Domain.Interfaces;
using Part_B.Services;
using FluentAssertions;
using Part_B.Domain.Dtos;
using Part_B.Domain.Exceptions;

namespace PartBTests;

public class GlossaryServiceTests
{
    private readonly Mock<IGlossaryRepository> _mock = new();
    private readonly CancellationToken _ct = CancellationToken.None;
    
    // System Under Test
    private IGlossaryService SUT() => new GlossaryService(_mock.Object);
        
    
    [Fact]
    public async Task GetAllAsync_ReturnMappedDtos()
    {
        _mock.Setup(r => r.GetAllAsync(_ct)).ReturnsAsync(new List<GlossaryTerm>
        {
            new() { Id = Guid.NewGuid(), Term = "alpha", Definition = "A" },
            new() { Id = Guid.NewGuid(), Term = "beta",  Definition = "B" }
        });

        var result = await SUT().GetAllAsync(_ct);
        
        result.Should().BeEquivalentTo(new[]
        {
            new { Term = "alpha", Definition = "A" },
            new { Term = "beta",  Definition = "B" }
        }, o => o.ExcludingMissingMembers());
    }
    
    [Fact]
    public async Task GetByIdAsync_Found_ReturnsDto()
    {
        var id = Guid.NewGuid();
        _mock.Setup(r => r.GetByIdAsync(id, _ct))
            .ReturnsAsync(new GlossaryTerm { Id = id, Term = "t", Definition = "d" });

        var dto = await SUT().GetByIdAsync(id, _ct);

        dto.Should().BeEquivalentTo(new { Id = id, Term = "t", Definition = "d" });
    }
    
    [Fact]
    public async Task GetByIdAsync_NotFound_Should_Throw_NotFoundException()
    {
        var id = Guid.NewGuid();
        _mock.Setup(r => r.GetByIdAsync(id, _ct)).ReturnsAsync((GlossaryTerm?)null);


        Func<Task> act = () => SUT().GetByIdAsync(id, _ct);

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{id}*");
    }
    
    [Fact]
    public async Task UpdateAsync_Found_Update_And_ReturnsDto()
    {
        var id = Guid.NewGuid();
        var entity = new GlossaryTerm { Id = id, Term = "old", Definition = "old" };
        _mock.Setup(r => r.GetByIdAsync(id, _ct)).ReturnsAsync(entity);
        _mock.Setup(r => r.SaveChangesAsync(_ct)).Returns(Task.CompletedTask);

        var dto = await SUT().UpdateAsync(id, new GlossaryTermRequestDto(" x ", " y "), _ct);

        entity.Term.Should().Be("x");
        entity.Definition.Should().Be("y");
        dto.Should().BeEquivalentTo(new { Id = id, Term = "x", Definition = "y" });
        _mock.Verify(r => r.SaveChangesAsync(_ct), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_NotFound_Should_Throw_NotFoundException()
    {
        var id = Guid.NewGuid();
        _mock.Setup(r => r.GetByIdAsync(id, _ct)).ReturnsAsync((GlossaryTerm?)null);

        Func<Task> act = () => SUT().UpdateAsync(id, new GlossaryTermRequestDto("x", "y"), _ct);

        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task DeleteAsync_Found_Removes_And_Saves()
    {
        var id = Guid.NewGuid();
        var entity = new GlossaryTerm { Id = id, Term = "t", Definition = "d" };
        _mock.Setup(r => r.GetByIdAsync(id, _ct)).ReturnsAsync(entity);
        _mock.Setup(r => r.RemoveAsync(entity, _ct)).Returns(Task.CompletedTask);
        _mock.Setup(r => r.SaveChangesAsync(_ct)).Returns(Task.CompletedTask);

        await SUT().DeleteAsync(id, _ct);

        _mock.Verify(r => r.RemoveAsync(entity, _ct), Times.Once);
        _mock.Verify(r => r.SaveChangesAsync(_ct), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_NotFound_Should_Throw_NotFoundException()
    {
        var id = Guid.NewGuid();
        _mock.Setup(r => r.GetByIdAsync(id, _ct)).ReturnsAsync((GlossaryTerm?)null);

        Func<Task> act = () => SUT().DeleteAsync(id, _ct);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}