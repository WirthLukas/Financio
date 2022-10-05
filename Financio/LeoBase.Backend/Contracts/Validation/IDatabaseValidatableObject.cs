using System.ComponentModel.DataAnnotations;

namespace LeoBase.Backend.Contracts.Validation;

/// <summary>
/// Marks an Entity that it should be validated with database context
/// </summary>
internal interface IDatabaseValidatableObject : IValidatableObject
{
}
