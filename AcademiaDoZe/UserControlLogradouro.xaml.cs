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

namespace AcademiaDoZe
{
    /// <summary>
    /// Interação lógica para UserControlLogradouro.xam
    /// </summary>
    public partial class UserControlLogradouro : UserControl
    {
        public UserControlLogradouro()
        {
            InitializeComponent();
        }

		private void txtCep_GotFocus(object sender, RoutedEventArgs e)
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

		private void txtCep_LostFocus(object sender, RoutedEventArgs e)
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

		private void txtLogradouro_GotFocus(object sender, RoutedEventArgs e)
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

		private void txtLogradouro_LostFocus(object sender, RoutedEventArgs e)
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

		private void txtBairro_GotFocus(object sender, RoutedEventArgs e)
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

		private void txtBairro_LostFocus(object sender, RoutedEventArgs e)
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

		private void txtCidade_GotFocus(object sender, RoutedEventArgs e)
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

		private void txtCidade_LostFocus(object sender, RoutedEventArgs e)
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

		private void txtUf_GotFocus(object sender, RoutedEventArgs e)
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

		private void txtUf_LostFocus(object sender, RoutedEventArgs e)
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

		private void txtPais_GotFocus(object sender, RoutedEventArgs e)
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

		private void txtPais_LostFocus(object sender, RoutedEventArgs e)
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
	}
}
