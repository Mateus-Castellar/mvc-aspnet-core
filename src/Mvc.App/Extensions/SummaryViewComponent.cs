﻿using Microsoft.AspNetCore.Mvc;
using Mvc.Business.Interfaces;

namespace Mvc.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());

            notificacoes.ForEach(lbda => ViewData.ModelState
                .AddModelError(string.Empty, lbda.Mensagem));

            return View();
        }
    }
}