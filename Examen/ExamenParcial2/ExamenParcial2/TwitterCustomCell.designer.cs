// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ExamenParcial2
{
	[Register ("TwitterCustomCell")]
	partial class TwitterCustomCell
	{
		[Outlet]
		UIKit.UIImageView imgProfile { get; set; }

		[Outlet]
		UIKit.UITextView lblTweetInfo { get; set; }

		[Outlet]
		UIKit.UILabel lblUser { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgProfile != null) {
				imgProfile.Dispose ();
				imgProfile = null;
			}

			if (lblUser != null) {
				lblUser.Dispose ();
				lblUser = null;
			}

			if (lblTweetInfo != null) {
				lblTweetInfo.Dispose ();
				lblTweetInfo = null;
			}
		}
	}
}
