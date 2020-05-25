create procedure dbo.p_criarCurso @param_sig_un varchar(6), @param_descr text, @param_sig_dep varchar(6), @num_anos int
as 
	begin transaction
	if not exists(select * from Curso where sig_un = @param_sig_un)
	begin
		insert into Curso(sig_un, descr, sig_dep) values (@param_sig_un, @param_descr, @param_sig_dep)
		declare @ano int
		declare @semestre int
		set @ano = 1
		set @semestre = 1
		while(@ano < @num_anos)
		begin
			insert into Ano(ano, semestre, sig_curs) values(@ano, @semestre, @param_sig_un)
			set @semestre = @semestre + 1
			if @semestre = 2 
			begin
				set @semestre = 1;
				set @ano = @ano + 1
			end
		end
	end

drop procedure dbo.p_criarCurso
