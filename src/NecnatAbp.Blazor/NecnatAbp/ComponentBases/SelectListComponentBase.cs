using Microsoft.AspNetCore.Components;
using NecnatAbp.AppServices;
using NecnatAbp.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace NecnatAbp.ComponentBases
{
    public abstract class SelectListComponentBase<TEntityDto, TKey, TAppService, TSearchInput> : AbpComponentBase
        where TAppService : IGetAndSearchAppService<TEntityDto, TKey, TSearchInput>
        where TEntityDto : IEntityDto<TKey>
        where TKey : struct
        where TSearchInput : OptionalPagedAndSortedResultRequestDto, new()
    {
        [Inject] protected TAppService? AppService { get; set; }

        [Parameter]
        public List<TEntityDto>? Data { get; set; }

        [Parameter]
        public TEntityDto? SelectedValue { get; set; }

        [Parameter]
        public EventCallback<TEntityDto?> SelectedValueChanged { get; set; }

        TKey _internalSelectedValue;
        public TKey InternalSelectedValue
        {
            get => _internalSelectedValue;
            set
            {
                if (_internalSelectedValue.Equals(value))
                    return;

                _internalSelectedValue = value;
                InvokeAsync(InternalSelectedValueChanged);
            }
        }

        protected async Task InternalSelectedValueChanged()
        {
            SelectedValue = Data!.Where(x => x.Id.Equals(_internalSelectedValue)).FirstOrDefault();
            await SelectedValueChanged.InvokeAsync(SelectedValue);
        }

        protected override async Task OnInitializedAsync()
        {
            if (Data == null)
            {
                var pagedResultDto = await AppService!.SearchAsync(new TSearchInput { IsPaged = false });
                Data = pagedResultDto.Items.ToList();
            }

            if (SelectedValue != null)
                if (Data.Any(x => x.Id.Equals(SelectedValue.Id)))
                    _internalSelectedValue = SelectedValue.Id;
                else
                    _internalSelectedValue = default(TKey);

            await base.OnInitializedAsync();
        }
    }
}
