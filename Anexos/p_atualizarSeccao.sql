create procedure p_atualizarSeccao @param_sig_un varchar(6), @param_descr text, @param_sig_dep varchar(6)
as
	
	if exists (select * from Sec��o where sig_un = @param_sig_un)
	begin
		if @param_descr is null
		begin
			update Sec��o set sig_dep = @sig_dep where sig_un = @param_sig_un
		end
		else if @param_sig_dep is null
		begin
			update Sec��o set descr = @param_descr where sig_un = @param_sig_un
		end
		else
		begin
			update Sec��o set descr = @param_descr, sig_dep = @param_sig_dep where sig_un = @param_sig_un
		end
	end
	else
	begin
		raiserror('Invalid Parameter: Sec��o.sig_un', 16, 2)
	end
