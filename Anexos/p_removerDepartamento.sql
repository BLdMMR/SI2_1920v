create procedure dbo.p_removerDepartamento @param_sig_un varchar(6)
as
	begin transaction
	if exists (select sig_un from Departamento where sig_un = @param_sig_un)
	begin	
		delete from Departamento where sig_un = @param_sig_un
	end

	commit

drop procedure dbo.p_removerDepartamento