using Game.Models;
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
			if (currentCount == 0 || currentCount > EngineViewModel.Engine.MaxNumberPartyCharacters)
			{
				BeginBattleButton.IsEnabled = false;
			}

			//PartyCountLabel.Text = currentCount.ToString();
		}

		/// <summary>
		/// Jump to the Battle
		/// 
		/// Its Modal because don't want user to come back...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void BattleButton_Clicked(object sender, EventArgs e)
		{
			CreateEngineCharacterList();

			await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
			await Navigation.PopAsync();
		}

		/// <summary>
		/// Clear out the old list and make the new list
		/// </summary>
		public void CreateEngineCharacterList()
		{
			// Clear the currett list
			EngineViewModel.Engine.CharacterList.Clear();

			// Load the Characters into the Engine
			foreach (var data in EngineViewModel.PartyCharacterList)
			{
				EngineViewModel.Engine.CharacterList.Add(new EntityInfoModel(data));
			}
		}

		/// <summary>
		/// Cancel and close this page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void Cancel_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		/// <summary>
		/// The row selected from the list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public void OnCharacterToggled(object sender, ToggledEventArgs e)
		{
			var sw = sender as Xamarin.Forms.Switch;

			CharacterModel data = sw.BindingContext as CharacterModel;

			if(e.Value == true)
			{
				EngineViewModel.PartyCharacterList.Add(data);
			} else
			{
				EngineViewModel.PartyCharacterList.Remove(data);
			}

			UpdateNextButtonState();
		}
	}
}