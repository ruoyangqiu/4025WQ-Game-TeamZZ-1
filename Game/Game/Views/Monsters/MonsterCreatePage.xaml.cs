using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    [DesignTimeVisible(false)]
    public partial class MonsterCreatePage : ContentPage
    {
        GenericViewModel<MonsterModel> ViewMode { get; set; }
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            data.Data = new MonsterModel();

            BindingContext = this.ViewMode = data;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}