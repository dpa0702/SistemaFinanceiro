using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categories
{
    public partial class UpdateCategoryPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public UpdateCategoryRequest InputModel { get; set; } = new();
        public Category Category { get; set; } = new();

        #endregion

        #region Services

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        #endregion

        [Parameter]
        public int IdCategory { get; set; }

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetCategoryByIdRequest
                {
                    Id = IdCategory
                };
                var result = await Handler.GetByIdAsync(request);
                if (result.IsSuccess)
                    Category = result.Data;
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
                InputModel.Id = Category.Id;
                InputModel.Title = Category.Title;
                InputModel.Description = Category.Description;

                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/categorias");
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
