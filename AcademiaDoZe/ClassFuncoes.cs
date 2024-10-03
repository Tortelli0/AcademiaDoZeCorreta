using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe;
public class ClassFuncoes
{

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
}
