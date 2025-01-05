namespace eShopV1.Application.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMessage);

