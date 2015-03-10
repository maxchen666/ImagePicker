using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.IO;
using Foundation;
using ImagePicker.iOS;

[assembly:ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]

namespace ImagePicker.iOS
{
	public class CustomContentPageRenderer: PageRenderer
	{
		public CustomContentPageRenderer ()
		{

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);

			// var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.Camera };
			var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.PhotoLibrary };
			App.Instance.ShouldTakePicture += () => PresentViewController(imagePicker, true, null);

			imagePicker.FinishedPickingMedia += (sender, e) => {
				var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "tmp.png");
				var image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));
				InvokeOnMainThread(() => {
					image.AsPNG().Save(filepath, false);
					App.Instance.ShowImage(filepath);
				});
				DismissViewController(true, null);
			};

			imagePicker.Canceled += (sender, e) => DismissViewController(true, null);
		}	
	}
}

