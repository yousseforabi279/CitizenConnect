using Application.Common;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // No validators registered
        if (!_validators.Any())
        {
            return await next();
        }

        // Create validation context
        var context = new ValidationContext<TRequest>(request);

        // Execute all validators
        var validationResults = await Task.WhenAll(
            _validators.Select(
                validator => validator.ValidateAsync(
                    context,
                    cancellationToken)));

        // Collect errors
        var errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .Select(error => error.ErrorMessage)
            .ToList();

        // Validation failed
        if (errors.Any())
        {
            var errorMessage = string.Join(" | ", errors);

            // Since your commands return Result<T>,
            // create the Result<T> dynamically.
            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse)
                    .GetGenericArguments()[0];

                var failureMethod = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(
                        nameof(Result<object>.Failure),
                        new[]
                        {
                            typeof(ResultStatus),
                            typeof(string)
                        });

                var result = failureMethod!.Invoke(
                    null,
                    new object[]
                    {
                        ResultStatus.ValidationError,
                        errorMessage
                    });

                return (TResponse)result!;
            }
        }

        // Validation passed
        return await next();
    }
}