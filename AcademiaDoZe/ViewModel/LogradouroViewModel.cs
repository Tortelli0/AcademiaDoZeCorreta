using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.ViewModel;
public class LogradouroViewModel
{
	// implementa a interface INotifyPropertyChanged
	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	// utilizados no databinding
	public ObservableCollection<Model.Logradouro> Logradouros { get; set; }
	private Model.Logradouro _selectedLogradouro;
	public Model.Logradouro SelectedLogradouro
	{
		get => _selectedLogradouro; set
		{
			_selectedLogradouro = value;
			OnPropertyChanged(nameof(SelectedLogradouro));
		}
	}
	// Atributos para conexão e persistência com o banco de dados
	private readonly DbProviderFactory factory;
	private string ConnectionString { get; set; }
	private string ProviderName { get; set; }
	// Construtor
	public LogradouroViewModel(string providerName, string connectionString)
	{
		ConnectionString = connectionString;
		ProviderName = providerName;
		// define a factory, ou seja, o provedor de dados - Mysql, SqlServer, etc
		factory = DbProviderFactories.GetFactory(ProviderName);
		// inicializa a lista de logradouros
		Logradouros = new ObservableCollection<Model.Logradouro>();
	}
	// implementa os métodos de CRUD, utilizando DBProviderFactory
	// método para carregar os dados aqui

	public void Load()
	{
		Logradouros.Clear();
		using var conexao = factory.CreateConnection(); //Cria conexão
		conexao!.ConnectionString = ConnectionString; //Atribui a string de conexão
		using var comando = factory.CreateCommand(); //Cria comando
		comando!.Connection = conexao; //Atribui conexão
		conexao.Open();
		comando.CommandText = @"SELECT id_logradouro, cep, pais, uf, cidade, bairro, logradouro FROM tb_logradouro;";
		using var reader = comando.ExecuteReader();
		// carrega os dados para ser utilizado no databinding
		while (reader.Read())
		{
			Logradouros.Add(new Model.Logradouro
			{
				Id = reader.GetInt32(0),
				Cep = reader.GetString(1),
				Pais = reader.GetString(2),
				Uf = reader.GetString(3),
				Cidade = reader.GetString(4),
				Bairro = reader.GetString(5),
				Nome = reader.GetString(6)
			});
		}
	}
	// método para inserir os dados aqui

	public void Add(Model.Logradouro dado)
	{
		using var conexao = factory.CreateConnection(); // Cria conexão
		conexao!.ConnectionString = ConnectionString; // Atribui a string de conexão
		using var comando = factory.CreateCommand(); // Cria comando
		comando!.Connection = conexao; // Atribui conexão

		// Adiciona parâmetro (@campo e valor)
		var cep = comando.CreateParameter();
		cep.ParameterName = "@cep";
		cep.Value = dado.Cep;
		comando.Parameters.Add(cep);

		var pais = comando.CreateParameter();
		pais.ParameterName = "@pais";
		pais.Value = dado.Pais;
		comando.Parameters.Add(pais);

		var uf = comando.CreateParameter();
		uf.ParameterName = "@uf";
		uf.Value = dado.Uf;
		comando.Parameters.Add(uf);

		var cidade = comando.CreateParameter();
		cidade.ParameterName = "@cidade";
		cidade.Value = dado.Cidade;
		comando.Parameters.Add(cidade);

		var bairro = comando.CreateParameter();
		bairro.ParameterName = "@bairro";
		bairro.Value = dado.Bairro;
		comando.Parameters.Add(bairro);

		var logradouro = comando.CreateParameter();
		logradouro.ParameterName = "@logradouro";
		logradouro.Value = dado.Nome;
		comando.Parameters.Add(logradouro);

		conexao.Open();
		comando.CommandText = @"INSERT INTO tb_logradouro (cep, pais, uf, cidade, bairro, logradouro) VALUES (@cep, @pais, @uf, @cidade, @bairro, @logradouro);";

		// Executa o script na conexão e armazena o número de linhas afetadas.
		var linhas = comando.ExecuteNonQuery();
	}
	// método para atualizar os dados aqui

	public void Update(Model.Logradouro dado)
	{
		using var conexao = factory.CreateConnection(); // Cria conexão
		conexao!.ConnectionString = ConnectionString; // Atribui a string de conexão
		using var comando = factory.CreateCommand(); // Cria comando
		comando!.Connection = conexao; // Atribui conexão

		// Adiciona parâmetro (@campo e valor)
		var id = comando.CreateParameter();
		id.ParameterName = "@id";
		id.Value = dado.Id;
		comando.Parameters.Add(id);

		var cep = comando.CreateParameter();
		cep.ParameterName = "@cep";
		cep.Value = dado.Cep;
		comando.Parameters.Add(cep);

		var pais = comando.CreateParameter();
		pais.ParameterName = "@pais";
		pais.Value = dado.Pais;
		comando.Parameters.Add(pais);

		var uf = comando.CreateParameter();
		uf.ParameterName = "@uf";
		uf.Value = dado.Uf;
		comando.Parameters.Add(uf);

		var cidade = comando.CreateParameter();
		cidade.ParameterName = "@cidade";
		cidade.Value = dado.Cidade;
		comando.Parameters.Add(cidade);

		var bairro = comando.CreateParameter();
		bairro.ParameterName = "@bairro";
		bairro.Value = dado.Bairro;
		comando.Parameters.Add(bairro);

		var logradouro = comando.CreateParameter();
		logradouro.ParameterName = "@logradouro";
		logradouro.Value = dado.Nome;
		comando.Parameters.Add(logradouro);

		conexao.Open();
		comando.CommandText = @"UPDATE tb_logradouro SET cep = @cep, pais = @pais, uf = @uf, cidade = @cidade, bairro = @bairro, logradouro = @logradouro WHERE id_logradouro = @id;";

		// Executa o comando no banco de dados
		_ = comando.ExecuteNonQuery();
	}

	// método para deletar os dados aqui


	public void Delete(Model.Logradouro dado)
	{
		using var conexao = factory.CreateConnection(); //Cria conexão
		conexao!.ConnectionString = ConnectionString; //Atribui a string de conexão
		using var comando = factory.CreateCommand(); // Cria comando
		comando!.Connection = conexao; // Atribui Conexao

		// Adiciona parâmetro (@campo e valor)
		var id = comando.CreateParameter();
		id.ParameterName = "@id";
		id.Value = dado.Id;
		comando.Parameters.Add(id);

		conexao.Open();

		//realiza o DELETE
		comando.CommandText = @"DELETE FROM tb_logradouro WHERE id_logradouro = @id;";

		//executa o comando no banco de dados
		_ = comando.ExecuteNonQuery();
	}
}
