create procedure dbo.p_atribuirNotaAAlunoEmUC @param_num_aluno int, @param_sig_uc varchar(6), @param_ano int, @param_nota int
as
	if exists (select num_aluno, sig_uc from Inscrição where num_aluno = @param_num_aluno and sig_uc = @param_sig_uc)
	begin
		update Inscrição set nota = @param_nota where num_aluno = @param_num_aluno and sig_uc = @param_sig_uc
	end

drop procedure dbo.p_atribuirNotaAAlunoEmUC
