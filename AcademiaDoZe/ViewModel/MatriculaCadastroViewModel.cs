using AcademiaDoZe.DataAcess;
using AcademiaDoZe.Model;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AcademiaDoZe.ViewModel;

public class MatriculaCadastroViewModel : ViewModelBase
{
	private readonly AlunoRepository _alunoRepository;
	private readonly MatriculaRepository _matriculaRepository;
	private readonly ColaboradorRepository _colaboradorRepository;

	private DateTime? _dataNascimentoAluno;

	public Matricula Matricula { get; set; }
	public ICommand SalvarCommand { get; private set; }
	public ICommand CarregarLaudoCommand { get; private set; }

	public event EventHandler MatriculaSalva;

	public MatriculaCadastroViewModel()
	{
		Matricula = new Matricula();
		_alunoRepository = new AlunoRepository();
		_matriculaRepository = new MatriculaRepository();
		SalvarCommand = new RelayCommand(SalvarMatricula);
		CarregarLaudoCommand = new RelayCommand(CarregarLaudo);
		_colaboradorRepository = new ColaboradorRepository();
	}

	private int _idAluno;

	public int IdAluno
	{
		get => _idAluno;
		set
		{
			_idAluno = value;
			OnPropertyChanged(nameof(IdAluno));
		}
	}

	public MatriculaCadastroViewModel(Matricula matricula) : this()
	{
		Matricula = matricula;
	}

	public void PesquisarAlunoPorCPF(string cpf)
	{
		try
		{
			var aluno = _alunoRepository.BuscarAlunoPorCpf(cpf);
			if (aluno != null)
			{
				Matricula.IdAluno = aluno.Id;
				_dataNascimentoAluno = aluno.Nascimento;
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

	public void BuscarColaboradorPorCpf(string cpf)
	{
		try
		{
			var colaborador = _colaboradorRepository.BuscarColaboradorPorCpf(cpf);
			if (colaborador != null)
			{
				Matricula.IdColaborador = colaborador.Id;
			}
			else
			{
				MessageBox.Show("Colaborador não encontrado! Verifique o CPF informado.");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Erro ao buscar Colaborador pelo CPF: {ex.Message}");
		}
	}

	private void SalvarMatricula(object obj)
	{
		try
		{
			if (string.IsNullOrEmpty(Matricula.IdAluno.ToString()) || Matricula.IdAluno == 0)
			{
				MessageBox.Show("Aluno não encontrado. Por favor, busque um aluno válido.");
				return;
			}

			if (string.IsNullOrEmpty(Matricula.IdColaborador.ToString()) || Matricula.IdColaborador == 0)
			{
				MessageBox.Show("Colaborador não encontrado. Por favor, busque um colaborador válido.");
				return;
			}

			if (Matricula.Plano <= 0)
			{
				MessageBox.Show("Selecione um Plano válido antes de salvar.");
				return;
			}

			if (_matriculaRepository.ExisteMatriculaAtiva(Matricula.IdAluno, Matricula.Id))
			{
				MessageBox.Show("Já existe uma matrícula ativa para este aluno.");
				return;
			}

			if (Matricula.Id == 0)
			{
				_matriculaRepository.Add(Matricula);
			}
			else
			{
				_matriculaRepository.Update(Matricula);
			}

			MessageBox.Show("Matrícula salva com sucesso!");

			MatriculaSalva?.Invoke(this, EventArgs.Empty);
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Erro ao salvar matrícula: {ex.Message}");
		}
	}

	private BitmapImage laudoMedicoBitmap;

	public BitmapImage LaudoMedicoBitmap
	{
		get => laudoMedicoBitmap;
		set
		{
			laudoMedicoBitmap = value;
			OnPropertyChanged(nameof(LaudoMedicoBitmap));
		}
	}

	private void CarregarLaudo(object obj)
	{
		var openFileDialog = new OpenFileDialog
		{
			Filter = "Arquivos de Imagem (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
			Title = "Selecionar Laudo Médico"
		};

		if (openFileDialog.ShowDialog() == true)
		{
			Matricula.LaudoMedico = File.ReadAllBytes(openFileDialog.FileName);

			var bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.StreamSource = new MemoryStream(Matricula.LaudoMedico);
			bitmap.CacheOption = BitmapCacheOption.OnLoad;
			bitmap.EndInit();
			LaudoMedicoBitmap = bitmap;
		}
	}

	private DateTime _dataInicio;

	public DateTime DataInicio
	{
		get => _dataInicio;
		set
		{
			_dataInicio = value;
			OnPropertyChanged(nameof(DataInicio));
			AtualizarDataFim();
		}
	}

	private DateTime _dataFim;

	public DateTime DataFim
	{
		get => _dataFim;
		private set
		{
			_dataFim = value;
			OnPropertyChanged(nameof(DataFim));
		}
	}

	private int _plano;

	public int Plano
	{
		get => _plano;
		set
		{
			_plano = value;
			OnPropertyChanged(nameof(Plano));
			AtualizarDataFim();
		}
	}

	private void AtualizarDataFim()
	{
		if (Plano > 0)
		{
			DataFim = DataInicio.AddMonths(Plano);
		}
	}
}