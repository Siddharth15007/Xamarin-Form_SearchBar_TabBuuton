using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SearchPage.Models;
using SearchPage.Views;

namespace SearchPage.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Properties
        //this propety usr for tab1 list
        bool _isTab1 = true;
        public bool IsTab1
        {
            get { return _isTab1; }
            set { SetProperty(ref _isTab1, value); }
        }

        // this property use for tab2 list
        bool _isTab2 = false;
        public bool IsTab2
        {
            get { return _isTab2; }
            set { SetProperty(ref _isTab2, value); }
        }

        // here are paste  tab 2 item property  here our list of tab 2 store
        public ObservableCollection<Item> Tab2Items { get; set; }


        public ObservableCollection<Item> Items { get; set; }


        string _searchKeyword;
        public string SearchKeyword
        {
            get { return _searchKeyword; }
            set { SetProperty(ref _searchKeyword, value); }
        }
        #endregion

        #region Commands
        public Command LoadItemsCommand { get; set; }


        public Command SearchCommand { get; set; }


        //this command call when we trigger tab 1 button 
        public Command Tab1Command { get; set; }

        // and on click on tab2 button this cmd call
        public Command Tab2Command { get; set; }
        #endregion

        #region Constructor
        public ItemsViewModel()
        {
            Title = "Search Page";

            Items = new ObservableCollection<Item>();
            Tab2Items = new ObservableCollection<Item>();

            #region Assign Commands
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            SearchCommand = new Command(ExecuteSearchCommand);

            // we need to call command of both tab btn:
            Tab1Command = new Command(() =>
            {
                IsTab1 = true;
                IsTab2 = false;
            });

            Tab2Command = new Command(() =>
            {
                IsTab1 = false;
                IsTab2 = true;
            });
            #endregion

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }
        #endregion

        public async void ExecuteSearchCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsBySearchKeyword(SearchKeyword, true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();

                // API for GetTab1 List
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                // Call API for GETTab 2 list:
                var tab2Items = await DataStore.GetTab2ItemsAsync(true);
                foreach (var item in tab2Items)
                {
                    Tab2Items.Add(item);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}