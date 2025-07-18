﻿using Microsoft.Maui.Controls; // Make sure this is included for ContentPage
using Elevate.ViewModels;

namespace Elevate
{
    public partial class TodaysTaskPage : ContentPage
    {
        public TodaysTaskPage(TodaysTaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel; // This correctly sets the BindingContext via DI
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is AddTaskViewModel vm)
            {
                //vm.SaveContentCommand.Execute(null);
            }
        }
    }
}