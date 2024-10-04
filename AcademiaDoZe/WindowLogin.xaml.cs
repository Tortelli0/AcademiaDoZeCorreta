using System;
using System.Collections.Generic;
using System.Configuration.Provider;
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
using System.Windows.Shapes;

namespace AcademiaDoZe;
/// <summary>
/// Lógica interna para WindowLogin.xaml
/// </summary>
public partial class WindowLogin : Window
{
	private string ConnectionString { get; set; }
	private string ProviderName { get; set; }

	public WindowLogin(string connectionString, string providerName)
	{
		InitializeComponent();

		this.Loaded += Page_Loaded;

		txtCpf.Focus();

		this.KeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);

		this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(ClassFuncoes.Window_KeyDown);

		txtCpf.PreviewTextInput += ClassFuncoes.TxtCPF_PreviewTextInput;

		this.ConnectionString = connectionString;
		this.ProviderName = providerName;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		ClassFuncoes.AjustaResources(this);
	}

	private void txtCpf_GotFocus_1(object sender, RoutedEventArgs e)
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

	private void txtCpf_LostFocus_1(object sender, RoutedEventArgs e)
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

	private void txtSenha_GotFocus(object sender, RoutedEventArgs e)
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

	private void txtSenha_LostFocus(object sender, RoutedEventArgs e)
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
}
