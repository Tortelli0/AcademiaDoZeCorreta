using System.Configuration;
using System.Data;
using System.Windows;

namespace AcademiaDoZe;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	// aplicando polimorfismo
	// reescrita do método OnStartup
	protected override void OnStartup(StartupEventArgs e)
	{
		// mantem o que já acontecia no método original
		base.OnStartup(e);

		// Define a cultura padrão
		ClassFuncoes.AjustaIdiomaRegiao();
	}
}

