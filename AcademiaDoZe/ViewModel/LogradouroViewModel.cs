using AcademiaDoZe.DataAcess;
using AcademiaDoZe.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AcademiaDoZe.ViewModel;
public class LogradouroViewModel : ViewModelBase
{
	// Objetos utilizados no Databinding
	// Recurso que permite a sincronização automática entre View e ViewModel,
	// através da propriedade DataContext da View.
	public ObservableCollection<Logradouro> Logradouros { get; set; }
	private Logradouro _selectedLogradouro;
	public Logradouro SelectedLogradouro
	{
		get { return _selectedLogradouro; }
		set
		{
			_selectedLogradouro = value;
			OnPropertyChanged("SelectedLogradouro");
		}
	}

	// atributo para acessar o banco de dados
	private LogradouroRepository _repository;

	// Comandos para o CRUD
	public ICommand LogradouroAdicionarCommand { get; set; }
	public ICommand LogradouroAtualizarCommand { get; set; }
	public ICommand LogradouroRemoverCommand { get; set; }

	public LogradouroViewModel()
	{
		Logradouros = new ObservableCollection<Logradouro>();
		_repository = new LogradouroRepository();

		//Inicializando os comandos
		LogradouroAdicionarCommand = new RelayCommand(AdicionarLogradouro);
		LogradouroAtualizarCommand = new RelayCommand(AtualizarLogradouro);
		LogradouroRemoverCommand = new RelayCommand(RemoverLogradouro);

		GetAll();
	}
	public void GetAll()
	{
		// busca no banco de dados e carrega em Logradouros
		Logradouros.Clear();
		_repository.GetAll().ForEach(data => Logradouros.Add(data));
	}

	private void AdicionarLogradouro(object obj)
	{
		if (SelectedLogradouro == null) return;
		if (MessageBox.Show("Confirma?", "Logradouro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
		{
			try
			{
				_repository.Add(SelectedLogradouro);
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
	private void AtualizarLogradouro(object obj)
	{
		if (SelectedLogradouro == null) return;
		if (MessageBox.Show("Confirma?", "Logradouro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
		{
			try
			{
				_repository.Update(SelectedLogradouro);

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
	private void RemoverLogradouro(object obj)
	{
		if (SelectedLogradouro == null) return;
		if (MessageBox.Show("Confirma?", "Logradouro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
		{
			try
			{
				_repository.Delete(SelectedLogradouro);

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
