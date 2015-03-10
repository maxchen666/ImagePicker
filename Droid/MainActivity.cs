//using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;
using Android.Provider;
using Android.Net;
using Android.Database;

namespace ImagePicker.Droid
{
	[Activity (Label = "ImagePicker.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		static readonly File file = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "tmp.jpg");
		public static readonly int PickImageId = 1000;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());

			App.Instance.ShouldTakePicture += () => {
				//var intent = new Intent(MediaStore.ActionImageCapture);
				//intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(file));
				//StartActivityForResult(intent, 0);m
				Intent = new Intent();
				Intent.SetType("image/*");
				Intent.SetAction (Intent.ActionGetContent);
				StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
			};
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			//var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			//mediaScanIntent.SetData(Uri.FromFile(file));
			//SendBroadcast(mediaScanIntent);
			//App.Instance.ShowImage(file.Path);
			if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
			{
				Uri uri = data.Data;

				string path = GetPathToImage(uri);
				App.Instance.ShowImage(path);
			}

		}

		private string GetPathToImage(Uri uri)
		{
			string path = null;
			// The projection contains the columns we want to return in our query.
			string[] projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
			using (ICursor cursor = ManagedQuery(uri, projection, null, null, null))
			{
				if (cursor != null)
				{
					int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
					cursor.MoveToFirst();
					path = cursor.GetString(columnIndex);
				}
			}
			return path;
		}
	}
}

