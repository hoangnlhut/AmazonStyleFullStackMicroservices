using FluentValidation;
using MediatR;

namespace Ordering.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;   
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                //Run all the validation one by one and return the VAlidation Result
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                //now need to check for any failures
                var failures = validationResults.SelectMany(e => e.Errors).Where(f => f != null).ToList();
                if(failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
