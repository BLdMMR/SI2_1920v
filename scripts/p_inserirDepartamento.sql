create procedure dbo.p_inserirDepartamento @param_sig_un varchar(6), @param_descr text
as 
	begin transaction
	if not exists (select sig_un from Departamento where sig_un = @param_sig_un)
	begin
		insert into Departamento(sig_un, descr) values (@param_sig_un, @param_descr)
	end
	commit

drop procedure dbo.p_inserirDepartamento