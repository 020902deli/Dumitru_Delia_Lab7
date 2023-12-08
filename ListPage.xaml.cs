namespace Dumitru_Delia_Lab7;
using Dumitru_Delia_Lab7.Models;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem != null)
        {
            var selectedItem = (Product)listView.SelectedItem; // presupunând că produsele sunt de tipul Product
            var shopList = (ShopList)BindingContext;

            // Implementarea metodei pentru ștergerea unui produs din lista curentă de cumpărături
            await App.Database.DeleteProductAsync(selectedItem); // poți adapta această metodă în funcție de logica aplicației tale
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID); // reîmprospătează sursa de date
        }
    }
}