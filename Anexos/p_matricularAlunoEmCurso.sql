create procedure dbo.p_matricularAlunoEmCurso 
@param_num_cc_aluno varchar(8), @param_nome varchar(255),
@param_endereco varchar(255), @param_cod_post varchar(8), 
@param_localidade varchar(255), @param_data_nasc date,
@param_sig_curs varchar(6)
as
	set NOCOUNT on
	begin transaction
	if not exists (select num_cc from Aluno where num_cc = @param_num_cc_aluno)
	begin
		insert into Aluno(num_cc, nome, endereço, cod_post, localidade, data_nasc) 
			values (@param_num_cc_aluno, @param_nome, @param_endereco, @param_cod_post, @param_localidade, @param_data_nasc)
	end
	insert into Matricula(num_aluno, sig_curs, data_inic, data_conc, média)
		values (@param_num_cc_aluno, @param_sig_curs, GETDATE(), NULL, NULL)

	commit

drop procedure dbo.p_matricularAlunoEmCurso