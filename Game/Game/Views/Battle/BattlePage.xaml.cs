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

					// Image for characters
					if (player.PlayerType == PlayerTypeEnum.Character)
					{
						var characterImage = new Image { Source = player.ImageURI };

						cellContent.Children.Add(characterImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
					}
					// Image button for monsters
					else if (player.PlayerType == PlayerTypeEnum.Monster)
					{
						var monsterButtonImage = new ImageButton { Source = player.ImageURI };

						if (map.MapGridLocation[i, j].IsSelectable)
						{
							monsterButtonImage.IsEnabled = true;
						}
						else
						{
							monsterButtonImage.IsEnabled = false;
						}

						monsterButtonImage.BindingContext = player;

						//monsterButtonImage.Clicked += OnMonsterClicked;

						cellContent.Children.Add(monsterButtonImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
					}

					BattleBoard.Children.Add(cellContent, i, j);
				}
			}
		}

		/// <summary>
		/// When first appear, redraw game board and go to next turn
		/// </summary>
		protected override void OnAppearing()
		{
			DrawBattleBoard();
			NextTurn();
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

			htmlSource.Html = EngineViewModel.Engine.BattleMessageModel.GetHTMLFormattedTurnMessage();
			HtmlBox.Source = HtmlBox.Source = htmlSource;
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

		#region NextTurn
		/// <summary>
		/// Start the next turn
		/// </summary>
		public void NextTurn()
		{
			// set attacker
			EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.GetNextPlayerTurn();

			// update attacker and defender images on the page
			AttackerImage.Source = EngineViewModel.Engine.CurrentAttacker.ImageURI;

			DefenderImage.Source = "question_mark.png";

			// monster turn or character turn?
			if (EngineViewModel.Engine.CurrentAttacker.PlayerType == PlayerTypeEnum.Character) // player turn
			{
				PlayerTurnBox.IsVisible = true;
				MonsterTurnBox.IsVisible = false;

				// Player turn, so wait for player to select a target
			}
			else if (EngineViewModel.Engine.CurrentAttacker.PlayerType == PlayerTypeEnum.Monster) // mosnter turn
			{
				PlayerTurnBox.IsVisible = false;
				MonsterTurnBox.IsVisible = true;

				// Monsters turn, so auto pick a Character to Attack
				EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);
			}
		}


		#endregion NextTurn
	}
}