using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRoundPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        MapModelLocation From;

        ImageButton Selected;

        /// <summary>
        /// Constructor
        /// </summary>
        public NewRoundPage()
        {
            InitializeComponent();

            DrawBattleBoard();
        }



        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BeginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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

                    // Image button for characters
                    if (player.PlayerType == PlayerTypeEnum.Character)
                    {
                        var displayed = new ImageButton { Source = player.ImageURI };

                        displayed.BindingContext = player;

                        displayed.Clicked += OnPlayerClicked;

                        BattleBoard.Children.Add(displayed, i, j);
                    }
                    // Image for monsters
                    else if (player.PlayerType == PlayerTypeEnum.Monster)
                    {
                        var displayed = new Image { Source = player.ImageURI };

                        BattleBoard.Children.Add(displayed, i, j);
                    }
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

            if (player_clicked.PlayerType != PlayerTypeEnum.Character)
            {
                return; // validate the player is a character
            }

            if (From == null)
            {
                From = EngineViewModel.Engine.MapModel.GetLocationForPlayer(player_clicked);

                Selected = imgButton;

                Selected.IsEnabled = false;
            }
            else
            {
                var to = EngineViewModel.Engine.MapModel.GetLocationForPlayer(player_clicked);

                var temp = From.Player;

                From.Player = to.Player;

                to.Player = temp;

                From = null;

                Selected.IsEnabled = true;

                // redraw game board
                DrawBattleBoard();
            }
        }
    }
}