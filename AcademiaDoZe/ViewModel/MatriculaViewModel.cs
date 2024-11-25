using AcademiaDoZe.DataAcess;
using AcademiaDoZe.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace AcademiaDoZe.ViewModel;

public class MatriculaViewModel : ViewModelBase
{
	private readonly MatriculaRepository _repository;

	public ObservableCollection<Matricula> Matriculas { get; set; }

	private Matricula _selectedMatricula;

	private readonly MatriculaRepository _matriculaRepository;

	private Matricula _matriculaSelecionada;

	public Matricula MatriculaSelecionada
	{
		get => _matriculaSelecionada;
		set
		{
			_matriculaSelecionada = value;
			OnPropertyChanged(nameof(MatriculaSelecionada));

			AtualizarCommand.RaiseCanExecuteChanged();
			RemoverCommand.RaiseCanExecuteChanged();
		}
	}

	public MatriculaViewModel()
	{
		_matriculaRepository = new MatriculaRepository();
		_repository = new MatriculaRepository();
		Matriculas = new ObservableCollection<Matricula>();
		CarregarMatriculas();

		AdicionarCommand = new RelayCommand(AdicionarMatricula);
		AtualizarCommand = new RelayCommand(AtualizarMatricula, CanExecuteEditarExcluir);
		RemoverCommand = new RelayCommand(RemoverMatricula, CanExecuteEditarExcluir);
	}

	public void CarregarMatriculas()
	{
		Matriculas.Clear();

		var listaMatriculas = _matriculaRepository.GetAll();
		foreach (var matricula in listaMatriculas)
		{
			Matriculas.Add(matricula);
		}
	}

	public Matricula SelectedMatricula
	{
		get => _selectedMatricula;
		set
		{
			_selectedMatricula = value;
			OnPropertyChanged(nameof(SelectedMatricula));

			AtualizarCommand.RaiseCanExecuteChanged();
			RemoverCommand.RaiseCanExecuteChanged();
		}
	}

	public RelayCommand AdicionarCommand { get; private set; }
	public RelayCommand AtualizarCommand { get; private set; }
	public RelayCommand RemoverCommand { get; private set; }

	private void AdicionarMatricula(object obj)
	{
		var janelaCadastro = new WindowMatricula();
		var viewModel = new MatriculaCadastroViewModel();
		janelaCadastro.DataContext = viewModel;

		viewModel.MatriculaSalva += (sender, e) =>
		{
			CarregarMatriculas();
			janelaCadastro.Close();
			MessageBox.Show("Matrícula adicionada com sucesso.");
		};

		janelaCadastro.ShowDialog();
	}

	private void AtualizarMatricula(object obj)
	{
		if (SelectedMatricula == null) return;

		var janelaCadastro = new WindowMatricula();
		var viewModel = new MatriculaCadastroViewModel(SelectedMatricula);
		janelaCadastro.DataContext = viewModel;

		viewModel.MatriculaSalva += (sender, e) =>
		{
			CarregarMatriculas();
			janelaCadastro.Close();
			MessageBox.Show("Matrícula atualizada com sucesso.");
		};

		janelaCadastro.ShowDialog();
	}

	private void RemoverMatricula(object obj)
	{
		if (SelectedMatricula == null) return;

		if (MessageBox.Show("Confirma a exclusão?", "Excluir Matrícula", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
		{
			_matriculaRepository.Delete(SelectedMatricula);
			CarregarMatriculas();
			MessageBox.Show("Matrícula excluída com sucesso.");
		}
	}

	private bool CanExecuteEditarExcluir(object arg)
	{
		return SelectedMatricula != null;
	}
}