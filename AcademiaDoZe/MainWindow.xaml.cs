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
	public MainWindow()
	{
		InitializeComponent();
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
}