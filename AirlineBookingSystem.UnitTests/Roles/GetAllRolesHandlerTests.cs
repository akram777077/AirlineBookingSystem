using AirlineBookingSystem.Application.Features.Roles.Queries.All;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Enums;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Roles;

public class GetAllRolesHandlerTests
{
    [Fact]
    public async Task GetAllRoles_ShouldReturnAllRoles()
    {
        var roles = Enum.GetValues(typeof(UserRoleEnum))
            .Cast<UserRoleEnum>()
            .Select(role => new Role
                {
                    Id = (int)role,
                    RoleName = role
                }

            )
            .ToList();
        var rolesDto = Enum.GetValues(typeof(UserRoleEnum))
            .Cast<UserRoleEnum>()
            .Select(role => new RoleDto
                {
                    Id = (int)role,
                    RoleName = role.ToString()
                }

            )
            .ToList();
        var mockRoleRepository = new Mock<IRoleRepository>();
        var mockMapper = new Mock<IMapper>();
        mockRoleRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(roles);
        mockMapper.Setup(m => m.Map<IReadOnlyCollection<RoleDto>>(roles))
            .Returns(rolesDto);
        var handler = new GetAllRolesHandler(mockRoleRepository.Object, mockMapper.Object);
        var query = new GetAllRolesQuery();
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();
        // Assert
        Assert.NotNull(resultList);
        Assert.Equal(rolesDto.Count, resultList.Count);
        for (int i = 0; i < rolesDto.Count; i++)
        {
            Assert.Equal(rolesDto[i].Id, resultList[i].Id);
            Assert.Equal(rolesDto[i].RoleName, resultList[i].RoleName);
        }
        
    }
}