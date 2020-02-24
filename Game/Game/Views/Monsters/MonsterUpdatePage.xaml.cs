using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using Game.ViewModels;
using Game.Models;


namespace Game.Views
{
    /// <summary>
    /// Monster Update Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {
        // View Model for Monster
        readonly GenericViewModel<MonsterModel> ViewModel;

        /// <summary>
        /// Constructor that takes an existing data item
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();
            
            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Need to make the SelectedItem a string, so it can select the correct item.
            DifficultyPicker.SelectedItem = data.Data.DifficultyLevel.ToString();

            ImagePic.SelectedItem = data.Data.ImageURI;

            PrimaryHandPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.PrimaryHand));

            HeadPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.Head));

            NecklacePic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.Necklass));

            OffHandPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.OffHand));

            RightFingerPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.RightFinger));

            LeftFingerPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.LeftFinger));

            FeetPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset.Where(a => a.Location == ItemLocationEnum.Feet));

            UniqueItemPic.ItemsSource = new List<ItemModel>(ItemIndexViewModel.Instance.Dataset);
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If input name is empty or null, diaplay alert and return
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "You need to enter a name!", "OK");
                return;
            }
            // If input difficulty level is unknown, diaplay alert and return
            if (ViewModel.Data.DifficultyLevel == Models.Enum.DifficultyLevelEnum.Unknown)
            {
                await DisplayAlert("Alert", "You need to select a Difficulty!", "OK");
                return;
            }
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }



        /// <summary>
        /// Catch the change to the stepper for Drop Rate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DropRate_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DropRateValue.Text = String.Format("{0} %", e.NewValue);
        }







    }
}