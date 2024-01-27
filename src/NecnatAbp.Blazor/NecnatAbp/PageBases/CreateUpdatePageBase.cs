using Blazorise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NecnatAbp.AppServices;
using NecnatAbp.Injections;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;

namespace NecnatAbp.PageBases
{
    public abstract class CreateUpdatePageBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TSearchInput>
    : AbpComponentBase
    where TAppService : ICrudsAppService<
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TSearchInput>
    {
        [Inject] protected TAppService? AppService { get; set; }
        [Inject] protected PageHistoryState? PageHistoryState { get; set; }
        [Inject] protected NavigationManager? NavigationManager { get; set; }

        protected TKey? EntityId;
        protected TCreateUpdateInput? Entity;
        protected Validations? ValidationsRef;

        protected string? CreatePolicyName { get; set; }
        protected string? UpdatePolicyName { get; set; }

        public bool HasCreatePermission { get; set; }
        public bool HasUpdatePermission { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task SetPermissionsAsync()
        {
            if (CreatePolicyName != null)
            {
                HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
            }

            if (UpdatePolicyName != null)
            {
                HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
            }
        }

        protected virtual async Task CheckPolicyAsync(string? policyName)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                return;
            }

            await AuthorizationService.CheckAsync(policyName);
        }

        protected virtual async Task CheckCreatePolicyAsync()
        {
            await CheckPolicyAsync(CreatePolicyName);
        }

        protected virtual async Task CheckUpdatePolicyAsync()
        {
            await CheckPolicyAsync(UpdatePolicyName);
        }

        protected void ToBackPage()
        {
            if (PageHistoryState!.CanGoBack())
                NavigationManager!.NavigateTo(PageHistoryState!.GetGoBackPage()?.Uri ?? "/");
        }

        protected virtual async Task CreateEntityAsync()
        {
            try
            {
                var validate = true;
                if (ValidationsRef != null)
                {
                    validate = await ValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await OnCreatingEntityAsync();

                    await CheckCreatePolicyAsync();
                    await AppService!.CreateAsync(Entity!);

                    await OnCreatedEntityAsync();
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual Task OnCreatingEntityAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnCreatedEntityAsync()
        {
            ToBackPage();
            return Task.CompletedTask;
        }

        protected virtual async Task UpdateEntityAsync()
        {
            try
            {
                var validate = true;
                if (ValidationsRef != null)
                {
                    validate = await ValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await OnUpdatingEntityAsync();

                    await CheckUpdatePolicyAsync();
                    await AppService!.UpdateAsync(EntityId!, Entity!);

                    await OnUpdatedEntityAsync();
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual Task OnUpdatingEntityAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnUpdatedEntityAsync()
        {
            ToBackPage();
            return Task.CompletedTask;
        }
    }
}
