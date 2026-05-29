namespace Ecomercio.Application.Common.DTOs;

/// <summary>
/// DTO para respuestas de error
/// </summary>
public record ErrorDto(
    string Message,
    string? Details = null,
    Dictionary<string, string[]>? Errors = null
);