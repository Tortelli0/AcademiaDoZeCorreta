using AcademiaDoZe.DataAcess;
using AcademiaDoZe.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademiaDoZe;

/// <summary>
/// Lógica interna para WindowMatricula.xaml
/// </summary>
public partial class WindowMatricula : Window
{
	//private string ConnectionString { get; set; }
	//private string ProviderName { get; set; }

	private readonly AlunoRepository _alunoRepository;

	public WindowMatricula()
	{
		InitializeComponent();

		this.Loaded += Page_Loaded;

		DataContext = new MatriculaCadastroViewModel();
		InitializeComponent();
		DataContext = new MatriculaCadastroViewModel();
		_alunoRepository = new AlunoRepository();

		//this.ConnectionString = connectionString;
		//this.ProviderName = providerName;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		ClassFuncoes.AjustaResources(this);
	}

	private void txtCpfAluno_GotFocus_1(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.LightCyan;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void txtCpfAluno_LostFocus_1(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.White;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void DateInicioMatricula_GotFocus(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.LightCyan;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void DateInicioMatricula_LostFocus(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.White;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void DateFimMatricula_GotFocus(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.LightCyan;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void DateFimMatricula_LostFocus(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.White;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void txtLaudoMedico_GotFocus(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.LightCyan;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void txtLaudoMedico_LostFocus(object sender, RoutedEventArgs e)
	{
		var cor = System.Windows.Media.Brushes.White;
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Background = cor;
		}
		else if (sender is PasswordBox)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			passwordBox.Background = cor;
		}
	}

	private void btn_salvar_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private void btn_salvar_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Enter)
		{
			((Button)sender).RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}
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
		if (e.Key == Key.Enter)
		{
			e.Handled = true;
			TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
			UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
			if (elementWithFocus != null)
			{
				elementWithFocus.MoveFocus(request);
			}
		}

		if (e.Key == Key.Escape)
		{
			this.Close();
		}
	}

	private void btnBuscarCpf_Click(object sender, RoutedEventArgs e)
	{
		string cpf = txtCpfAluno.Text.Trim();

		try
		{
			var aluno = _alunoRepository.BuscarAlunoPorCpf(cpf);
			if (aluno != null)
			{
				var viewModel = DataContext as MatriculaCadastroViewModel;
				if (viewModel != null)
				{
					viewModel.IdAluno = aluno.Id;
					viewModel.Matricula.IdAluno = aluno.Id;
				}

				MessageBox.Show("Aluno encontrado!");
			}
			else
			{
				MessageBox.Show("Aluno não encontrado! Verifique o CPF informado.");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Erro ao buscar aluno pelo CPF: {ex.Message}");
		}
	}

	private void btnBuscarCpfColaborador_Click(object sender, RoutedEventArgs e)
	{
		string cpf = txtCpfColaborador.Text.Trim();

		if (string.IsNullOrEmpty(cpf))
		{
			MessageBox.Show("Por favor, insira um CPF válido.");
			return;
		}

		var colaborador = new ColaboradorRepository().BuscarColaboradorPorCpf(cpf);

		if (colaborador != null)
		{
			//txtIdColaborador.Text = colaborador.Id.ToString();
			MessageBox.Show($"Colaborador encontrado: {colaborador.Nome}");
		}
		else
		{
			MessageBox.Show("Colaborador não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
	}
}