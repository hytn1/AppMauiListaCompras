using AppMauiListaCompras.Models;

namespace AppMauiListaCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text,
				Preco = Convert.ToDouble(txt_texto.Text),
				Quantidade = Convert.ToDouble(txt_quantidade)

			};
			await App.Db.Insert(p);
			await DisplayAlert("Sucesso", "Produto inserido", "OK");
			await Navigation.PushAsync(new MainPage());
		} catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}