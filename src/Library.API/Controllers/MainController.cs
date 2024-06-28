﻿using Library.Core.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        protected MainController(INotifier notifier) =>
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        protected bool IsValid() =>
            !_notifier.HasNotification();
        protected void NotifyError(string mensagem) =>
            _notifier.Handle(new Notication(mensagem));

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
        {
            if (IsValid())
            {
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(statusCode),
                };
            }

            return BadRequest(new
            {
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }
    }
}