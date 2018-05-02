using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Practica2
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        #region Global
        int multi = 0;
        int rows = 1;
        #endregion

        #region Constructors
        protected ViewController(IntPtr handle) : base(handle) { }
        #endregion

        #region Life Cycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            tblView.Delegate = this;
            tblView.DataSource = this;
            // Perform any additional setup after loading the view, typically from a nib.
        }
        #endregion

        #region Alerts
        void LengthAlert()
        {
            var alert = UIAlertController.Create("Choose the length of the table", null, UIAlertControllerStyle.Alert);
            alert.AddTextField((obj) => { });
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (obj) => SetLength(alert)));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Default, null));

            PresentViewController(alert, true, null);
        }
        void MultiplicationAlert()
        {
            var alert = UIAlertController.Create("Choose a Number", null, UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create($"1", UIAlertActionStyle.Default, (obj) => SetMultiplication(1)));
            alert.AddAction(UIAlertAction.Create($"2", UIAlertActionStyle.Default, (obj) => SetMultiplication(2)));
            alert.AddAction(UIAlertAction.Create($"3", UIAlertActionStyle.Default, (obj) => SetMultiplication(3)));
            alert.AddAction(UIAlertAction.Create($"4", UIAlertActionStyle.Default, (obj) => SetMultiplication(4)));
            alert.AddAction(UIAlertAction.Create($"5", UIAlertActionStyle.Default, (obj) => SetMultiplication(5)));
            alert.AddAction(UIAlertAction.Create($"6", UIAlertActionStyle.Default, (obj) => SetMultiplication(6)));
            alert.AddAction(UIAlertAction.Create($"7", UIAlertActionStyle.Default, (obj) => SetMultiplication(7)));
            alert.AddAction(UIAlertAction.Create($"8", UIAlertActionStyle.Default, (obj) => SetMultiplication(8)));
            alert.AddAction(UIAlertAction.Create($"9", UIAlertActionStyle.Default, (obj) => SetMultiplication(9)));
            alert.AddAction(UIAlertAction.Create($"10", UIAlertActionStyle.Default, (obj) => SetMultiplication(10)));
            alert.AddAction(UIAlertAction.Create($"11", UIAlertActionStyle.Default, (obj) => SetMultiplication(11)));
            alert.AddAction(UIAlertAction.Create($"12", UIAlertActionStyle.Default, (obj) => SetMultiplication(12)));
            alert.AddAction(UIAlertAction.Create($"13", UIAlertActionStyle.Default, (obj) => SetMultiplication(13)));
            alert.AddAction(UIAlertAction.Create($"14", UIAlertActionStyle.Default, (obj) => SetMultiplication(14)));
            alert.AddAction(UIAlertAction.Create($"15", UIAlertActionStyle.Default, (obj) => SetMultiplication(15)));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Default, null));

            PresentViewController(alert, true, null);
        }
        #endregion

        #region Functionality

        void SetMultiplication(int Multi)
        {
            multi = Multi;
            LengthAlert();
        }
        void reinitializeVariables()
        {
            multi = 0;
            rows = 1;
        }
        void SetLength(UIAlertController ale)
        {
            int tempN;
            bool t = int.TryParse(ale.TextFields[0].Text, out tempN);
            if (t)
            {
                rows = tempN;
            }

            tblView.ReloadData();
        }
        #endregion

        #region User Interactions
        partial void BtnShow_Add(NSObject sender)
        {
            reinitializeVariables();
            MultiplicationAlert();
        }

        #endregion

        #region Table Delegate
        public nint RowsInSection(UITableView tableView, nint section)
        {
            if (rows == 0)
            {
                return 1;
            }
            return rows;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tblView.DequeueReusableCell("TableViewRow");
            int res = indexPath.Row * multi;
            cell.TextLabel.Text = $"{multi} x {indexPath.Row} = {res}";
            return cell;
        }
        #endregion


    }
}
