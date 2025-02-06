using System.ComponentModel.DataAnnotations;

namespace FootballersCatalog.Api.Dtos
{
    public record FootballersResponse(
        Guid Id,
        string? Name,
        string? Surname,
        string? Gender,
        DateTime? Birthday,
        string? Country,
        string? TeamTitle,
        Guid? TeamId);

    public record AddFootballerRequest(
        string Name,
        string Surname,
        string Gender,
        DateTime Birthday,
        [Required, EnumDataType(typeof(Country))]
        string Country,
        Guid TeamId);

    public enum Country
    {
        Russia = 0,
        USA = 1,
        Italy = 2,
    }

    public record UpdateFootballerRequest(
        string Name,
        string Surname,
        string Gender,
        DateTime Birthday,
        [Required, EnumDataType(typeof(Country))]
        string Country,
        Guid TeamId);
}
