using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Transactions
{
    public partial class UpdateTransactionsPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public UpdateTransactionRequest InputModel { get; set; } = new();
        public Transaction Transactions { get; set; } = new();

        #endregion

        #region Services

        [Inject]
        public ITransactionHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        #endregion

        #region Parameters

        [Parameter]
        public int IdTransacao { get; set; }

        #endregion Parameters

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetTransactionByIdRequest
                {
                    Id = IdTransacao
                };
                var result = await Handler.GetByIdAsync(request);
                if (result.IsSuccess)
                    Transactions = result.Data;
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region Methods

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                InputModel.Id = Transactions.Id;
                InputModel.Title = Transactions.Title;
                InputModel.PaidOrReceivedAt = Transactions.PaidOrReceivedAt;
                InputModel.Type = Transactions.Type;
                InputModel.Amount = Transactions.Amount;
                InputModel.CategoryId = Transactions.CategoryId;

                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/transacoes");
                }
                else
                    Snackbar.Add(result.Message, Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
