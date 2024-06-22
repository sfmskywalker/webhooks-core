using System.Net.Http.Json;
using Polly;
using Polly.Extensions.Http;

namespace WebhooksCore.Services;

public class HttpWebhookEndpointInvoker(IHttpClientFactory httpClientFactory) : IWebhookEndpointInvoker
{
    public async Task InvokeAsync(WebhookEndpoint webhookEndpoint, WebhookEvent webhookEvent, CancellationToken cancellationToken = default)
    {
        var httpClient = httpClientFactory.CreateClient();
        var retryPolicy = GetRetryPolicy();

        var response = await retryPolicy.ExecuteAsync(
            async () => await httpClient.PostAsJsonAsync(webhookEndpoint.Url, webhookEvent, cancellationToken));

        response.EnsureSuccessStatusCode();
    }

    private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        // TODO: Make retry policies configurable.
        var retryAttempts = 3;

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .RetryAsync(retryAttempts);
    }
}