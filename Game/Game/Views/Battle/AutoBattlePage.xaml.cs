using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage()
		{
			InitializeComponent();
		}

		public async void AutobattleButton_Clicked(object sender, EventArgs e)
		{
			// Call into Auto Battle from here to do the Battle...

			//var Engine = new Game.Engine.AutoBattleEngine();

			//string BattleMessage = "";

			//var result = await Engine.RunAutoBattle();

			//var Score = Engine.GetScoreObject();

			string BattleMessage = string.Format("Number of Rounds： 7 \n Monsters Killed： 19\n EXP Gained：9500\n Items Dropped:\n Blue Horn X 1, Golden Hair Pin X 3");

			BattleMessageValue.Text = BattleMessage;
		}
	}
}