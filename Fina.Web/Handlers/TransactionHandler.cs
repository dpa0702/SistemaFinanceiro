using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using System.Net.Http.Json;

namespace Fina.Web.Handlers
{
    public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/transactions", request);
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                ?? new Response<Transaction?>(null, 400, "Falha ao criar transação.");
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            var result = await _client.DeleteAsync($"v1/transactions/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                   ?? new Response<Transaction?>(null, 400, "Falha ao excluir a categoria.");
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
            => await _client.GetFromJsonAsync<Response<Transaction?>>($"v1/transactions/{request.Id}")
               ?? new Response<Transaction?>(null, 400, "Não foi possível obter a categoria.");

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
            => await _client.GetFromJsonAsync<PagedResponse<List<Transaction>?>>("v1/transactions")
               ?? new PagedResponse<List<Transaction>?>(null, 400, "Não foi possível obter as categorias.");

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                   ?? new Response<Transaction?>(null, 400, "Falha ao atualizar a categoria.");
        }
    }
}
