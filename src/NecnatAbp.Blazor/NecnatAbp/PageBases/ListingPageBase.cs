using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NecnatAbp.AppServices;
using NecnatAbp.ComponentBases;
using NecnatAbp.Dtos;
using NecnatAbp.Injections;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace NecnatAbp.PageBases
{
    public abstract class ListingPageBase<
        TSearchComponent,
        TAppService,
        TGetOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TSearchInput>
    : AbpComponentBase
    where TSearchComponent : SearchComponentBase<TAppService,
        TGetOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TSearchInput>, new()
    where TAppService : ICrudsAppService<
        TGetOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TSearchInput>
    where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
    where TGetOutputDto : IEntityDto<TKey>
    {
        [Inject] protected TAppService? AppService { get; set; }
        [Inject] protected PageHistoryState? PageHistoryState { get; set; }
        [Inject] protected NavigationManager? NavigationManager { get; set; }

        protected TSearchComponent SearchComponent = new TSearchComponent();

        protected string? CreatePolicyName { get; set; }
        protected string? UpdatePolicyName { get; set; }
        protected string? DeletePolicyName { get; set; }

        public bool HasCreatePermission { get; set; }
        public bool HasUpdatePermission { get; set; }
        public bool HasDeletePermission { get; set; }

        protected string ListingPageUri { get; set; } = "/";
        protected string CreatePageUri { get; set; } = "/";
        protected string UpdatePageUri { get; set; } = "/";

        protected async override Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await InvokeAsync(StateHasChanged);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var goBackPage = PageHistoryState!.GetGoBackPage();
                if (goBackPage != null && goBackPage.Uri == ListingPageUri)
                {
                    if (goBackPage.Data != null && goBackPage.Data is TSearchInput)
                        SearchComponent.SearchInput = (TSearchInput)goBackPage.Data;

                    PageHistoryState!.RemoveLastPage();
                }
                else
                    PageHistoryState!.Clear();
            }

            await base.OnAfterRenderAsync(firstRender);
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

            if (DeletePolicyName != null)
            {
                HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
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

        protected virtual async Task CheckDeletePolicyAsync()
        {
            await CheckPolicyAsync(DeletePolicyName);
        }

        protected void ToCreatePage()
        {
            PageHistoryState!.AddPageToHistory(ListingPageUri, SearchComponent.SearchInput);
            NavigationManager!.NavigateTo(CreatePageUri);
        }

        protected void ToUpdatePage(TGetOutputDto getOutputDto)
        {
            PageHistoryState!.AddPageToHistory(ListingPageUri, SearchComponent.SearchInput);
            NavigationManager!.NavigateTo(UpdatePageUri + "?id=" + getOutputDto.Id);
        }

        protected virtual async Task DeleteEntityAsync(TGetOutputDto entity)
        {
            try
            {
                await CheckDeletePolicyAsync();
                await OnDeletingEntityAsync();
                await AppService!.DeleteAsync(entity.Id);
                await OnDeletedEntityAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual Task OnDeletingEntityAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual async Task OnDeletedEntityAsync()
        {
            await SearchComponent.RefreshAsync();
        }
    }
}
