namespace FootballersCatalog.Api.Dtos
{
    public record TeamsResponse(
        Guid Id,
        string? TeamTitle);

    public record AddTeamRequest(
        string TeamTitle);
}
