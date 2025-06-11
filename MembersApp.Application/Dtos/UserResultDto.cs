namespace MembersApp.Application.Dtos;

public record UserResultDto(string? ErrorMessage)
{
    public bool Succeeded => ErrorMessage == null;
}
