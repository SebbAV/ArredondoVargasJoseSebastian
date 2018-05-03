using System;

using UIKit;
using Photos;

using Foundation;
using AVFoundation;

namespace PhotoPicker09
{
    public partial class ViewController : UIViewController, IUIImagePickerControllerDelegate
    {
        #region Class Variables
        UITapGestureRecognizer profileTapGesture;
        UITapGestureRecognizer coverTapGesture;
        bool editModeEnabled = false;

        #endregion

        #region Constructor
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        #endregion

        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeComponents();
//BtnEdit.Clicked += BtnEdit_Action;
            // Perform any additional setup after loading the view, typically from a nib. 
        }
        #endregion
        void BtnEdit_Action(object sender, EventArgs e){
            BtnEdit.Title = editModeEnabled ? "Done" : "Edit";
            profileTapGesture.Enabled = coverTapGesture.Enabled = editModeEnabled;
            lblEdit.Hidden = LblCover.Hidden = !editModeEnabled;
        }
        #region User Interactions

        void ShowOptions(UITapGestureRecognizer gesture)
        {

            var alert = UIAlertController.Create("Choose Your Option", "", UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("Open Camera", UIAlertActionStyle.Default, Action => OpenCamera()));
            alert.AddAction(UIAlertAction.Create("Open Photo Gallery", UIAlertActionStyle.Default, Action => OpenGallery()));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
            PresentViewController(alert, animated: true, completionHandler: null);

        }

        #endregion

        void OpenCamera()
        {
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                //TODO: Print a message 
                ShowMessage("Error", "Camera resource is not available", NavigationController);
                return;
            }
            CheckCameraAtuhorizationStatus(AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video));


        }
        void CheckCameraAtuhorizationStatus(AVAuthorizationStatus authorizationStatus){
            switch (authorizationStatus)
            {
                case AVAuthorizationStatus.NotDetermined:
                    AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
                    break;
                case AVAuthorizationStatus.Restricted:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Resource not available", "The resource is not available due it's restricted to use", NavigationController);
                    });
                    break;
                case AVAuthorizationStatus.Denied:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Resource not available", "The resource is not available due it's restricted to use", NavigationController);
                    });
                    break;
                case AVAuthorizationStatus.Authorized:
                    InvokeOnMainThread(() =>
                    {
                        var Camera = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.Camera,
                            Delegate = this
                        };
                        PresentViewController(Camera, true, null);
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        void OpenGallery()
        {
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {
                //TODO: Print a message 
                return;
            }
            CheckPhotoLibraryAuthorizationStatus(PHPhotoLibrary.AuthorizationStatus);


        }
        void CheckPhotoLibraryAuthorizationStatus(PHAuthorizationStatus authorizationStatus)
        {
            switch (authorizationStatus)
            {
                case PHAuthorizationStatus.NotDetermined:
                    //TODO: Ask for permission to open galerry
                    PHPhotoLibrary.RequestAuthorization(CheckPhotoLibraryAuthorizationStatus);
                    break;
                case PHAuthorizationStatus.Restricted:
                    //TODO: Display a message of restricted
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Resource not available", "The resource is not available due it's restricted to use", NavigationController);
                    });
                    break;
                case PHAuthorizationStatus.Denied:
                    //TODO: Show a message that user denied the authorization
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Resource not available", "The resource is not available due it was denied by the user.", NavigationController);
                    });
                    break;
                case PHAuthorizationStatus.Authorized:
                    //TODO: Open Gallery
                    InvokeOnMainThread(() =>
                    {
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                            Delegate = this
                        };
                        PresentViewController(imagePickerController, true, null);
                    });

                    break;
                default:
                    break;
            }
        }



        #region UIImage Picker Controller elegate
        [Export("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            var image = info[UIImagePickerController.OriginalImage] as UIImage;
            ImgProfile.Image = image;
            picker.DismissViewController(true,null);
        }
           [Export("imagePickerControllerDidCancel:")]
        public void Canceled(UIImagePickerController picker)
        {
            picker.DismissViewController(true,null);

        }

            
        #endregion
       // override Finis

        #region Internal Functionality
        void InitializeComponents()
        {
            //Hidding components for edit
            lblEdit.Hidden = LblCover.Hidden = true;

            profileTapGesture = new UITapGestureRecognizer(ShowOptions) {Enabled = true};
            ProfileView.AddGestureRecognizer(profileTapGesture);
            coverTapGesture = new UITapGestureRecognizer(ShowOptions) {Enabled = true};
            CoverView.AddGestureRecognizer(coverTapGesture);

        }
        void ShowMessage(string title,string messages, UIViewController fromViewController){
            var alert = UIAlertController.Create(title, messages, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok" ,UIAlertActionStyle.Default,null ));
            fromViewController.PresentViewController(alert,true,null);
        }
        
        #endregion
    }
}
