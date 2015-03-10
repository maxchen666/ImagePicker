using System;

using Xamarin.Forms;

namespace ImagePicker
{
	public class App : Application
	{
		public static App Instance;
		readonly Image image = new Image();
		public App ()
		{
			Instance = this;

			var button = new Button {
				Text = "Snap!",
				Command = new Command(o => ShouldTakePicture()),
			};

			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						button,
						image,
					},
				},
			};
		}
		public event Action ShouldTakePicture = () => {};
		public void ShowImage(string filepath)
		{
			image.Source = ImageSource.FromFile(filepath);
		}
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

