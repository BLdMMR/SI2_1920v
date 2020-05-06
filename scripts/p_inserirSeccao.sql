create procedure dbo.p_inserirSeccao @param_sig_un varchar(6), @param_descr text, @param_sig_dep varchar(6)
as 
	set Nocount on

	begin transaction 

	if @param_sig_dep not in (select sig_un from Departamento)
		RAISERROR('Invalid parameter: sig_dep', 16, 1);
	

	insert into Secção(sig_un, descr, sig_dep) values (@param_sig_un, @param_descr, @param_sig_dep)

	commit

drop procedure dbo.p_inserirSeccao