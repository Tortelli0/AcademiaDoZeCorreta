using AcademiaDoZe.Model;
using System.Configuration;
using System.Data.Common;

namespace AcademiaDoZe.DataAcess;

public class MatriculaRepository
{
	private readonly DbProviderFactory factory;
	private readonly string connectionString;
	private readonly string _connectionString;
	private readonly string _providerName;

	public MatriculaRepository()
	{
		_providerName = System.Configuration.ConfigurationManager.ConnectionStrings["BD"].ProviderName;
		_connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BD"].ConnectionString;

		factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["BD"].ProviderName);
		connectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
	}

	public bool ExisteMatriculaAtiva(int idAluno, int idMatriculaAtual = 0)
	{
		DbProviderFactory factory = DbProviderFactories.GetFactory(_providerName);
		using var conexao = factory.CreateConnection();
		conexao.ConnectionString = _connectionString;

		using var comando = conexao.CreateCommand();
		comando.CommandText = @"SELECT COUNT(*) FROM tb_matricula WHERE aluno_id = @aluno_id AND data_fim >= NOW() AND id_matricula != @id_matricula_atual";

		var parametroAluno = comando.CreateParameter();
		parametroAluno.ParameterName = "@aluno_id";
		parametroAluno.Value = idAluno;
		comando.Parameters.Add(parametroAluno);

		var parametroMatricula = comando.CreateParameter();
		parametroMatricula.ParameterName = "@id_matricula_atual";
		parametroMatricula.Value = idMatriculaAtual;
		comando.Parameters.Add(parametroMatricula);

		conexao.Open();

		var resultado = comando.ExecuteScalar();
		return Convert.ToInt32(resultado) > 0;
	}

	public List<Matricula> GetAll()
	{
		var matriculas = new List<Matricula>();

		try
		{
			using var connection = factory.CreateConnection();
			connection.ConnectionString = connectionString;

			using var command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM tb_matricula";
			connection.Open();

			using var reader = command.ExecuteReader();
			while (reader.Read())
			{
				matriculas.Add(new Matricula
				{
					Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
					IdAluno = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
					IdColaborador = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
					Plano = !reader.IsDBNull(3) ? reader.GetString(3)[0] - '0' : 0,
					DataInicio = !reader.IsDBNull(4) ? reader.GetDateTime(4) : DateTime.MinValue,
					Objetivo = !reader.IsDBNull(6) ? reader.GetString(6) : string.Empty,
					RestricaoMedica = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty,
					ObsRestricao = !reader.IsDBNull(8) ? reader.GetString(8) : string.Empty,
					LaudoMedico = !reader.IsDBNull(9) ? (byte[])reader["laudo_medico"] : null
				});
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Erro ao obter matrículas.", ex);
		}

		return matriculas;
	}

	public void Add(Matricula matricula)
	{
		try
		{
			if (matricula.IdAluno <= 0)
			{
				throw new ArgumentException("Aluno inválido. O CPF fornecido não corresponde a um aluno no banco de dados.");
			}

			matricula.CalcularDataFim();

			using var connection = factory.CreateConnection();
			connection.ConnectionString = connectionString;

			using var command = connection.CreateCommand();
			command.CommandText = @"INSERT INTO tb_matricula (aluno_id, colaborador_id, plano, data_inicio, data_fim, restricao_medica, obs_restricao, laudo_medico, objetivo)
                    VALUES (@aluno_id, @colaborador_id, @plano, @data_inicio, @data_fim, @restricao_medica, @obs_restricao, @laudo_medico, @objetivo)";

			AdicionarParametros(command, matricula);
			connection.Open();
			command.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			throw new Exception("Erro ao adicionar matrícula.", ex);
		}
	}

	public void Update(Matricula matricula)
	{
		try
		{
			matricula.CalcularDataFim();

			using var connection = factory.CreateConnection();
			connection.ConnectionString = connectionString;

			using var command = connection.CreateCommand();
			command.CommandText = @"UPDATE tb_matricula SET aluno_id=@aluno_id, colaborador_id=@colaborador_id, plano=@plano, data_inicio=@data_inicio, data_fim=@data_fim, restricao_medica=@restricao_medica,
                    obs_restricao=@obs_restricao, laudo_medico=@laudo_medico, objetivo=@objetivo
                    WHERE id_matricula=@id";

			command.Parameters.Add(CreateParameter(command, "@id", matricula.Id));
			AdicionarParametros(command, matricula);
			connection.Open();
			command.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			throw new Exception("Erro ao atualizar matrícula.", ex);
		}
	}

	public void Delete(Matricula matricula)
	{
		try
		{
			using var connection = factory.CreateConnection();
			connection.ConnectionString = connectionString;

			using var command = connection.CreateCommand();
			command.CommandText = "DELETE FROM tb_matricula WHERE id_matricula=@id";
			command.Parameters.Add(CreateParameter(command, "@id", matricula.Id));

			connection.Open();
			command.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			throw new Exception("Erro ao excluir matrícula.", ex);
		}
	}

	private void AdicionarParametros(DbCommand command, Matricula matricula)
	{
		command.Parameters.Add(CreateParameter(command, "@aluno_id", matricula.IdAluno));
		command.Parameters.Add(CreateParameter(command, "@colaborador_id", matricula.IdColaborador));
		command.Parameters.Add(CreateParameter(command, "@plano", matricula.Plano));
		command.Parameters.Add(CreateParameter(command, "@data_inicio", matricula.DataInicio));
		command.Parameters.Add(CreateParameter(command, "@data_fim", matricula.DataFim));
		command.Parameters.Add(CreateParameter(command, "@restricao_medica", matricula.RestricaoMedica));
		command.Parameters.Add(CreateParameter(command, "@obs_restricao", matricula.ObsRestricao));
		command.Parameters.Add(CreateParameter(command, "@laudo_medico", matricula.LaudoMedico));
		command.Parameters.Add(CreateParameter(command, "@objetivo", matricula.Objetivo));
	}

	private DbParameter CreateParameter(DbCommand command, string name, object value)
	{
		var parameter = command.CreateParameter();
		parameter.ParameterName = name;
		parameter.Value = value ?? DBNull.Value;
		return parameter;
	}
}