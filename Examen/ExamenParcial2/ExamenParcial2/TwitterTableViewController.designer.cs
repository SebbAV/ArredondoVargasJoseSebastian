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
	[Register ("TwitterTableViewController")]
	partial class TwitterTableViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem btnTweet_TouchUpInside { get; set; }

		[Action ("btnSearch_TouchUpInside:")]
		partial void btnSearch_TouchUpInside (Foundation.NSObject sender);

		[Action ("btnTweet:")]
		partial void btnTweet (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnTweet_TouchUpInside != null) {
				btnTweet_TouchUpInside.Dispose ();
				btnTweet_TouchUpInside = null;
			}
		}
	}
}
