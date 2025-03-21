using Fanex.BetCount.Business.Models;
using Fanex.LetstalkService.Client;
using System.Collections.Generic;

namespace Fanex.BetCount.Business.Services;

using LetstalkService.Client.Models;

public interface MessageService
{
    bool Execute(List<BetCountModel> model);
}

public class MessageServiceImp : MessageService
{
    private readonly LetstalkClientService notificationService;
    private readonly MessageContentBuilder messageContentBuilder;

    public MessageServiceImp(
        LetstalkClientService notificationService,
        MessageContentBuilder messageContentBuilder)
    {
        this.notificationService = notificationService;
        this.messageContentBuilder = messageContentBuilder;
    }

    public bool Execute(List<BetCountModel> model)
    {
        var letstalkModel = new LetstalkModel
        {
            TargetName = AppConfigService.Config.TargetName,
            Group = AppConfigService.Config.Group,
            Subject = messageContentBuilder.BuildSubject(AppConfigService.Config.RptDate),
            Content = messageContentBuilder.BuildContent(model)
        };

        return notificationService.SendMessage(letstalkModel);
    }
}