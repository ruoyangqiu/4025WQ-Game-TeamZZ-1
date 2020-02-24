using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// Create Monster
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterCreatePage : ContentPage
    {
        // Monster to create
        GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Constructor for create to make a new model
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            data.Data = new MonsterModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";

            DifficultyLevelPicker.SelectedItem = data.Data.DifficultyLevel.ToString();

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
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        async void Save_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "You need to enter a name!", "OK");
                return;
            }
            if(ViewModel.Data.DifficultyLevel == Models.Enum.DifficultyLevelEnum.Unknown)
            {
                await DisplayAlert("Alert", "You need to select a Difficulty!", "OK");
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