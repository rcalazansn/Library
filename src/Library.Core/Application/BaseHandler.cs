using FluentValidation;
using FluentValidation.Results;
using Library.Core.Domain;
using Library.Core.Notification;

namespace Library.Core.Application
{
    public abstract class BaseHandler
    {
        private readonly INotifier _notifier;

        protected BaseHandler(INotifier notifier) =>
          _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
                Notify($"{item.PropertyName} - {item.ErrorMessage}");
        }
        protected void Notify(string mensagem) =>
            _notifier.Handle(new Notication(mensagem));

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : BaseEntity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }
    }
}
