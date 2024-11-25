using System.ComponentModel;

namespace AcademiaDoZe.Model;

public class Matricula : INotifyPropertyChanged
{
	public int Id { get; set; }
	public int IdAluno { get; set; }
	public int IdColaborador { get; set; }

	private DateTime dataInicio = DateTime.Today;

	public DateTime DataInicio
	{
		get => dataInicio;
		set
		{
			if (dataInicio != value)
			{
				dataInicio = value;
				CalcularDataFim();
				OnPropertyChanged(nameof(DataInicio));
			}
		}
	}

	private int plano;

	public int Plano
	{
		get => plano;
		set
		{
			if (plano != value)
			{
				CalcularDataFim();
				OnPropertyChanged(nameof(Plano));
			}
		}
	}

	private DateTime dataFim;

	public DateTime DataFim
	{
		get => dataFim;
		private set
		{
			if (dataFim != value)
			{
				dataFim = value;
				OnPropertyChanged(nameof(DataFim));
			}
		}
	}

	public string RestricaoMedica { get; set; }
	public string ObsRestricao { get; set; }
	public byte[] LaudoMedico { get; set; }
	public string Objetivo { get; set; }

	public void CalcularDataFim()
	{
		if (Plano < 1 || Plano > 4)
		{
			throw new ArgumentException("Plano inválido. Escolha entre 1 (Mensal), 2 (Trimestral), 3 (Semestral) ou 4 (Anual).");
		}

		DataFim = Plano switch
		{
			1 => DataInicio.AddMonths(1),
			2 => DataInicio.AddMonths(3),
			3 => DataInicio.AddMonths(6),
			4 => DataInicio.AddYears(1),
			_ => throw new InvalidOperationException("Plano não tratado.")
		};
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}