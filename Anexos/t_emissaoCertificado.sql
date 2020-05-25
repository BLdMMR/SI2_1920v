create trigger dbo.t_emissaoCertificado on 
Inscrição after update
as
begin	
	
	begin try
		declare curs cursor for select num_aluno from Aluno
		declare @num_aluno	int
		declare @cerd_atuais int 
		declare @curso varchar(6)
		open @curs
		fetch next from @curs into @num_aluno
		while @@FETCH_STATUS = 0
		begin
			set @cerd_atuais = (select sum(UC.num_cred) from Inscrição inner join 
			UC on Inscrição.sig_uc = UC.sig_un
			where Inscrição.nota is not null and num_aluno = @num_aluno)
	
			set @curso = (select sig_curs from Matricula where num_aluno = @num_aluno and data_conc is null)
			if @cerd_atuais = dbo.f_totalCreditos(@curso)
			begin
				declare @media float = (select AVG(nota) from inscrição where num_aluno = @num_aluno)
				update Matricula set média = @media, data_conc = GETDATE() where num_aluno = @num_aluno and sig_curs = @curso
			end
			fetch next from curs into @num_aluno
		end
		commit
	end try
	begin catch
		rollback
		raiserror('Something went wrong in trigger execution', 16, 1);
	end catch
end


	
