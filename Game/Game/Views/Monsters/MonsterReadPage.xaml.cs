using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Monster Read Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterReadPage : ContentPage
    {
        // View Model for Monster
        readonly GenericViewModel<MonsterModel> ViewModel;

        /// <summary>
        /// Constructor that takes an existing data item
        /// </summary>
        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(new GenericViewModel<MonsterModel>(ViewModel.Data))));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterDeletePage(new GenericViewModel<MonsterModel>(ViewModel.Data))));
            await Navigation.PopAsync();
        }
    }
}