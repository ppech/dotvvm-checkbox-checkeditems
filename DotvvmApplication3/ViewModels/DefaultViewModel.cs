using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmApplication3.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        public string Title { get; set; }

        public DefaultViewModel()
        {
            Title = "Hello from DotVVM!";
        }

        public GridViewDataSet<ItemDTO> DataGridView { get; set; }
        public List<Guid> DataGridViewCheckedItems { get; set; } = new();
        public GridViewDataSet<ItemDTO> DataRepeater { get; set; }
        public List<Guid> DataRepeaterCheckedItems { get; set; } = new();
        public GridViewDataSet<ItemDTO> DataGridViewKey { get; set; }
        public List<ItemDTO> DataGridViewKeyCheckedItems { get; set; } = new();

        public override async Task Init()
        {
            await base.Init();

            DataGridView = new GridViewDataSet<ItemDTO>
            {
                PagingOptions = { PageSize = 10 },
            };

            DataRepeater = new GridViewDataSet<ItemDTO>
            {
                PagingOptions = { PageSize = 10 },
            };

            DataGridViewKey = new GridViewDataSet<ItemDTO>
            {
                PagingOptions = { PageSize = 10 },
            };
        }

        public override async Task PreRender()
        {
            await base.PreRender();

            if (!Context.IsPostBack || DataGridView.IsRefreshRequired)
            {
                DataGridView.LoadFromQueryable(ItemDTO.GetAll());
            }

            if (!Context.IsPostBack || DataRepeater.IsRefreshRequired)
            {
                DataRepeater.LoadFromQueryable(ItemDTO.GetAll());
            }

            if (!Context.IsPostBack || DataGridViewKey.IsRefreshRequired)
            {
                DataGridViewKey.LoadFromQueryable(ItemDTO.GetAll());
            }
        }

        public void CheckBoxChanged()
        {
            // business logic
        }
    }

    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static IQueryable<ItemDTO> GetAll()
        {
            return Enumerable.Range(1, 50)
                .Select(i => new ItemDTO
                {
                    Id = new Guid(i, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    Name = $"Item {i}"
                })
                .ToList()
                .AsQueryable();
        }
    }

}
