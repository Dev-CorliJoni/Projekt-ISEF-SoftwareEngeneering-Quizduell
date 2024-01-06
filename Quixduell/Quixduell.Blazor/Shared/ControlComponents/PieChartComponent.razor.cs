using Microsoft.AspNetCore.Components;
using System;
using System.Net.NetworkInformation;

namespace Quixduell.Blazor.Shared.ControlComponents
{
    public partial class PieChartComponent
    {
        [Parameter]
        public List<PieChartPart> Parts { get; set; }

        private string _conicGradient;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _conicGradient = "conic-gradient(";

            for (int i = 0; i < Parts.Count; i++)
            {
                int lastPercentage = i > 0 ? Parts[i - 1].Percentage : 0;
                _conicGradient = $"{_conicGradient}{Parts[i].Color} {lastPercentage}% {Parts[i].Percentage}%,";
            }

            _conicGradient = $"{_conicGradient.Substring(0, _conicGradient.Length - 1)});";
        }
    }

    public class PieChartPart 
    {
        public int Percentage { get; set; }
        public string Color { get; set; }

        public PieChartPart(int percentage, string color)
        {
            Percentage = percentage;
            Color = color;
        }
    }
}
