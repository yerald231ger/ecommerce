using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull

{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handle Request: {request}, Response: {response}, Request Data: {data}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);
        var timer = Stopwatch.StartNew();

        timer.Start();

        var response = await next();

        timer.Stop();

        var timeElapsed = timer.Elapsed;
        if (timeElapsed.Seconds > 3)
            logger.LogWarning("[End] Handle Request: {request}, Response: {response}, Time Elapsed: {timeElapsed}",
                typeof(TRequest).Name, typeof(TResponse).Name, timeElapsed);
        
        return response;
    }
}