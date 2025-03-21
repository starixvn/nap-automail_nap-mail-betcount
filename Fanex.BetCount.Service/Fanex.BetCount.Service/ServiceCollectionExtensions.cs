using Fanex.BetCount.Business.Models;
using Fanex.BetCount.Business.Services;
using Fanex.Data;
using Fanex.Data.Repository;
using Fanex.LetstalkService.Client;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.MailDelivery.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharpRaven;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LetstalkConfig = Fanex.LetstalkService.Client.AppConfigService;

namespace Fanex.BetCount.Service;

using AppConfigService = Business.Services.AppConfigService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Services { get; set; }

    public static void AddConfig(this IServiceCollection services)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var text = File.ReadAllText("appsettings.json");
        AppConfigService.Config = JsonConvert.DeserializeObject<ConfigSettingModel>(text);
        LetstalkConfig.Config.LetstalkServiceApi = AppConfigService.Config.LetstalkServiceApi;
    }

    public static void AddInternalServices(this IServiceCollection services)
    {
        services.AddSingleton<Log, LogImp>();
        services.AddSingleton<IDynamicRepository, DynamicRepository>();
        services.AddSingleton<DataService, DataServiceImp>();
        services.AddSingleton<DateService, DateServiceImp>();
        services.AddSingleton<MainService, MainServiceImp>();
        services.AddSingleton<MessageContentBuilder, MessageContentBuilderImp>();
        services.AddSingleton<MessageService, MessageServiceImp>();
        services.AddSingleton<FormatNumberService, FormatNumberServiceImp>();
        services.AddSingleton<LetstalkClientService, LetstalkClientServiceImp>();

        Services = services;
        MailClientHandler.Start();
    }
}