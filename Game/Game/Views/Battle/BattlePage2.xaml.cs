using Game.Models;
using Game.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage2: ContentPage
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
		public BattlePage2 ()
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
			NextRoundButton.IsVisible = false;
			StartBattleButton.IsVisible = false;
			AttackButton.IsVisible = false;
			MessageDisplayBox.IsVisible = false;
			BattlePlayerInfomationBox.IsVisible = false;
		}

		/// <summary>
		/// Draw the UI for
		///
		/// Attacker vs Defender Mode
		/// 
		/// </summary>
		public void DrawGameAttackerDefenderBoard()
		{
			// Clear the current UI
			DrawGameBoardClear();

			// Show the Attacker and Defender
			DrawGameBoardAttackerDefenderSection();
		}

		/// <summary>
		/// Draws the Game Board Attacker and Defender
		/// </summary>
		public void DrawGameBoardAttackerDefenderSection()
		{
			BattlePlayerBoxVersus.Text = "Click to Begin";

			if (EngineViewModel.Engine.CurrentAttacker == null)
			{
				return;
			}

			if (EngineViewModel.Engine.CurrentDefender == null)
			{
				return;
			}

			AttackerImage.Source = EngineViewModel.Engine.CurrentAttacker.ImageURI;
			AttackerName.Text = EngineViewModel.Engine.CurrentAttacker.Name;
			AttackerHealth.Text = EngineViewModel.Engine.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentAttacker.GetMaxHealthTotal.ToString();

			DefenderImage.Source = EngineViewModel.Engine.CurrentDefender.ImageURI;
			DefenderName.Text = EngineViewModel.Engine.CurrentDefender.Name;
			DefenderHealth.Text = EngineViewModel.Engine.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentDefender.GetMaxHealthTotal.ToString();

			if (EngineViewModel.Engine.CurrentDefender.Alive == false)
			{
				DefenderImage.BackgroundColor = Color.Red;
			}

			BattlePlayerBoxVersus.Text = "vs";
		}

		/// <summary>
		/// Draws the Game Board Attacker and Defender areas to be null
		/// </summary>
		public void DrawGameBoardClear()
		{
			AttackerImage.Source = string.Empty;
			AttackerName.Text = string.Empty;
			AttackerHealth.Text = string.Empty;

			DefenderImage.Source = string.Empty;
			DefenderName.Text = string.Empty;
			DefenderHealth.Text = string.Empty;
			DefenderImage.BackgroundColor = Color.Transparent;

			BattlePlayerBoxVersus.Text = string.Empty;
		}

		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void AttackButton_Clicked(object sender, EventArgs e)
		{
			NextAttackExample();
		}

		/// <summary>
		/// Next Attack Example
		/// 
		/// This code example follows the rule of
		/// 
		/// Auto Select Attacker
		/// Auto Select Defender
		/// 
		/// Do the Attack and show the result
		/// 
		/// So the pattern is Click Next, Next, Next until game is over
		/// 
		/// </summary>
		private void NextAttackExample()
		{
			// Get the turn, set the current player and attacker to match
			SetAttackerAndDefender();

			// Hold the current state
			var RoundCondition = EngineViewModel.Engine.RoundNextTurn();

			// Output the Message of what happened.
			GameMessage();

			// Show the outcome on the Board
			DrawGameAttackerDefenderBoard();

			if (RoundCondition == RoundEnum.NewRound)
			{
				// Pause
				Task.Delay(WaitTime);

				Debug.WriteLine("New Round");

				// Show the Round Over, after that is cleared, it will show the New Round Dialog
				ShowModalRoundOverPage();
			}

			// Check for Game Over
			if (RoundCondition == RoundEnum.GameOver)
			{
				// Pause
				Task.Delay(WaitTime);

				Debug.WriteLine("Game Over");

				GameOver();
				return;
			}
		}

		/// <summary>
		/// Decide The Turn and who to Attack
		/// </summary>
		public void SetAttackerAndDefender()
		{
			EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.GetNextPlayerTurn();

			switch (EngineViewModel.Engine.CurrentAttacker.PlayerType)
			{
				case PlayerTypeEnum.Character:
					// User would select who to attack

					// for now just auto selecting
					EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);
					break;

				case PlayerTypeEnum.Monster:
				default:

					// Monsters turn, so auto pick a Character to Attack
					EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);
					break;
			}
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
			// Wrap up
			EngineViewModel.Engine.EndBattle();

			// Save the Score to the Score View Model, by sending a message to it.
			var Score = EngineViewModel.Engine.BattleScore;
			MessagingCenter.Send(this, "AddData", Score);

			// Hide the Game Board
			GameUIDisplay.IsVisible = false;

			// Show the Game Over Display
			GameOverDisplay.IsVisible = true;
		}
		#region MessageHandelers

		/// <summary>
		/// Builds up the output message
		/// </summary>
		/// <param name="message"></param>
		public void GameMessage()
		{
			// Output The Message that happened.
			BattleMessages.Text = string.Format("{0} \n {1}", EngineViewModel.Engine.BattleMessageModel.TurnMessage, BattleMessages.Text);

			Debug.WriteLine(BattleMessages.Text);

			if (!string.IsNullOrEmpty(EngineViewModel.Engine.BattleMessageModel.LevelUpMessage))
			{
				BattleMessages.Text = string.Format("{0} \n {1}", EngineViewModel.Engine.BattleMessageModel.LevelUpMessage, BattleMessages.Text);
			}

			//htmlSource.Html = EngineViewModel.Engine.BattleMessageModel.GetHTMLFormattedTurnMessage();
			//HtmlBox.Source = HtmlBox.Source = htmlSource;
		}

		/// <summary>
		///  Clears the messages on the UX
		/// </summary>
		public void ClearMessages()
		{
			BattleMessages.Text = "";
			htmlSource.Html = EngineViewModel.Engine.BattleMessageModel.GetHTMLBlankMessage();
			HtmlBox.Source = htmlSource;
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
			await Navigation.PopModalAsync();
		}


		/// <summary>
		/// The Next Round Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void NextRoundButton_Clicked(object sender, EventArgs e)
		{
			ShowModalNewRoundPage();
		}

		/// <summary>
		/// The Start Button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void StartButton_Clicked(object sender, EventArgs e)
		{
			HideUIElements();

			// Set for a trun to begin
			AttackButton.IsVisible = true;
			MessageDisplayBox.IsVisible = true;
			BattlePlayerInfomationBox.IsVisible = true;
		}

		/// <summary>
		/// Show the Game Over Screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public async void ShowScoreButton_Clicked(object sender, EventArgs args)
		{
			Debug.WriteLine("Showing Score Page : " + EngineViewModel.Engine.BattleScore.ScoreTotal.ToString());

			await Navigation.PushModalAsync(new ScorePage());
		}

		/// <summary>
		/// Show the Round Over page
		/// 
		/// Round Over is where characters get items
		/// 
		/// </summary>
		public async void ShowModalRoundOverPage()
		{
			HideUIElements();

			// Show the Round Over page
			// Then show the Next Round Button
			NextRoundButton.IsVisible = true;

			await Navigation.PushModalAsync(new RoundOverPage());
		}

		/// <summary>
		/// Show the Page for New Round
		/// 
		/// Upcomming Monsters
		/// 
		/// </summary>
		public async void ShowModalNewRoundPage()
		{
			await Navigation.PushModalAsync(new NewRoundPage());

			DrawBattleBoard();

			HideUIElements();

			ClearMessages();

			// Show the Attack Button Set
			BattlePlayerInfomationBox.IsVisible = true;
			MessageDisplayBox.IsVisible = true;
			AttackButton.IsVisible = true;
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

		/// <summary>
		/// Draw the battle board
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DrawBattleBoard()
		{

			BattleBoard.Children.Clear();

			var map = EngineViewModel.Engine.MapModel;

			int num_row = map.MapXAxiesCount;

			int num_col = map.MapXAxiesCount;

			// Create rows and columns
			for (int i = 0; i < num_row; i++)
			{
				BattleBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100) });
			}

			for (int i = 0; i < num_col; i++)
			{
				BattleBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
			}

			// Fill the grid with players
			for (int i = 0; i < num_row; i++)
			{
				for (int j = 0; j < num_col; j++)
				{
					var player = map.MapGridLocation[i, j].Player;

					var cellContent = new AbsoluteLayout();

					var backgroundImage = new Image { Source = "battle_tile.png" };

					cellContent.Children.Add(backgroundImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

					// Image button for characters
					if (player.PlayerType == PlayerTypeEnum.Character)
					{
						var characterButtonImage = new ImageButton { Source = player.ImageURI };

						characterButtonImage.BindingContext = player;

						characterButtonImage.Clicked += OnPlayerClicked;

						cellContent.Children.Add(characterButtonImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
					}
					// Image for monsters
					else if (player.PlayerType == PlayerTypeEnum.Monster)
					{
						var monsterImage = new Image { Source = player.ImageURI };

						cellContent.Children.Add(monsterImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
					}

					BattleBoard.Children.Add(cellContent, i, j);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnPlayerClicked(object sender, EventArgs e)
		{
			var imgButton = (ImageButton)sender;

			var player_clicked = (EntityInfoModel)imgButton.BindingContext;
			
			// todo: define what happens

		}

		protected override void OnAppearing()
		{
			DrawBattleBoard();
		}
	}
}