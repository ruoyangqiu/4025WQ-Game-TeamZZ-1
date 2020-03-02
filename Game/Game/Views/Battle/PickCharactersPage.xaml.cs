using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// Selecting Characters for the Game
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickCharactersPage : ContentPage
	{
		// This uses the Instance so it can be shared with other Battle Pages as needed
		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

		// Empty Constructor for UTs
		//public PickCharactersPage(bool UnitTest) { }

		/// <summary>
		/// Constructor
		/// </summary>
		public PickCharactersPage()
		{
			InitializeComponent ();

			BindingContext = EngineViewModel;

			// Clear the Database List and the Party List to start
			EngineViewModel.PartyCharacterList.Clear();

			UpdateNextButtonState();
		}

		/// <summary>
		/// Jump to the Battle
		/// 
		/// Its Modal because don't want user to come back...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void BattleButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
			await Navigation.PopAsync();
		}

		/// <summary>
		/// Next Button is based on the count
		/// 
		/// If no selected characters, disable
		/// 
		/// Show the Count of the party
		/// 
		/// </summary>
		public void UpdateNextButtonState()
		{
			// If no characters disable Next button
			BeginBattleButton.IsEnabled = true;

			var currentCount = EngineViewModel.PartyCharacterList.Count();
			if (currentCount == 0)
			{
				BeginBattleButton.IsEnabled = false;
			}

			PartyCountLabel.Text = currentCount.ToString();
		}
	}
}