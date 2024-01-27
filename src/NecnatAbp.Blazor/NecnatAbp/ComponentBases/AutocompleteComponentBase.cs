using Blazorise;
using Blazorise.Components;
using Microsoft.AspNetCore.Components;
using NecnatAbp.AppServices;
using NecnatAbp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace NecnatAbp.ComponentBases
{
    public abstract class AutocompleteComponentBase<TEntityDto, TKey, TAppService, TSearchInput> : AbpComponentBase
        where TAppService : IGetAndSearchAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : AutocompleteResultRequestDto, new()
    {
        [Inject] protected TAppService? AppService { get; set; }

        [Parameter]
        public int Qty { get; set; } = 5;

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Action<bool>? DisabledChanged { get; set; }

        [Parameter]
        public Validations? Validations { get; set; }

        [Parameter]
        public Action<ValidatorEventArgs>? Validator { get; set; }

        [Parameter]
        public TEntityDto? SelectedValue { get; set; }

        [Parameter]
        public EventCallback<TEntityDto?> SelectedValueChanged { get; set; }

        protected bool IsLoading = true;
        protected IEnumerable<TEntityDto>? ReadData;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            await InvokeAsync(StateHasChanged);

            if (SelectedValue != null)
            {
                var getOutputDto = await AppService!.GetAsync(SelectedValue.Id);
                ReadData = new List<TEntityDto> { getOutputDto };
                SelectedValue = ReadData.First();
                await SelectedValueChanged.InvokeAsync(SelectedValue);

                if (Validations != null && Validations.ValidateOnLoad)
                    await Validations.ValidateAll();
            }

            await base.OnInitializedAsync();

            IsLoading = false;
            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task OnHandleReadData(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        {
            if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
            {
                if (!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                {
                    if (autocompleteReadDataEventArgs.SearchValue.Length >= 3)
                    {
                        var searchInput = new TSearchInput();
                        searchInput!.GenericSearch = autocompleteReadDataEventArgs.SearchValue;
                        searchInput.MaxResultCount = Qty;
                        var pagedResultDto = await AppService!.SearchAsync(searchInput);
                        ReadData = pagedResultDto.Items;
                    }
                    else
                        ReadData = new List<TEntityDto>();
                }
            }
        }

        public virtual async Task ClearAsync()
        {
            IsLoading = true;
            await InvokeAsync(StateHasChanged);

            SelectedValue = default;
            await SelectedValueChanged.InvokeAsync(SelectedValue);
            await InvokeAsync(StateHasChanged);

            IsLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }
}
