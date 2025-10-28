using FluentValidation;
using MediatR;

namespace Trading.Core.Behaviors
{
    /// <summary>
    /// Applies all validators with each applicable request
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidationBehavior(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                await ApplyValidatorsAsync(request, cancellationToken);
            }

            return await next();
        }

        private async Task ApplyValidatorsAsync(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationTasks = _validators
                .Where(x => x.CanValidateInstancesOfType(typeof(TRequest)))
                .Select(v => v.ValidateAsync(context, cancellationToken));

            var failures = (await Task.WhenAll(validationTasks))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }
    }
}
