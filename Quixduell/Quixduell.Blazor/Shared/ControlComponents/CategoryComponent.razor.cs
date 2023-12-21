using Microsoft.AspNetCore.Components;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class CategoryComponent
    {
        [Parameter] 
        public Category Category { get; set; } = default!;
    }
}
