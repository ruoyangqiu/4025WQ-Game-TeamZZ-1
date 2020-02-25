using Game.Helpers;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;


using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// Character Create Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CharacterCreatePage : ContentPage
    {
        // the character view model
        GenericViewModel<CharacterModel> ViewModel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();
            data.Data = new CharacterModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";

            CharacterClassPicker.SelectedItem = data.Data.CharacterClass.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If the input Name is Empty, display alert and return 
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "You need to enter a name", "OK");
                return;
            }

            // If the input CharacterClass is Unknown, display alert and return
            if(ViewModel.Data.CharacterClass == CharacterClassEnum.Unknown)
            {
                await DisplayAlert("Alert", "You need to select a Class", "OK");
                return;
            } 

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
              

        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}