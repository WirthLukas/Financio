using LeoBase.Backend.Contracts.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace LeoBase.Backend.Common;

// <inheritdoc cref="IEntityObject" />
public class EntityObject : IEntityObject
{
    /// <inheritdoc />
    [Key] public int Id { get; set; }

    /// <inheritdoc />
    [Timestamp] public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
