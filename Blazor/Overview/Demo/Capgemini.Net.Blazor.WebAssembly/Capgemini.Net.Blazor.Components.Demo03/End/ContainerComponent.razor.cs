﻿using System;

namespace Capgemini.Net.Blazor.Components.Demo03.End
{
    public partial class ContainerComponent
    {
        private int maxRate = 6;
        private int iconIndex;

        private readonly string[] icons = {
            "fa-star",
            "fa-grin-stars",
            "fa-angry",
            "fa-sun"
        };

        public string this[int i]
        {
            get => icons[i];
            set => icons[i] = value;
        }

        public int MaxRate
        {
            get => maxRate;
            set => maxRate = Math.Max(2, value);
        }

        public int IconIndex
        {
            get => iconIndex;
            set => iconIndex = value < 0
                ? icons.Length - 1
                : value >= icons.Length
                ? 0
                : value;
        }
    }
}
