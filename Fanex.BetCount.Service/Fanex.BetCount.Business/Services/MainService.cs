using Fanex.BetCount.Business.Models;
using Fanex.MailDelivery.Core.Models;
using System;

namespace Fanex.BetCount.Business.Services;

public interface MainService
{
    JobMessage Execute();
}

public class MainServiceImp : MainService
{
    private readonly Log log;
    private readonly DataService dataService;
    private readonly MessageService messageService;

    public MainServiceImp(
        Log log,
        DataService dataService,
        MessageService messageService)
    {
        this.log = log;
        this.dataService = dataService;
        this.messageService = messageService;
    }

    public JobMessage Execute()
    {
        var result = new ExecuteResultModel();
        var jobMessage = new JobMessage();
        try
        {
            result = SendNotification();
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
            log.Log(e);
        }
        finally
        {
            jobMessage.Mail = result.Mail;
            jobMessage.JobStatus = result.Success ? JobStatus.SuccessWithoutSendmail : JobStatus.DataIsNotReady;
            jobMessage.TaskLog = new TaskLog
            {
                LogTime = DateTime.Now,
                Message = result.Message
            };
        }

        return jobMessage;
    }

    private ExecuteResultModel SendNotification()
    {
        if (!dataService.IsDataAvailable())
        {
            return new ExecuteResultModel { Success = false, Message = "DataIsNotReady, TaskID = 17" };
        }

        var data = dataService.Query();
        var result = messageService.Execute(data);

        return new ExecuteResultModel
        {
            Mail = new Mail { Subject = "Subject", Body = "Body", Title = "Title" },
            Message = string.Empty,
            Success = result
        };
    }
}