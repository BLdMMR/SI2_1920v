create procedure dbo.p_atualizarDepartamento @param_sig_un varchar(6), @param_descr text
as 
	begin transaction
	if exists (select sig_un from Departamento where sig_un = @param_sig_un)
	begin
		update Departamento set descr = @param_descr where sig_un = @param_sig_un
	end
	else
	begin
		raiserror('Invalid parameter: Departamento.sig_un', 16, 1)
	end
	commit

drop procedure dbo.p_atualizarDepartamento
