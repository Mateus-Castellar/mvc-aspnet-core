using Microsoft.AspNetCore.Mvc;
using Mvc.Business.Interfaces;

namespace Mvc.App.Controllers
{
    //Aqui fica todo codigo em comum ou compartilhado por todas as controllers
    public class BaseController : Controller
    {
        private readonly INotificador _notificador;

        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return _notificador.TemNotificacoes() is false;
        }
    }
}