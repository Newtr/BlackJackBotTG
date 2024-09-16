using Telegram.Bot;
using TelegramBot;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new ChatTypeConverter()));

        services.AddSingleton<ITelegramBotClient>(provider =>
            new TelegramBotClient("7383880918:AAEqtmZa_MkfdQIdCukKppx98_AGWf-ZfpA"));
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
