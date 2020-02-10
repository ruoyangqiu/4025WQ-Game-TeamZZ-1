using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;


using Xamarin.Forms.Xaml;

namespace Game.Views
{
    
    [DesignTimeVisible(false)]
    public partial class CharacterCreatePage : ContentPage
    {
        GenericViewModel<CharacterModel> ViewModel { get; set; }
        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();
            data.Data = new CharacterModel();

            CharacterClassPicker.SelectedItem = data.Data.CharacterClass.ToString();
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