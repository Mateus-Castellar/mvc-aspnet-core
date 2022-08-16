using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Mvc.App.Configuration
{
    public static class GlobalizationConfig
    {
        public static IApplicationBuilder UseGlobalizationCulture(this WebApplication app)
        {
            //Globalizacao da aplicacao para portugues
            var defaultCulture = new CultureInfo("pt-br");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}