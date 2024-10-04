using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace AcademiaDoZe;
public class ClassFuncoes
{

	/// <summary>
	/// De forma recursiva, varre todos os componentes da tela informada, executando o método ApplyResources em cada um dos componentes localizados.
	/// O ApplyResources realiza a leitura do satellite assembly, ou seja, do arquivo de resource que foi ativo conforme o idioma escolhido pelo usuário,
	/// e com base no nome do campo ajusta todos os parâmetros contidos no resource para aquele campo, por exemplo Text, Content, Font, Size, etc.
	/// Deve possuir, em Properties, um arquivo de resources para cada idioma desejado, devendo ser alimentado na coluna nome o nome do campo e a propriedade que deseja ajustar.
	/// Por exemplo, em cadeias de caracteres labelLogin.Content, em outros textBoxSenha.PasswordChar, ou seja, todas as propriedades podem ser ajustadas conforme o idioma e região em uso.
	/// </summary>
	/// <param name="parent">Informar o container inicial, geralmente this para pegar todos os campos da tela, ou então, por exemplo, o nome de um panel ou usercontrol.</param>
	/// 
	public static void AjustaIdiomaRegiao()
	{
		// pt-BR, en-US, es-ES

		// ? indica que o valor pode ser nulo
		string? auxIdiomaRegiao = ConfigurationManager.AppSettings.Get("IdiomaRegiao");
		// no ternário estamos tratando para isso não acontecer
		string idiomaRegiao = (auxIdiomaRegiao is not null) ? auxIdiomaRegiao : "";
		// Definir a cultura e ajusta o idioma/região
		// o operador ! (null-forgiving) afirma que o valor já foi tratado e não será nulo aqui
		CultureInfo culture = new(idiomaRegiao!);
		Thread.CurrentThread.CurrentUICulture = culture;
		Thread.CurrentThread.CurrentCulture = culture;
	}
	public static void AjustaResources(DependencyObject parent)
	{
		if (parent == null) return;
		// Percorre cada filho direto do objeto pai
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
		{
			// Obtém o filho no índice atual
			var child = VisualTreeHelper.GetChild(parent, i);
			if (child is FrameworkElement element)
			{
				// Aplicar recursos ao componente
				ComponentResourceManager resources = new(typeof(Properties.Idioma));
				resources.ApplyResources(element, element.Name);
			}
			// Chama recursivamente para percorrer os filhos do filho atual
			AjustaResources(child);
		}
	}

	public static void Window_KeyDown(object sender, KeyEventArgs e)
	{
		// Se a tecla ENTER for pressionada
		if (e.Key == Key.Enter)
		{
			// Move o foco para o próximo controle, como o TAB faria
			var focusedElement = Keyboard.FocusedElement as UIElement;
			// Move o foco para o próximo controle na ordem de tabulação
			focusedElement?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			e.Handled = true; // Previne comportamento padrão do ENTER (como som)
		}
		// Se a tecla ESC for pressionada
		else if (e.Key == Key.Escape)
		{
			// verifica se é window e fecha
			if (sender is Window window)
			{
				window.Close();
			}
			// carrega uma página inicial
			else
			{
				if (Application.Current.MainWindow is MainWindow mainWindow)
				{
					// precisa passar o método para public
					mainWindow.Home_btn_Click(sender, e);
				}
			}
		}
	}

	public static void TxtCPF_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		if (sender is TextBox textBoxCpf)
		{
			e.Handled = !Regex.IsMatch(e.Text, @"^\d+$"); // Ignora entrada se não for um dígito
			if (!e.Handled)
			{
				// Recupera o texto atual do TextBox
				var text = textBoxCpf.Text;
				// Remove qualquer caracter não numérico
				text = Regex.Replace(text, @"[^\d]", "");
				// Adiciona o novo caractere
				text += e.Text;
				// Aplica a máscara de CPF (###.###.###-##)
				if (text.Length <= 11)
				{
					if (text.Length > 9)
					{
						textBoxCpf.Text = $"{text.Substring(0, 3)}.{text.Substring(3, 3)}.{text.Substring(6, 3)}-{text.Substring(9)}";
					}
					else if (text.Length > 6)
					{
						textBoxCpf.Text = $"{text.Substring(0, 3)}.{text.Substring(3, 3)}.{text.Substring(6)}";
					}
					else if (text.Length > 3)
					{
						textBoxCpf.Text = $"{text.Substring(0, 3)}.{text.Substring(3)}";
					}
					else
					{
						textBoxCpf.Text = text;
					}
					textBoxCpf.CaretIndex = textBoxCpf.Text.Length; // Move o cursor para o fim
				}
				e.Handled = true; // Bloqueia a entrada direta no TextBox
			}
		}
	}

	public static void ValidaConexaoDB()
	{
		DbProviderFactory factory;
		string provider = ConfigurationManager.ConnectionStrings["BD"].ProviderName;
		string connectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
		try
		{
			factory = DbProviderFactories.GetFactory(provider);
			using var conexao = factory.CreateConnection();
			conexao!.ConnectionString = connectionString;
			using var comando = factory.CreateCommand();
			comando!.Connection = conexao;
			conexao.Open();
		}
		catch (DbException ex)
		{
			MessageBox.Show($"{ex.Source}\n\n{ex.Message}\n\n{ex.ErrorCode}\n\n{ex.SqlState}\n\n{ex.StackTrace}");
			var auxConfig = new WindowConfig(provider, connectionString);
			auxConfig.ShowDialog();
			ValidaConexaoDB();
		}
		catch (Exception ex)
		{
			MessageBox.Show($"{ex.Source}\n\n{ex.Message}\n\n{ex.StackTrace}");
			var auxConfig = new WindowConfig(provider, connectionString);
			auxConfig.ShowDialog();
			ValidaConexaoDB();
		}
	}
}
