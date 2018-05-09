using Foundation;
using System;
using UIKit;
using System.Linq;
using System.Collections.Generic;
using LinqToTwitter;

namespace ExamenParcial2
{
    public partial class TwitterTableViewController : UITableViewController, IUITableViewDelegate, IUITableViewDataSource, IUISearchResultsUpdating
    {
        UISearchController search;
        TwitterContext ctx;
         string globalfilter;
        string header;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            globalfilter = "Twitter";
            generatePermissions();
            header = $"Tweets of {globalfilter}";
            search = new UISearchController(searchResultsController: null)
            {
                DimsBackgroundDuringPresentation = false
            };
            //Starts a blank list so the app doesn't crash until the return of the method.
            LstTweets = new List<Tweet>();
            //Executes the method to run the task.
        

            //The refresh control load new tweets when executed.
            refreshControl = new UIRefreshControl();
            refreshControl.ValueChanged += async delegate {  await InitTweetsAsync(globalfilter); };
            TableView.RefreshControl = refreshControl;
         //   TableView.TableHeaderView = search.SearchBar;
            search.SearchResultsUpdater = this;
            search.SearchBar.SearchButtonClicked += async delegate {
                string globalfilter  = search.SearchBar.Text;
                header = globalfilter;
               await  InitTweetsAsync(globalfilter);

            };
            // ensures the segue works in the context of the underling ViewController, thanks @artemkalinovsky
            DefinesPresentationContext = true;

            //   UISearchControllerDelegate controllerDelegate;
            NavigationItem.SearchController = search;
        }
        async void  generatePermissions(){
            var auth = new ApplicationOnlyAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = TwitterAPICredentials.ConsumerApiKey,
                    ConsumerSecret = TwitterAPICredentials.ConsumerApiSecret
                },
            };
            await auth.AuthorizeAsync();
             ctx = new TwitterContext(auth);
            GetTweets();
        }
        public async void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
        //    var find = searchController.SearchBar.Text;
       //    if (!String.IsNullOrEmpty(find))
        //    {
          //      await InitTweetsAsync(find);
        //    }
          //  else
         //   {

         //   }
        }

    }
}
