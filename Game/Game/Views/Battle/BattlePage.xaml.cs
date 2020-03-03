using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{
		// This uses the Instance so it can be shared with other Battle Pages as needed
		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

		// HTML Formatting for message output box
		public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

		// Wait time before proceeding
		public int WaitTime = 1500;
		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage ()
		{
			InitializeComponent ();

			// Set up the UI to Defaults
			BindingContext = EngineViewModel;

			// Start the Battle Engine
			EngineViewModel.Engine.StartBattle(false);

			// Show the New Round Screen
			ShowModalNewRoundPage();

			// Ask the Game engine to select who goes first
			EngineViewModel.Engine.CurrentAttacker = null;

			// Game Starts with No Attacker or Defender selected

			// Add Players to Display
			DrawGameAttackerDefenderBoard();

			HideUIElements();

			StartBattleButton.IsVisible = true;
		}

		/// <summary>
		/// 
		/// Hide the differnt button states
		/// 
		/// Hide the message display box
		/// 
		/// </summary>
		public void HideUIElements()
		{

		}

		/// <summary>
		/// Dray the Player Boxes
		/// </summary>
		public void DrawPlayerBoxes()
		{ 
		
		}

		// <summary>
		/// Put the Player into a Display Box
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public StackLayout PlayerInfoDisplayBox(EntityInfoModel data)
		{
			return null;
		}

		/// <summary>
		/// Draw the UI for
		///
		/// Attacker vs Defender Mode
		/// 
		/// </summary>
		public void DrawGameAttackerDefenderBoard()
		{

		}

		/// <summary>
		/// Draws the Game Board Attacker and Defender
		/// </summary>
		public void DrawGameBoardAttackerDefenderSection()
		{

		}

		/// <summary>
		/// Draws the Game Board Attacker and Defender areas to be null
		/// </summary>
		public void DrawGameBoardClear()
		{

		}

		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void AttackButton_Clicked(object sender, EventArgs e)
		{
			DisplayAlert("SU", "Attack !!!", "OK");
		}

		/// <summary>
		/// Decide The Turn and who to Attack
		/// </summary>
		public void SetAttackerAndDefender()
		{ 
			
		}
		/// <summary>
		/// Game is over
		/// 
		/// Show Buttons
		/// 
		/// Clean up the Engine
		/// 
		/// Show the Score
		/// 
		/// Clear the Board
		/// 
		/// </summary>
		public void GameOver()
		{

		}
		#region MessageHandelers

		/// <summary>
		/// Builds up the output message
		/// </summary>
		/// <param name="message"></param>
		public void GameMessage()
		{

		}

		/// <summary>
		///  Clears the messages on the UX
		/// </summary>
		public void ClearMessages()
		{

		}

		#endregion MessageHandlers

		#region PageHandelers

		/// <summary>
		/// Battle Over, so Exit Button
		/// Need to show this for the user to click on.
		/// The Quit does a prompt, exit just exits
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void ExitButton_Clicked(object sender, EventArgs e)
		{

		}
		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void RoundOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RoundOverPage());
		}

		/// <summary>
		/// The Next Round Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void NextRoundButton_Clicked(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// The Start Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartButton_Clicked(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Show the Game Over Screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public async void ShowScoreButton_Clicked(object sender, EventArgs args)
		{

		}

		/// <summary>
		/// Show the Round Over page
		/// 
		/// Round Over is where characters get items
		/// 
		/// </summary>
		public async void ShowModalRoundOverPage()
		{

		}

		/// <summary>
		/// Show the Page for New Round
		/// 
		/// Upcomming Monsters
		/// 
		/// </summary>
		public async void ShowModalNewRoundPage()
		{

		}
		#endregion PageHandelers

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void NewRoundButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NewRoundPage());
		}
		

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void BattleOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ScorePage());
		}



		/// <summary>
		/// Quit the Battle
		/// 
		/// Quitting out
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void QuitButton_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");

			if (answer)
			{
				await Navigation.PopModalAsync();
			}
		}
	}
}