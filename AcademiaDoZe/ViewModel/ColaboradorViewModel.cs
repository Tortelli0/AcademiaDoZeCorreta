using AcademiaDoZe.DataAcess;
using AcademiaDoZe.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace AcademiaDoZe.ViewModel;

public class ColaboradorViewModel : ViewModelBase
{
	// Objetos utilizados no Databinding, recurso que permite a sincronização automática entre View e ViewModel, através da propriedade DataContext da View.
	public ObservableCollection<Colaborador> Colaboradors { get; set; }

	private Colaborador _selectedColaborador;

	public Colaborador SelectedColaborador
	{
		get { return _selectedColaborador; }
		set
		{
			_selectedColaborador = value;
			OnPropertyChanged("SelectedColaborador");
			// libera somente se houver um Colaborador selecionado
			ColaboradorAtualizarCommand.RaiseCanExecuteChanged();
			ColaboradorRemoverCommand.RaiseCanExecuteChanged();
		}
	}

	// atributo para acessar o banco de dados
	private ColaboradorRepository _repository;

	// Comandos para o CRUD
	public RelayCommand ColaboradorAdicionarCommand { get; set; }

	public RelayCommand ColaboradorAtualizarCommand { get; set; }
	public RelayCommand ColaboradorRemoverCommand { get; set; }

	public ColaboradorViewModel()
	{
		Colaboradors = new ObservableCollection<Colaborador>();
		_repository = new ColaboradorRepository();
		// Inicializando os comandos
		ColaboradorAdicionarCommand = new RelayCommand(AdicionarColaborador);
		ColaboradorAtualizarCommand = new RelayCommand(AtualizarColaborador, CanExecuteSubmit);
		ColaboradorRemoverCommand = new RelayCommand(RemoverColaborador, CanExecuteSubmit);
		GetAll();
	}

	private bool CanExecuteSubmit(object parameter)
	{
		// validação utilizada para liberar ou não os botões de atualizar e remover na view
		return SelectedColaborador != null;
	}

	// métodos com as operações de CRUD aqui

	public void GetAll()
	{
		// busca no banco de dados e carrega em Colaboradors, limpando antes
		Colaboradors.Clear();
		_repository.GetAll().ForEach(data => Colaboradors.Add(data));
	}

	private void AdicionarColaborador(object obj)
	{
		WindowColaborador janelaCadastro = new WindowColaborador();
		var viewModel = (ColaboradorCadastroViewModel)janelaCadastro.DataContext;
		viewModel.ColaboradorSalvo += (sender, e) =>
		{
			try
			{
				var novoColaborador = viewModel.GetColaborador();
				_repository.Add(novoColaborador);
				janelaCadastro.Close();
				MessageBox.Show("Sucesso.");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			finally
			{
				GetAll();
			}
		};
		janelaCadastro.ShowDialog();
	}

	private void AtualizarColaborador(object obj)
	{
		WindowColaborador janelaCadastro = new WindowColaborador();
		var viewModel = new ColaboradorCadastroViewModel(SelectedColaborador);
		janelaCadastro.DataContext = viewModel;
		viewModel.ColaboradorSalvo += (sender, e) =>
		{
			try
			{
				var ColaboradorEditado = viewModel.GetColaborador();
				_repository.Update(ColaboradorEditado);
				janelaCadastro.Close();
				MessageBox.Show("Sucesso.");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			finally
			{
				GetAll();
			}
		};
		janelaCadastro.ShowDialog();
	}

	private void RemoverColaborador(object obj)
	{
		if (SelectedColaborador == null) return;
		if (MessageBox.Show("Confirma?", "Colaborador", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
		{
			try
			{
				_repository.Delete(SelectedColaborador);
				MessageBox.Show("Sucesso.");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
			finally
			{
				GetAll();
			}
		}
	}
}