using LeoBase.Backend.Contracts.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LeoBase.Backend.Common;

public static class EntityValidator
{
    public static void Validate<TUnitOfWork>(TUnitOfWork unitOfWork, object[] entities)
    {
        foreach (var entity in entities)
        {
            var validationContext = new ValidationContext(entity, serviceProvider: null, items: null);

            // UnitOfWork injizieren, wenn Entity mit Interface markiert wurde
            if (entity is IDatabaseValidatableObject)
            {
                validationContext.InitializeServiceProvider(_ => unitOfWork);
            }

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(entity, validationContext, validationResults,
                validateAllProperties: true);

            if (!isValid)
            {
                var memberNames = new List<string>();
                var validationExceptions = new List<ValidationException>();

                foreach (ValidationResult validationResult in validationResults)
                {
                    validationExceptions.Add(new ValidationException(validationResult, null,
                        validationResult.MemberNames));
                    memberNames.AddRange(validationResult.MemberNames);
                }

                if (validationExceptions.Count == 1)  // eine Validationexception werfen
                {
                    throw validationExceptions.Single();
                }

                // AggregateException mit allen ValidationExceptions als InnerExceptions werfen
                throw new ValidationException($"Entity validation failed for {string.Join(", ", memberNames)}",
                    new AggregateException(validationExceptions));
            }
        }
    }
}
