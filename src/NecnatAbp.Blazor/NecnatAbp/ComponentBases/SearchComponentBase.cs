using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using NecnatAbp.AppServices;
using NecnatAbp.Dtos;
using NecnatAbp.Enums;
using NecnatAbp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace NecnatAbp.ComponentBases
{
    public abstract class SearchComponentBase<
        TAppService,
        TGetOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TSearchInput>
    : AbpComponentBase
    where TAppService : ICrudsAppService<
        TGetOutputDto,
        TKey,
        TGetListInput,
        TCreateUpdateInput,
        TSearchInput>
    where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService? AppService { get; set; }

        [Parameter]
        public int PageSize { get; set; } = LimitedResultRequestDto.DefaultMaxResultCount;

        [Parameter]
        public NnDataGridSelectionMode SelectionMode { get; set; } = NnDataGridSelectionMode.None;

        [Parameter]
        public TGetOutputDto? SelectedRow { get; set; }

        [Parameter]
        public EventCallback<TGetOutputDto?> SelectedRowChanged { get; set; }

        [Parameter]
        public List<TGetOutputDto>? SelectedRows { get; set; }

        [Parameter]
        public EventCallback<List<TGetOutputDto>?> SelectedRowsChanged { get; set; }

        protected int CurrentPage = 1;
        protected string? CurrentSorting;
        protected int? TotalCount;
        protected IReadOnlyList<TGetOutputDto> Entities = Array.Empty<TGetOutputDto>();

        protected Validations? SearchValidationsRef;
        public TSearchInput SearchInput = new TSearchInput();

        protected virtual async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TGetOutputDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.SortField + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;

            await GetEntitiesAsync();

            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task GetEntitiesAsync()
        {
            try
            {
                await UpdateGetListInputAsync();
                var result = await AppService!.SearchAsync(SearchInput);
                Entities = result.Items;
                TotalCount = (int?)result.TotalCount;
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual Task UpdateGetListInputAsync()
        {
            if (SearchInput is ISortedResultRequest sortedResultRequestInput)
            {
                sortedResultRequestInput.Sorting = CurrentSorting;
            }

            if (SearchInput is IPagedResultRequest pagedResultRequestInput)
            {
                pagedResultRequestInput.SkipCount = (CurrentPage - 1) * PageSize;
            }

            if (SearchInput is ILimitedResultRequest limitedResultRequestInput)
            {
                limitedResultRequestInput.MaxResultCount = PageSize;
            }

            return Task.CompletedTask;
        }

        protected virtual async Task SelectAllAsync()
        {
            if (SelectedRows != null && SelectedRows.Count > 0)
            {
                SelectedRows = new List<TGetOutputDto>();
                await SelectedRowsChanged.InvokeAsync(SelectedRows);
                return;
            }

            if (SelectionMode == NnDataGridSelectionMode.Multiple)
            {
                SearchInput.IsPaged = false;

                await GetEntitiesAsync();

                if (SelectedRows != null && SelectedRows.Count > 0)
                    SelectedRows = JsonUtil.RemakeList(Entities.ToList(), SelectedRows);

                SelectedRows = Entities.ToList();
            }

            await SelectedRowsChanged.InvokeAsync(SelectedRows);
            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task RemakeListAsync()
        {
            SelectedRows = JsonUtil.RemakeList(Entities.ToList(), SelectedRows!);

            await SelectedRowsChanged.InvokeAsync(SelectedRows);
            await InvokeAsync(StateHasChanged);
        }

        public virtual async Task RefreshAsync()
        {
            await GetEntitiesAsync();
        }

        public virtual async Task ClearAsync()
        {
            SearchInput = new TSearchInput();
            await InvokeAsync(StateHasChanged);
        }
    }
}
