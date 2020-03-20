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
    public partial class BattlePage : ContentPage
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
        public BattlePage()
        {
            InitializeComponent();

            // Set up the UI to Defaults
            BindingContext = EngineViewModel;

            // Start the Battle Engine
            EngineViewModel.Engine.StartBattle(false);

            // Show the New Round Screen
            ShowModalNewRoundPage();

            // Ask the Game engine to select who goes first
            EngineViewModel.Engine.CurrentAttacker = null;

            // Initialize battle board
            IntializeBattleBoard();
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
        /// Initialize the game board, create a grid layout with rows and colums
        /// </summary>
        void IntializeBattleBoard()
        {
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

            DrawBattleBoard();
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

            // create iamge buttons for each cell.
            for (int i = 0; i < num_row; i++)
            {
                for (int j = 0; j < num_col; j++)
                {
                    var player = map.MapGridLocation[i, j].Player;

                    var cellContent = new AbsoluteLayout();

                    var backgroundImage = new Image();

                    if (map.MapGridLocation[i, j].IsSelectable)
                    {
                        backgroundImage.Source = "battle_tile_green.png";

                        var imageButton = new ImageButton { Source = player.ImageURI };

                        imageButton.IsEnabled = true;

                        imageButton.Clicked += OnImageButtonClicked;

                        imageButton.BindingContext = map.MapGridLocation[i, j];

                        cellContent.Children.Add(backgroundImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

                        cellContent.Children.Add(imageButton, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
                    }
                    else 
                    {

                        backgroundImage.Source = "battle_tile.png";

                        var image = new Image { Source = player.ImageURI };

                        cellContent.Children.Add(backgroundImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

                        cellContent.Children.Add(image, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
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
            NextTurn();
            DrawBattleBoard();
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

        #region BattleFlow
        /// <summary>
        /// Start the next turn
        /// </summary>
        public void NextTurn()
        {
            // set attacker and defender
            EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.GetNextPlayerTurn();
            EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);

            // update attacker and defender on the page
            UpdateAttacker(EngineViewModel.Engine.CurrentAttacker);


            // monster turn or character turn?
            if (EngineViewModel.Engine.CurrentAttacker.PlayerType == PlayerTypeEnum.Character) // player turn
            {
                PlayerTurnBox.IsVisible = true;
                MonsterTurnBox.IsVisible = false;
                ActionSelectionBox.IsVisible = true;
                UpdateDefender(null);
            }
            else if (EngineViewModel.Engine.CurrentAttacker.PlayerType == PlayerTypeEnum.Monster) // mosnter turn
            {
                PlayerTurnBox.IsVisible = false;
                MonsterTurnBox.IsVisible = true;
                UpdateDefender(EngineViewModel.Engine.CurrentDefender);
            }
        }

        /// <summary>
        /// Take the turn
        /// </summary>
        public void TakeTurn()
        {
            // Hold the current state
            var RoundCondition = EngineViewModel.Engine.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

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
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {

            await Navigation.PushModalAsync(new RoundOverPage());
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


        #endregion BattleFlow

        #region GameOver

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new ScorePage());
        }

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

        #endregion GameOver

        #region ActionBox

        /// <summary>
        /// When attack button is clicked
        /// </summary>
        public void OnAttackButtonClicked(object sender, EventArgs e)
        {
            ActionSelectionBox.IsVisible = false;
            AttackActionBox.IsVisible = true;
            MoveActionBox.IsVisible = false;

            // enable image buttons for monsters
            EnableMonsterSelection();

            UpdateDefender(EngineViewModel.Engine.CurrentDefender);
        }

        /// <summary>
        /// When move button is clicked
        /// </summary>
        public void OnMoveButtonClicked(object sender, EventArgs e)
        {
            ActionSelectionBox.IsVisible = false;
            AttackActionBox.IsVisible = false;
            MoveActionBox.IsVisible = true;

            // enable image buttons for empty locations
            EnableEmptyLocationSelection();
        }

        /// <summary>
        /// When attack confirm button is clicked
        /// </summary>
        public void OnAttackConfirmClicked(object sender, EventArgs e)
        {
            AttackActionBox.IsVisible = false;

            DisableSelections();

            EngineViewModel.Engine.CurrentAction = ActionEnum.Attack;

            TakeTurn();

            NextTurn();

            DrawBattleBoard();
        }

        /// <summary>
        /// When action cancel button is clicked
        /// </summary>
        public void OnActionCancelClicked(object sender, EventArgs e)
        {
            AttackActionBox.IsVisible = false;
            MoveActionBox.IsVisible = false;
            ActionSelectionBox.IsVisible = true;
            DefenderImage.Source = "question_mark.png";
            DisableSelections();
            DrawBattleBoard();
        }

        /// <summary>
        /// When monster turn defense button is clicked
        /// </summary>
        public void OnConfirmDefenseClicked(object sender, EventArgs e)
        {
            TakeTurn();

            NextTurn();

            DrawBattleBoard();
        }

        #endregion ActionBox

        #region Selection

        /// <summary>
        /// Enable monster selection on the battle field
        /// </summary>
        public void EnableMonsterSelection()
        {
            foreach (var cell in EngineViewModel.Engine.MapModel.MapGridLocation)
            {
                if (cell.Player.PlayerType == PlayerTypeEnum.Monster)
                {
                    //var attacker = EngineViewModel.Engine.CurrentAttacker;

                    //var defender = cell.Player;

                    // todo : fix after movement is done
                    //if (EngineViewModel.Engine.MapModel.IsTargetInRange(attacker, defender)){
                    //	cell.IsClickable = true;
                    //}

                    cell.IsSelectable = true;
                }
            }
            DrawBattleBoard();
        }

        /// <summary>
        /// Enable empty location selection on the battle field
        /// </summary>
        public void EnableEmptyLocationSelection()
        {
            foreach (var cell in EngineViewModel.Engine.MapModel.MapGridLocation)
            {
                if (cell.Player.PlayerType == PlayerTypeEnum.Unknown)
                {
                    //var attacker = EngineViewModel.Engine.CurrentAttacker;

                    //var defender = cell.Player;

                    // todo : fix after movement is done
                    //if (EngineViewModel.Engine.MapModel.IsTargetInRange(attacker, defender)){
                    //	cell.IsClickable = true;
                    //}

                    cell.IsSelectable = true;
                }
            }
            DrawBattleBoard();
        }

        /// <summary>
        /// Disable selections on the battle field
        /// </summary>
        public void DisableSelections()
        {
            EngineViewModel.Engine.MapModel.ClearSelectable();
        }

        /// <summary>
        /// Event when image button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnImageButtonClicked(object sender, EventArgs e)
        {
            var imgButton = (ImageButton)sender;

            var location = (MapModelLocation)imgButton.BindingContext;

            var player_clicked = location.Player;

            // if a monster is clicked, assign it as defender
            if (player_clicked.PlayerType == PlayerTypeEnum.Monster)
            {
                EngineViewModel.Engine.CurrentDefender = player_clicked;

                UpdateDefender(player_clicked);
            }

            // if a empty location is clicked
            if (player_clicked.PlayerType == PlayerTypeEnum.Unknown)
            {
                // move player
                MoveActionBox.IsVisible = false;

                DisableSelections();

                EngineViewModel.Engine.CurrentAction = ActionEnum.Move;

                EngineViewModel.Engine.TargetLocation = location;

                TakeTurn();

                NextTurn();

                DrawBattleBoard();
            }
        }

        #endregion Selection

        /// <summary>
        /// Update attacker info on the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateAttacker(EntityInfoModel player)
        {
            if (player == null)
            {
                AttackerImage.Source = "question_mark.png";
                AttackerLabel.Text = "";
                return;
            }
            var attacker = EngineViewModel.Engine.CurrentAttacker;
            AttackerImage.Source = attacker.ImageURI;
            AttackerLabel.Text = attacker.GetCurrentHealthTotal.ToString() + " / " + attacker.GetMaxHealthTotal.ToString();
        }

        /// <summary>
        /// Update defender info on the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateDefender(EntityInfoModel player)
        {
            if (player == null)
            {
                DefenderImage.Source = "question_mark.png";
                DefenderLabel.Text = "";
                return;
            }
            var defender = EngineViewModel.Engine.CurrentDefender;
            DefenderImage.Source = defender.ImageURI;
            DefenderLabel.Text = defender.GetCurrentHealthTotal.ToString() + " / " + defender.GetMaxHealthTotal.ToString();
        }
    }
}