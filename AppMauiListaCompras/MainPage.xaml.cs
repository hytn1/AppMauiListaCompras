﻿
using System.Collections.ObjectModel;
using AppMauiListaCompras;
using AppMauiListaCompras.Models;

namespace AppMauiListaCompras
{
    public partial class MainPage : ContentPage
    {

    ObservableCollection<Produto>lista_produtos =
     new ObservableCollection<Produto>();

        public MainPage()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => (i.Preco* i.Quantidade));
            string msg = $"o total é {soma:C}";
            DisplayAlert($"Somatoria", msg, "Fechar");
        }
        protected async void OnAppearing()
        {
            if(lista_produtos.Count == 0)
            {
              
                    List<Produto> tmp = await App.Db.GetAll();
                    foreach (Produto p in tmp)
                    {
                        lista_produtos.Add(p);
                    }
               
            }
        }

        private void ToolbarItem_Clicked_Add(object sender, EventArgs e)
        {
            
        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;
            lista_produtos.Clear();
            Task.Run(async () =>
            {
                List<Produto> tmp = await App.Db.GetAll();
                foreach (Produto p in tmp)
                {
                    lista_produtos.Add(p);
                }
            });
        }

        private  void ref_carregando_Refreshing(object sender, EventArgs e)
        {
            lista_produtos.Clear();
            Task.Run(async() =>
            {
                List<Produto> tmp = await App.Db.GetAll();
                foreach(Produto p in tmp)
                {
                    lista_produtos.Add(p);
                }
            });
            ref_carregando.IsRefreshing = false;
        }

        private  void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto? p = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.Editar
                {
                BindingContext = p
                });
        }

        private async void MenuItem_Clicked_Remov(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = (MenuItem)sender;
                Produto p = selecionado.BindingContext as Produto;

                bool confirm = await DisplayAlert(
                    "tem certeza?", "Remover Produto?",
                    "sim", "cancelar");

                if (confirm)
                {
                    await App.Db.Delete(p.Id);
                    await DisplayAlert("sucesso!",
                         "Produto Removido", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ops", ex.Message, "OK");
            }
            
        }
    }

}
