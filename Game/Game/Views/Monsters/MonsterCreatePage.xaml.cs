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
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        async void Save_Clicked(object sender, EventArgs e)
        {
            if(ViewModel.Data.Name == "")
            {
                await DisplayAlert("Alert", "You need to enter a name!", "OK");
                return;
            }
            if(ViewModel.Data.DifficultyLevel != Models.Enum.DifficultyLevelEnum.Easy &&
                ViewModel.Data.DifficultyLevel != Models.Enum.DifficultyLevelEnum.Medium &&
                ViewModel.Data.DifficultyLevel != Models.Enum.DifficultyLevelEnum.Hard)
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
        /// Catch the change to the stepper for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for MaxHealth
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MaxHealth_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            MaxHealthValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", e.NewValue);
        }

       

        /// <summary>
        /// Catch the change to the stepper for Drop Rate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DropRate_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //DropRateValue.Text = String.Format("{0}", e.NewValue);
        }
    }
}