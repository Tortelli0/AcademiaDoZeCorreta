using AcademiaDoZe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AcademiaDoZe;
/// <summary>
/// Interação lógica para PageListaLogradouro.xam
/// </summary>
public partial class PageListaLogradouro : Page
{
	// Atributos para conexão e persistência com o banco de dados
	private string ConnectionString { get; set; }
	private string ProviderName { get; set; }

	// declaração ViewModel
	private LogradouroViewModel ViewModelLogradouro;

	public PageListaLogradouro()
	{
		InitializeComponent();

		try
		{
			// criação de objeto ViewModel
			ViewModelLogradouro = new LogradouroViewModel();
			// carrega os dados
			ViewModelLogradouro.GetAll(); //Load();
			// associa o objeto da ViewModel ao DataContext da janela
			// DataContext é uma propriedade que permite que elementos de interface gráfica sejam associados a objetos de dados
			DataContext = ViewModelLogradouro;
		}
		catch
		{
			MessageBox.Show("Erro ao carregar a lista de logradouros!");
		}
	}
}