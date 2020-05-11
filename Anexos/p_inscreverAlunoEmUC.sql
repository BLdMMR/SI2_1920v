create procedure dbo.p_inscreverAlunoEmUC @param_num_aluno int, @param_sig_uc varchar(6), @param_ano int
as
	set nocount on
	begin transaction
	--create table matricula em ano para o aluno poder increver em disciplinas
	if exists (select num_aluno from MatriculaAlunoEmAno where num_aluno = @param_num_aluno)
	begin
		insert into Inscrição(ano, num_aluno, sig_uc) values (@param_ano, @param_num_aluno, @param_sig_uc)
	end
	else
	begin
		raiserror('Aluno não se encontra matriculado no ano', 16, 1);
	end
	commit

drop procedure dbo.p_inscreverAlunoEmUC