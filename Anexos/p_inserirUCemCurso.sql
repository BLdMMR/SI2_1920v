create procedure dbo.p_inserirUCInCurso @param_sig_uc varchar(6), @param_num_cred int, @param_descr text, @param_sig_curs varchar(6), @param_semestre int
as
	begin transaction
	if not exists (select sig_un from UC where sig_un = @param_sig_uc)
	begin	
		insert into UC(sig_un, num_cred, descr) values (@param_sig_uc, @param_num_cred, @param_descr)
	end
	declare @ano int
	set @ano = (@param_semestre % 2 + @param_semestre / 2);
	insert into UCdeCurso(sig_uc, sig_curs, ano, semestre) values (@param_sig_uc, @param_sig_curs, @ano, @param_semestre)
	commit


drop procedure dbo.p_inserirUCInCurso