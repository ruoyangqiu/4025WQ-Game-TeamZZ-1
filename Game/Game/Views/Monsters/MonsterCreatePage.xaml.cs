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
        GenericViewModel<MonsterModel> ViewModel { get; set; }
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            data.Data = new MonsterModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";

            DifficultyLevelPicker.SelectedItem = data.Data.DifficultyLevel.ToString();
        }

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

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = String.Format("{0}", e.NewValue);
        }

        void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", e.NewValue);
        }

        void MaxHealth_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            MaxHealthValue.Text = String.Format("{0}", e.NewValue);
        }

        void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", e.NewValue);
        }

        void Experience_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ExperienceValue.Text = String.Format("{0}", e.NewValue);
        }

        void DropRate_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //DropRateValue.Text = String.Format("{0}", e.NewValue);
        }
    }
}