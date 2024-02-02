using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Html;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using System.Drawing;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class UploadButtonComponent
    {

        private bool _isEditing = false;

        [Parameter]
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                    IsEditingChanged.InvokeAsync(value);
                }
            }
        }

        [Parameter]
        public EventCallback<bool> IsEditingChanged { get; set; }

        [Parameter]
        public Func<MouseEventArgs, Task<bool>> UploadFunction { get; set; } = default!;


        public string GetDivCssClass()
        {
            return IsEditing ? "color-green-div" : "color-blue-div";
        }

        private object GetButtonCssClass()
        {
            return IsEditing ? "upload-button-checkmark" : "upload-button-edit";
        }

        public MarkupString GetButtonContent()
        {
            string content = IsEditing ? "&#10003;" : "&#9998;";
            return new MarkupString(content);
        }

        public async Task Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            if (IsEditing)
            {
                bool validAction = await UploadFunction(e);
                if (validAction == false)
                {
                    return;
                }
            }

            IsEditing = !IsEditing;
        }
    }
}
