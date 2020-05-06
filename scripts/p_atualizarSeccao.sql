create procedure p_atualizarSeccao @param_sig_un varchar(6), @param_descr text, @param_sig_dep varchar(6)
as
	
	if exists (select * from Secção where sig_un = @param_sig_un)
	begin
		if @param_descr is null
		begin
			update Secção set sig_dep = @sig_dep where sig_un = @param_sig_un
		end
		else if @param_sig_dep is null
		begin
			update Secção set descr = @param_descr where sig_un = @param_sig_un
		end
	end
	else
	begin
		exec dbo.p_inserirSeccao @param_sig_un, @param_descr, @param_sig_dep
	end
