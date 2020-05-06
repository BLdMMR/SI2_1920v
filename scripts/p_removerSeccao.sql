create procedure dbo.p_removerSeccao @param_sig_un varchar(6)
as 
	begin transaction 

	if @param_sig_un not in (select sig_un from Sec��o)
		raiserror('Invalid parameter: sig_un', 16, 1);
	end

	delete from Sec��o where sig_un = @param_sig_un

	commit

drop procedure dbo.p_removerSeccao