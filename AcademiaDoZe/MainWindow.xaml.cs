using AcademiaDoZe.Model;
using System.Configuration;
using System.Globalization;
using System.Text;
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
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	// Atributos para conexão e persistência com o banco de dados
	private string ConnectionString { get; set; }
	private string ProviderName { get; set; }

	public MainWindow()
	{
		InitializeComponent();


		WindowStartupLocation = WindowStartupLocation.CenterScreen;

		// valida a conexão com o banco de dados
		ClassFuncoes.ValidaConexaoDB();

		// busca os dados de conexão com o banco de dados, do arquivo de configuração
		// e deixa disponível para toda a aplicação através de propriedades
		ProviderName = ConfigurationManager.ConnectionStrings["BD"].ProviderName;
		ConnectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
	}

	private void ChangeLanguage(string cultureCode)
	{
		// en-US, es-ES, pt-BR
		// Definir a cultura
		CultureInfo culture = new CultureInfo(cultureCode);
		Thread.CurrentThread.CurrentCulture = culture;
		Thread.CurrentThread.CurrentUICulture = culture;
		// Recargar a interface do usuário para refletir as mudanças
		var oldWindow = this;
		var newWindow = new MainWindow();
		Application.Current.MainWindow = newWindow;
		newWindow.Show();
		oldWindow.Close();
	}

	private void Logradouro_btn_Click(object sender, RoutedEventArgs e)
	{
		//WindowLogradouro windowLogradouro = new WindowLogradouro(ConnectionString, ProviderName);

		//windowLogradouro.Show();

		//Frame.Navigate(new PageListaLogradouro(ConnectionString, ProviderName));

		if (framePrincipal.Content is not PageListaLogradouro)
		{
			framePrincipal.Content = new PageListaLogradouro();
		}
	}

	private void Aluno_btn_Click(object sender, RoutedEventArgs e)
	{
		//WindowAluno windowAluno = new WindowAluno();

		//windowAluno.Show();

		if (framePrincipal.Content is not PageListaAluno)
		{
			framePrincipal.Content = new PageListaAluno();
		}
	}

	private void Colaborador_btn_Click(object sender, RoutedEventArgs e)
	{
		//WindowColaborador windowColaborador = new WindowColaborador();

		//windowColaborador.Show();

		if (framePrincipal.Content is not PageListaColaborador)
		{
			framePrincipal.Content = new PageListaColaborador();
		}
	}

	private void Senha_btn_Click(object sender, RoutedEventArgs e)
	{
		WindowSenha windowSenha = new WindowSenha(ConnectionString, ProviderName);

		windowSenha.Show();
	}

	private void Avaliacao_btn_Click(object sender, RoutedEventArgs e)
	{
		WindowAvaliacao windowAvaliacao = new WindowAvaliacao(ConnectionString, ProviderName);

		windowAvaliacao.Show();
	}

	private void Frequencia_btn_Click(object sender, RoutedEventArgs e)
	{
		WindowFrequencia windwFrequencia = new WindowFrequencia(ConnectionString, ProviderName);

		windwFrequencia.Show();
	}

	private void Matricula_btn_Click(object sender, RoutedEventArgs e)
	{
		WindowMatricula windowMatricula = new WindowMatricula(ConnectionString, ProviderName);

		windowMatricula.Show();
	}

	private void Login_btn_Click(object sender, RoutedEventArgs e)
	{
		WindowLogin windowLogin = new WindowLogin(ConnectionString, ProviderName);

		windowLogin.Show();
	}

	private void ButtonConfig_Click(object sender, RoutedEventArgs e)
	{
		WindowConfig windowConfig = new WindowConfig(ConnectionString, ProviderName);

		windowConfig.Show();
	}

	public void Home_btn_Click(object sender, RoutedEventArgs e)
	{
		MainWindow mainWindow = new MainWindow();

		mainWindow.Show();
	}

	private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
	{
		// Mensagem de confirmação
		MessageBoxResult result = MessageBox.Show(
			Properties.Idioma.MsgConfirmarFechamento,
			Properties.Idioma.TituloConfirmarFechamento,
			MessageBoxButton.YesNo,
			MessageBoxImage.Question);

		if (result == MessageBoxResult.No)
		{
			e.Cancel = true;
		}
	}

	private void Window_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Escape)
		{
			Close();
		}
	}
}